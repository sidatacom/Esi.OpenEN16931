using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;
using System;

namespace Esi.OpenEN16931.Validation.Providers;

/// <summary>
/// Implementation of XSLT transformation logic for XRechnung.
/// </summary>
public class XsltEngine : IXsltEngine
{
    private readonly ISchemaProvider _schemaProvider;

    public XsltEngine(ISchemaProvider schemaProvider)
    {
        _schemaProvider = schemaProvider;
    }

    /// <summary>
    /// Transforms the provided XML document using the configured XSLTs.
    /// </summary>
    /// <param name="document">The XML document to transform.</param>
    /// <param name="xsltStream">The XSLT stream to use.</param>
    /// <returns>The transformed XML document, or the original if no transformation occurred.</returns>
    public XmlDocument Transform(XmlDocument document, Stream xsltStream)
    {
        try
        {
            // Logic to determine the correct XSLT based on conformance/syntax
            var xslts = _schemaProvider.GetSchemas(XRechnungConformance.Extension, Syntax.UBL);

            if (xslts == null || xslts.Count == 0)
            {
                return document;
            }

            // If a specific stream is provided, use it. Otherwise, use the first one from the schema provider.
            Stream streamToUse = xsltStream ?? xslts.Values.First();

            using var stream = streamToUse;
            var transformer = new XslCompiledTransform();
            using var reader = XmlReader.Create(stream);
            transformer.Load(reader);

            var transformedDocument = new XmlDocument();
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, transformer.OutputSettings);
            transformer.Transform(document, xmlWriter);
            xmlWriter.Flush();
            transformedDocument.LoadXml(stringWriter.ToString());
            return transformedDocument;
        }
        catch (Exception ex)
        {
            // Log error (implementation of logging to be added)
            return document;
        }
    }
}