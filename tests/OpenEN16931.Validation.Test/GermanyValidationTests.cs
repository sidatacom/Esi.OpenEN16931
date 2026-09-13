using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using Esi.OpenEN16931.Core;
using Esi.OpenEN16931.Html;
using Esi.OpenEN16931.PDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutSmart.DAXon.Api;

namespace Esi.OpenEN16931.Validation.Test;

[TestClass]
public sealed class GermanyValidationTests
{
    private const string OfficialCiiInvoice =
        "origins/itplr-kosit/xrechnung-testsuite/src/test/technical-cases/cius/01.06_minimal_test_uncefact.xml";

    private const string OfficialUblInvoice =
        "origins/itplr-kosit/xrechnung-testsuite/src/test/technical-cases/cius/01.06_minimal_test_ubl.xml";

    [TestMethod]
    public void En16931CiiInvoicePassesNativeDaxonValidation() =>
        AssertValid(OfficialCiiInvoice, "EN16931/Schematron/cii/EN16931-CII-validation.xslt");

    [TestMethod]
    public void En16931UblInvoicePassesNativeDaxonValidation() =>
        AssertValid(OfficialUblInvoice, "EN16931/Schematron/ubl/EN16931-UBL-validation.xslt");

    [TestMethod]
    public void XRechnungCiiInvoicePassesNativeDaxonValidation() =>
        AssertValid(OfficialCiiInvoice, "XRechnung/Schematron/cii/XRechnung-CII-validation.xsl");

    [TestMethod]
    public void XRechnungUblInvoicePassesNativeDaxonValidation() =>
        AssertValid(OfficialUblInvoice, "XRechnung/Schematron/ubl/XRechnung-UBL-validation.xsl");

    [TestMethod]
    public void OfficialKositCiiReferenceDocumentPassesNativeDaxonValidation() =>
        AssertValid(
            "origins/itplr-kosit/xrechnung-testsuite/src/test/technical-cases/cius/01.05_minimal_test_uncefact.xml",
            "XRechnung/Schematron/cii/XRechnung-CII-validation.xsl");

    [TestMethod]
    public void OfficialKositComprehensiveCiiAndUblDocumentsPassNativeDaxonValidation()
    {
        string[] cases = { "01.01", "01.02", "01.03", "01.04" };

        foreach (string testCase in cases)
        {
            AssertValid(
                $"origins/itplr-kosit/xrechnung-testsuite/src/test/technical-cases/cius/{testCase}_comprehensive_test_uncefact.xml",
                "EN16931/Schematron/cii/EN16931-CII-validation.xslt");
            AssertValid(
                $"origins/itplr-kosit/xrechnung-testsuite/src/test/technical-cases/cius/{testCase}_comprehensive_test_ubl.xml",
                "EN16931/Schematron/ubl/EN16931-UBL-validation.xslt");
        }
    }

    [TestMethod]
    public async Task OfficialKositInvoicesProduceIndividualHtmlAndPdfReports()
    {
        string repositoryRoot = FindRepositoryRoot();
        string suiteRoot = Path.Combine(
            repositoryRoot,
            "src",
            "Dependencies",
            "Esi.OpenEn16931",
            "origins",
            "itplr-kosit",
            "xrechnung-testsuite",
            "src",
            "test");
        string reportDirectory = Path.Combine(
            repositoryRoot,
            "tests",
            "OpenEN16931.Validation.Test",
            "TestResults",
            "ValidationReports",
            "KoSIT");

        string[] invoices = Directory.GetFiles(suiteRoot, "*.xml", SearchOption.AllDirectories);
        Assert.AreEqual(86, invoices.Length, "The official KoSIT invoice fixture count changed.");
        Directory.CreateDirectory(reportDirectory);

        var reports = new List<ValidationReport>();
        foreach (string invoicePath in invoices.OrderBy(path => path, StringComparer.OrdinalIgnoreCase))
        {
            ValidationReport report = CreateReport(repositoryRoot, invoicePath);
            string reportName = Path.GetFileNameWithoutExtension(invoicePath);
            string html = await InvoiceDescriptorHtmlRenderer.RenderValidationReportAsync(report);
            byte[] pdf = ValidationReportPdfRenderer.Render(report);

            File.WriteAllText(Path.Combine(reportDirectory, reportName + ".html"), html);
            File.WriteAllBytes(Path.Combine(reportDirectory, reportName + ".pdf"), pdf);
            reports.Add(report);
        }

        var suiteReport = new ValidationReport
        {
            DocumentReference = "KoSIT XRechnung test suite",
            DocumentType = "86 official KoSIT invoice documents"
        };
        foreach (ValidationReport report in reports)
        {
            foreach (ValidationStepReport step in report.Steps)
            {
                var aggregateStep = new ValidationStepReport
                {
                    Id = $"{report.DocumentReference}/{step.Id}",
                    Name = $"{report.DocumentReference}: {step.Name}",
                    IsValid = step.IsValid
                };
                foreach (ValidationMessage message in step.Messages)
                {
                    aggregateStep.Messages.Add(new ValidationMessage
                    {
                        Id = $"{report.DocumentReference}/{message.Id}",
                        Code = message.Code,
                        Level = message.Level,
                        Text = message.Text,
                        XPathLocation = message.XPathLocation,
                        LineNumber = message.LineNumber,
                        ColumnNumber = message.ColumnNumber
                    });
                }

                suiteReport.Steps.Add(aggregateStep);
            }
        }

        suiteReport.IsValid = reports.All(report => report.IsValid);
        string suiteHtml = await InvoiceDescriptorHtmlRenderer.RenderValidationReportAsync(suiteReport);
        byte[] suitePdf = ValidationReportPdfRenderer.Render(suiteReport);
        string suiteHtmlPath = Path.Combine(reportDirectory, "KoSIT-Suite-ValidationReport.html");
        string suitePdfPath = Path.Combine(reportDirectory, "KoSIT-Suite-ValidationReport.pdf");
        File.WriteAllText(suiteHtmlPath, suiteHtml);
        File.WriteAllBytes(suitePdfPath, suitePdf);

        var expectedReportFiles = invoices
            .SelectMany(invoicePath => new[]
            {
                Path.GetFileNameWithoutExtension(invoicePath) + ".html",
                Path.GetFileNameWithoutExtension(invoicePath) + ".pdf"
            })
            .Append(Path.GetFileName(suiteHtmlPath))
            .Append(Path.GetFileName(suitePdfPath))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        string[] actualReportFiles = Directory.GetFiles(reportDirectory)
            .Select(Path.GetFileName)
            .Where(fileName => fileName is not null && expectedReportFiles.Contains(fileName))
            .ToArray()!;
        Assert.AreEqual(expectedReportFiles.Count, actualReportFiles.Length);
    }

    [TestMethod]
    public async Task OfficialKositCenMutationTestsMatchExpectedSchematronRules()
    {
        string repositoryRoot = FindRepositoryRoot();
        string mutationRoot = Path.Combine(
            repositoryRoot,
            "src",
            "Dependencies",
            "Esi.OpenEn16931",
            "origins",
            "itplr-kosit",
            "validator-configuration-xrechnung",
            "src",
            "test",
            "cen-unit-test");
        string[] mutationFiles = Directory.GetFiles(mutationRoot, "*.xml", SearchOption.TopDirectoryOnly);
        var failures = new List<string>();
        var mutationReports = new List<ValidationReport>();
        int generatedVariants = 0;

        foreach (string mutationFile in mutationFiles.OrderBy(path => path, StringComparer.OrdinalIgnoreCase))
        {
            var source = new XmlDocument { PreserveWhitespace = true };
            source.Load(mutationFile);
            XmlProcessingInstruction[] instructions = source
                .SelectNodes("//processing-instruction('xmute')")!
                .OfType<XmlProcessingInstruction>()
                .ToArray();

            for (int instructionIndex = 0; instructionIndex < instructions.Length; instructionIndex++)
            {
                XmlProcessingInstruction instruction = instructions[instructionIndex];
                Dictionary<string, string> attributes = ParseMutationAttributes(instruction.Data);
                string mutator = attributes.GetValueOrDefault("mutator", "identity");
                string expectedAttribute = attributes.ContainsKey("schematron-invalid")
                    ? "schematron-invalid"
                    : "schematron-valid";
                string[] expectedCodes = attributes.GetValueOrDefault(expectedAttribute, string.Empty)
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(NormalizeRuleCode)
                    .ToArray();
                string[] values = mutator == "code"
                    ? attributes.GetValueOrDefault("values", string.Empty)
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    : new[] { string.Empty };

                foreach (string value in values)
                {
                    generatedVariants++;
                    XmlDocument variant = CreateMutationVariant(source, instructionIndex, mutator, attributes, value);
                    string stylesheetPath = GetEn16931StylesheetPath(repositoryRoot, variant);
                    XmlDocument svrl = TransformToSvrl(variant, stylesheetPath);
                    var mutationReport = new ValidationReport
                    {
                        DocumentReference = $"{Path.GetFileName(mutationFile)}#{instructionIndex + 1}:{value}",
                        DocumentType = "KoSIT CEN mutation regression"
                    };
                    ValidationReport.AddSchematronStep(
                        svrl,
                        "val-sch.1",
                        $"Expected {expectedAttribute}: {string.Join(", ", expectedCodes)}",
                        mutationReport);
                    mutationReports.Add(mutationReport);
                    HashSet<string> failedCodes = svrl
                        .SelectNodes("//*[local-name()='failed-assert']")!
                        .OfType<XmlElement>()
                        .Select(element => NormalizeRuleCode(element.GetAttribute("id")))
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);
                    bool isExpectedInvalid = expectedAttribute == "schematron-invalid";
                    bool matches = isExpectedInvalid
                        ? expectedCodes.All(failedCodes.Contains)
                        : expectedCodes.All(expectedCode => !failedCodes.Contains(expectedCode));

                    if (!matches)
                    {
                        failures.Add($"{Path.GetFileName(mutationFile)} instruction {instructionIndex + 1} value '{value}': expected {expectedAttribute} [{string.Join(", ", expectedCodes)}], actual failed codes [{string.Join(", ", failedCodes.OrderBy(code => code))}]");
                    }
                }
            }
        }

        ValidationReport aggregateReport = new()
        {
            DocumentReference = "KoSIT CEN mutation regression suite",
            DocumentType = $"{generatedVariants} generated mutation variants"
        };
        foreach (ValidationReport mutationReport in mutationReports)
        {
            foreach (ValidationStepReport step in mutationReport.Steps)
            {
                aggregateReport.Steps.Add(step);
            }
        }

        aggregateReport.IsValid = failures.Count == 0;
        string reportDirectory = Path.Combine(
            repositoryRoot,
            "tests",
            "OpenEN16931.Validation.Test",
            "TestResults",
            "ValidationReports",
            "KoSIT");
        Directory.CreateDirectory(reportDirectory);
        string reportHtml = await InvoiceDescriptorHtmlRenderer.RenderValidationReportAsync(aggregateReport);
        byte[] reportPdf = ValidationReportPdfRenderer.Render(aggregateReport);
        File.WriteAllText(Path.Combine(reportDirectory, "KoSIT-CEN-Mutation-RegressionReport.html"), reportHtml);
        File.WriteAllBytes(Path.Combine(reportDirectory, "KoSIT-CEN-Mutation-RegressionReport.pdf"), reportPdf);

        Assert.AreEqual(0, failures.Count, string.Join(Environment.NewLine, failures));
        Assert.IsGreaterThan(0, generatedVariants);
    }

    private static void AssertValid(string invoiceRelativePath, string stylesheetRelativePath)
    {
        string repositoryRoot = FindRepositoryRoot();
        string invoicePath = Path.Combine(repositoryRoot, "src", "Dependencies", "Esi.OpenEn16931", invoiceRelativePath.Replace('/', Path.DirectorySeparatorChar));
        string stylesheetPath = Path.Combine(repositoryRoot, "src", "Dependencies", "Esi.OpenEn16931", "src", "Esi.OpenEN16931", "Validation", "Sources", "Germany", stylesheetRelativePath.Replace('/', Path.DirectorySeparatorChar));

        Assert.IsTrue(File.Exists(invoicePath), $"Invoice fixture was not found: {invoicePath}");
        Assert.IsTrue(File.Exists(stylesheetPath), $"Stylesheet fixture was not found: {stylesheetPath}");

        var processor = new Processor(false);
        var executable = processor.NewXsltCompiler().Compile(stylesheetPath);
        var transformer = executable.Load30();

        using var input = File.OpenRead(invoicePath);
        var document = processor.NewDocumentBuilder().Build(input, new Uri(invoicePath).AbsoluteUri);
        using var output = new StringWriter();
        transformer.SetGlobalContextItem(document, false);
        transformer.ApplyTemplates(document, processor.NewSerializer(output));

        var report = new XmlDocument();
        report.LoadXml(output.ToString());
        var failedAssertions = report.SelectNodes("//*[local-name()='failed-assert']");

        Assert.IsNotNull(failedAssertions);
        Assert.IsEmpty(failedAssertions, output.ToString());
    }

    private static ValidationReport CreateReport(string repositoryRoot, string invoicePath)
    {
        var invoice = new XmlDocument();
        invoice.Load(invoicePath);
        string syntax = invoice.DocumentElement?.LocalName == "CrossIndustryInvoice" ? "cii" : "ubl";
        string stylesheetPath = Path.Combine(
            repositoryRoot,
            "src",
            "Dependencies",
            "Esi.OpenEn16931",
            "src",
            "Esi.OpenEN16931",
            "Validation",
            "Sources",
            "Germany",
            "XRechnung",
            "Schematron",
            syntax,
            syntax == "cii" ? "XRechnung-CII-validation.xsl" : "XRechnung-UBL-validation.xsl");

        Assert.IsTrue(File.Exists(stylesheetPath), $"Stylesheet fixture was not found: {stylesheetPath}");

        var processor = new Processor(false);
        var executable = processor.NewXsltCompiler().Compile(stylesheetPath);
        var transformer = executable.Load30();
        using var input = File.OpenRead(invoicePath);
        var document = processor.NewDocumentBuilder().Build(input, new Uri(invoicePath).AbsoluteUri);
        using var output = new StringWriter();
        transformer.SetGlobalContextItem(document, false);
        transformer.ApplyTemplates(document, processor.NewSerializer(output));

        var svrl = new XmlDocument();
        svrl.LoadXml(output.ToString());
        var report = new ValidationReport
        {
            DocumentReference = Path.GetFileNameWithoutExtension(invoicePath),
            DocumentType = syntax == "cii"
                ? "EN16931 XRechnung (CII Invoice)"
                : "EN16931 XRechnung (UBL Invoice)"
        };
        ValidationReport.AddSchematronStep(svrl, "val-sch.1", "XRechnung Schematron", report);
        return report;
    }

    private static XmlDocument CreateMutationVariant(
        XmlDocument source,
        int instructionIndex,
        string mutator,
        IReadOnlyDictionary<string, string> attributes,
        string value)
    {
        var variant = new XmlDocument { PreserveWhitespace = true };
        variant.LoadXml(source.OuterXml);
        XmlProcessingInstruction[] instructions = variant
            .SelectNodes("//processing-instruction('xmute')")!
            .OfType<XmlProcessingInstruction>()
            .ToArray();
        XmlProcessingInstruction instruction = instructions[instructionIndex];
        XmlNode? targetNode = instruction.NextSibling;
        while (targetNode != null && targetNode is not XmlElement)
        {
            targetNode = targetNode.NextSibling;
        }

        XmlElement? target = targetNode as XmlElement;

        if (target == null)
        {
            throw new InvalidOperationException($"The xmute instruction at index {instructionIndex} has no following element.");
        }

        switch (mutator)
        {
            case "identity":
                break;
            case "remove":
                if (attributes.TryGetValue("attribute", out string? removeAttributeName))
                {
                    target.RemoveAttribute(removeAttributeName);
                }
                else
                {
                    target.ParentNode!.RemoveChild(target);
                }

                break;
            case "empty":
                if (attributes.TryGetValue("attribute", out string? emptyAttributeName))
                {
                    target.SetAttribute(emptyAttributeName, string.Empty);
                }
                else
                {
                    RemoveChildNodes(target);
                    target.InnerText = string.Empty;
                }

                break;
            case "code":
                if (attributes.TryGetValue("attribute", out string? attributeName))
                {
                    target.SetAttribute(attributeName, value);
                }
                else
                {
                    RemoveChildNodes(target);
                    target.InnerText = value;
                }

                break;
            default:
                throw new InvalidOperationException($"Unsupported xmute mutator '{mutator}'.");
        }

        foreach (XmlProcessingInstruction remainingInstruction in variant
                     .SelectNodes("//processing-instruction('xmute')")!
                     .OfType<XmlProcessingInstruction>()
                     .ToArray())
        {
            remainingInstruction.ParentNode!.RemoveChild(remainingInstruction);
        }

        return variant;
    }

    private static void RemoveChildNodes(XmlElement element)
    {
        while (element.HasChildNodes)
        {
            element.RemoveChild(element.FirstChild!);
        }
    }

    private static Dictionary<string, string> ParseMutationAttributes(string data)
    {
        return Regex.Matches(data, "(?<name>[A-Za-z][A-Za-z0-9-]*)=\"(?<value>[^\"]*)\"")
            .ToDictionary(match => match.Groups["name"].Value, match => match.Groups["value"].Value, StringComparer.OrdinalIgnoreCase);
    }

    private static string NormalizeRuleCode(string code)
    {
        int separator = code.IndexOf(':');
        return separator >= 0 ? code[(separator + 1)..] : code;
    }

    private static string GetEn16931StylesheetPath(string repositoryRoot, XmlDocument document)
    {
        string syntax = document.DocumentElement?.LocalName == "CrossIndustryInvoice" ? "cii" : "ubl";
        return Path.Combine(
            repositoryRoot,
            "src",
            "Dependencies",
            "Esi.OpenEn16931",
            "src",
            "Esi.OpenEN16931",
            "Validation",
            "Sources",
            "Germany",
            "EN16931",
            "Schematron",
            syntax,
            syntax == "cii" ? "EN16931-CII-validation.xslt" : "EN16931-UBL-validation.xslt");
    }

    private static XmlDocument TransformToSvrl(XmlDocument document, string stylesheetPath)
    {
        var processor = new Processor(false);
        var executable = processor.NewXsltCompiler().Compile(stylesheetPath);
        var transformer = executable.Load30();
        using var inputWriter = new StringWriter();
        document.Save(inputWriter);
        using var inputReader = new StringReader(inputWriter.ToString());
        var input = processor.NewDocumentBuilder().Build(inputReader, "urn:esi:mutation-input");
        using var output = new StringWriter();
        transformer.SetGlobalContextItem(input, false);
        transformer.ApplyTemplates(input, processor.NewSerializer(output));
        var svrl = new XmlDocument();
        svrl.LoadXml(output.ToString());
        return svrl;
    }

    private static string FindRepositoryRoot()
    {
        DirectoryInfo? directory = new(AppContext.BaseDirectory);
        while (directory != null)
        {
            string dependencyRoot = Path.Combine(directory.FullName, "src", "Dependencies", "Esi.OpenEn16931");
            if (Directory.Exists(dependencyRoot))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        Assert.Fail("Could not locate the Esi repository root from the test output directory.");
        return string.Empty;
    }
}