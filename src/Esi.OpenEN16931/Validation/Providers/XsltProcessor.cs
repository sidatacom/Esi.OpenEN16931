using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;
using Esi.OpenEN16931.Core;
using System;
using OutSmart.DAXon.Api;

namespace Esi.OpenEN16931.Validation.Providers;

/// <summary>
/// Implementation of XSLT transformation logic for XRechnung.
/// </summary>
public class XsltProcessor : IXsltProcessor
{
    private static readonly Processor DaxonProcessor = new Processor();
    private readonly ISchemaProvider _schemaProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="XsltProcessor"/> class with the specified schema provider.
    /// </summary>
    /// <param name="schemaProvider">The schema provider to use for retrieving XSLT schemas.</param>
    public XsltProcessor(ISchemaProvider schemaProvider)
    {
        _schemaProvider = schemaProvider ?? throw new ArgumentNullException(nameof(schemaProvider));
    }

    /// <summary>
    /// Transforms the provided XML document using the configured XSLTs.
    /// </summary>
    /// <param name="document">The XML document to transform.</param>
    /// <param name="xsltStream">The XSLT stream to use.</param>
    /// <returns>The transformed XML document, or the original if no transformation occurred.</returns>
    public XmlDocument Transform(XmlDocument document, Stream xsltStream)
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        var xslts = _schemaProvider.GetSchemas(XRechnungConformance.Extension, Syntax.UBL);
        if (xslts == null || xslts.Count == 0)
        {
            return document;
        }

        xsltStream ??= xslts.Values.FirstOrDefault();
        if (xsltStream == null)
        {
            throw new InvalidOperationException("The schema provider returned no XSLT stream.");
        }

        var executable = DaxonProcessor.NewXsltCompiler().Compile(xsltStream, "urn:esi:stylesheet");
        using var inputWriter = new StringWriter();
        document.Save(inputWriter);
        using var inputReader = new StringReader(inputWriter.ToString());
        var input = DaxonProcessor.NewDocumentBuilder().Build(inputReader, "urn:esi:input");
        using var outputWriter = new StringWriter();
        var transformer = executable.Load30();
        transformer.SetGlobalContextItem(input, false);
        transformer.ApplyTemplates(input, DaxonProcessor.NewSerializer(outputWriter));
        var transformedDocument = new XmlDocument();
        transformedDocument.LoadXml(outputWriter.ToString());
        return transformedDocument;
    }

    /// <summary>
    /// Applies a Schematron stylesheet and converts its SVRL output into a structured report step.
    /// </summary>
    /// <param name="document">The invoice XML document to validate.</param>
    /// <param name="xsltStream">The DAXon-compatible Schematron stylesheet.</param>
    /// <param name="stepId">The KoSIT-style validation step identifier.</param>
    /// <param name="stepName">The human-readable validation step name.</param>
    /// <returns>A structured report containing the parsed SVRL messages.</returns>
    public ValidationReport TransformReport(XmlDocument document, Stream xsltStream, string stepId = "val-sch.1", string stepName = "Schematron validation")
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (xsltStream == null)
        {
            throw new ArgumentNullException(nameof(xsltStream));
        }

        var report = new ValidationReport();
        ValidationReport.AddSchematronStep(Transform(document, xsltStream), stepId, stepName, report);
        return report;
    }
}