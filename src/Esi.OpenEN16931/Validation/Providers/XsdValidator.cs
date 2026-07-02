using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;
using System.Xml.Schema;

namespace Esi.OpenEN16931.Validation.Providers;

/// <summary>
/// Implementation of XSD validation logic for XRechnung.
/// </summary>
public class XsdValidator : IXsdValidator
{
    private readonly ISchemaProvider _schemaProvider;

    public XsdValidator(ISchemaProvider schemaProvider)
    {
        _schemaProvider = schemaProvider;
    }

    /// <summary>
    /// Validates the provided XML document against the configured XSDs.
    /// </summary>
    /// <param name="document">The XML document to validate.</param>
    /// <param name="errors">The list of validation errors, if any.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public bool Validate(XmlDocument document, out string[]? errors)
    {
        errors = null;
        try
        {
            // We need to determine which schemas to use based on the document content or context.
            // For now, we'll assume a default or check the document's root.
            // This logic will be refined as we identify specific conformance/syntax mappings.
            
            // Example: get schemas for a specific conformance/syntax
            var schemas = _schemaProvider.GetSchemas(XRechnungConformance.CIUS, Syntax.UBL);

            if (schemas == null || schemas.Count == 0)
            {
                errors = new[] { "No schemas found for the specified conformance and syntax." };
                return false;
            }

            var schemaSet = new XmlSchemaSet();
            foreach (var schemaEntry in schemas)
            {
                using var stream = schemaEntry.Value;
                schemaSet.Add(null, XmlReader.Create(stream));
            }
            schemaSet.Compile();

            document.Schemas.Add(schemaSet);
            var validationErrors = new List<string>();
            document.Validate((sender, args) =>
            {
                validationErrors.Add(args.Message);
            });

            errors = validationErrors.Count > 0 ? validationErrors.ToArray() : null;

            return errors == null;
        }
        catch (XmlSchemaValidationException ex)
        {
            errors = new[] { $"Validation failed due to an internal error: {ex.Message}" };
            return false;
        }
    }
}