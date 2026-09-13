using System.Text;
using System.Xml;
using Esi.OpenEN16931.Core;
using Esi.OpenEN16931.Html;
using Esi.OpenEN16931.PDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esi.OpenEN16931.Validation.Test;

[TestClass]
public sealed class ValidationReportTests
{
    [TestMethod]
    public async Task HtmlReportContainsValidStatusAndMessages()
    {
        var result = new ValidationResult
        {
            IsValid = true,
            Messages = new List<string> { "The invoice passed validation." }
        };

        string html = await InvoiceDescriptorHtmlRenderer.RenderValidationReportAsync(result);

        StringAssert.Contains(html, "The document is formally compliant.");
        StringAssert.Contains(html, "recommended for acceptance");
        StringAssert.Contains(html, "The invoice passed validation.");
    }

    [TestMethod]
    public async Task HtmlReportContainsInvalidStatusAndMessages()
    {
        var result = new ValidationResult
        {
            IsValid = false,
            Messages = new List<string> { "BR-01 failed." }
        };

        string html = await InvoiceDescriptorHtmlRenderer.RenderValidationReportAsync(result);

        StringAssert.Contains(html, "The document is not formally compliant.");
        StringAssert.Contains(html, "recommended for rejection");
        StringAssert.Contains(html, "BR-01 failed.");
    }

    [TestMethod]
    public void PdfReportStartsWithPdfSignature()
    {
        var result = new ValidationResult { IsValid = false };

        byte[] pdf = ValidationReportPdfRenderer.Render(result);

        Assert.IsTrue(pdf.Length > 5);
        StringAssert.StartsWith(Encoding.ASCII.GetString(pdf, 0, 5), "%PDF-");
    }

    [TestMethod]
    public void NullValidationResultIsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => ValidationReportPdfRenderer.Render((ValidationResult)null));
    }

    [TestMethod]
    public async Task KoSitSchematronMetadataIsRenderedInHtmlAndPdf()
    {
        var svrl = new XmlDocument();
        svrl.LoadXml("""
            <svrl:schematron-output xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
              <svrl:failed-assert id="BR-01" flag="error" location="/ubl:Invoice/cbc:ID">
                <svrl:text>Invoice identifier is required.</svrl:text>
              </svrl:failed-assert>
            </svrl:schematron-output>
            """);
        var report = new ValidationReport
        {
            DocumentReference = "invoice-001",
            DocumentType = "EN16931 XRechnung (UBL Invoice)"
        };

        ValidationReport.AddSchematronStep(svrl, "val-sch.1", "XRechnung Schematron", report);

        string html = await InvoiceDescriptorHtmlRenderer.RenderValidationReportAsync(report);
        byte[] pdf = ValidationReportPdfRenderer.Render(report);

        Assert.IsFalse(report.IsValid);
        Assert.AreEqual("BR-01", report.Steps[0].Messages[0].Code);
        Assert.AreEqual("error", report.Steps[0].Messages[0].Level);
        StringAssert.Contains(html, "BR-01");
        StringAssert.Contains(html, "/ubl:Invoice/cbc:ID");
        StringAssert.Contains(html, "recommended for rejection");
        StringAssert.StartsWith(Encoding.ASCII.GetString(pdf, 0, 5), "%PDF-");

        string reportDirectory = Path.Combine(
            FindRepositoryRoot(),
            "tests",
            "OpenEN16931.Validation.Test",
            "TestResults",
            "ValidationReports");
        Directory.CreateDirectory(reportDirectory);
        File.WriteAllText(Path.Combine(reportDirectory, "KoSitValidationReport.html"), html);
        File.WriteAllBytes(Path.Combine(reportDirectory, "KoSitValidationReport.pdf"), pdf);
    }

    private static string FindRepositoryRoot()
    {
        DirectoryInfo? directory = new(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (Directory.Exists(Path.Combine(directory.FullName, "src", "Dependencies", "Esi.OpenEn16931")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        Assert.Fail("Could not locate the Esi repository root from the test output directory.");
        return string.Empty;
    }
}