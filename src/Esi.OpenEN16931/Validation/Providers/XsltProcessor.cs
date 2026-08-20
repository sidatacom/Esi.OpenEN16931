using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
#if !NET10_0_OR_GREATER
using System.Xml.Xsl;
#endif
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;
using System;
#if NET10_0_OR_GREATER
using OutSmart.DAXon.Api;
#endif

namespace Esi.OpenEN16931.Validation.Providers;

/// <summary>
/// Implementation of XSLT transformation logic for XRechnung.
/// </summary>
public class XsltProcessor : IXsltProcessor
{
#if NET10_0_OR_GREATER
    private static readonly Processor DaxonProcessor = new Processor();
#endif
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

#if NET10_0_OR_GREATER
        var executable = DaxonProcessor.NewXsltCompiler().Compile(xsltStream, "urn:esi:stylesheet");
        using var inputWriter = new StringWriter();
        document.Save(inputWriter);
        using var inputReader = new StringReader(inputWriter.ToString());
        var input = DaxonProcessor.NewDocumentBuilder().Build(inputReader, "urn:esi:input");
        using var outputWriter = new StringWriter();
        var transformer = executable.Load30();
        transformer.ApplyTemplates(input, DaxonProcessor.NewSerializer(outputWriter));
        var transformedDocument = new XmlDocument();
        transformedDocument.LoadXml(outputWriter.ToString());
        return transformedDocument;
#else
        var transformer = new XslCompiledTransform();
        using var reader = XmlReader.Create(xsltStream);
        transformer.Load(reader);

        var transformedDocument = new XmlDocument();
        using var outputWriter = new StringWriter();
        using var xmlWriter = XmlWriter.Create(outputWriter, transformer.OutputSettings);
        transformer.Transform(document, xmlWriter);
        xmlWriter.Flush();
        transformedDocument.LoadXml(outputWriter.ToString());
        return transformedDocument;
#endif
    }
}