using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Esi.OpenEN16931.Core;

/// <summary>
/// Structured validation report data modelled after the KoSIT VARL report.
/// </summary>
public sealed class ValidationReport
{
    /// <summary>Initializes a report with the current UTC timestamp.</summary>
    public ValidationReport()
    {
        Timestamp = DateTimeOffset.UtcNow;
    }

    /// <summary>Gets or sets the overall validation status.</summary>
    public bool IsValid { get; set; }

    /// <summary>Gets or sets the document reference.</summary>
    public string? DocumentReference { get; set; }

    /// <summary>Gets or sets the recognized validation scenario.</summary>
    public string? DocumentType { get; set; }

    /// <summary>Gets or sets the validation timestamp.</summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>Gets or sets the validator name.</summary>
    public string EngineName { get; set; } = "Esi.OpenEN16931";

    /// <summary>Gets the validation steps in execution order.</summary>
    public List<ValidationStepReport> Steps { get; } = new();

    /// <summary>Gets the final recommendation.</summary>
    public string Recommendation => IsValid
        ? "The document is recommended for acceptance and further processing."
        : "The document is recommended for rejection.";

    /// <summary>Gets all messages from all validation steps.</summary>
    public IEnumerable<ValidationMessage> Messages => Steps.SelectMany(step => step.Messages);

    /// <summary>Creates a report step from a Schematron SVRL result.</summary>
    /// <param name="svrl">The SVRL result document.</param>
    /// <param name="stepId">The KoSIT-style validation step identifier.</param>
    /// <param name="stepName">The human-readable validation step name.</param>
    /// <param name="report">The report receiving the parsed step.</param>
    public static void AddSchematronStep(XmlDocument svrl, string stepId, string stepName, ValidationReport report)
    {
        if (svrl == null)
        {
            throw new ArgumentNullException(nameof(svrl));
        }

        if (string.IsNullOrWhiteSpace(stepId))
        {
            throw new ArgumentException("A validation step identifier is required.", nameof(stepId));
        }

        if (string.IsNullOrWhiteSpace(stepName))
        {
            throw new ArgumentException("A validation step name is required.", nameof(stepName));
        }

        if (report == null)
        {
            throw new ArgumentNullException(nameof(report));
        }

        const string svrlNamespace = "http://purl.oclc.org/dsdl/svrl";
        XmlNamespaceManager namespaces = new(svrl.NameTable);
        namespaces.AddNamespace("svrl", svrlNamespace);

        var step = new ValidationStepReport { Id = stepId, Name = stepName };
        foreach (XmlElement element in svrl.SelectNodes("/svrl:schematron-output/*[self::svrl:failed-assert or self::svrl:successful-report]", namespaces)!)
        {
            string level = element.GetAttribute("flag");
            if (string.IsNullOrWhiteSpace(level))
            {
                level = element.GetAttribute("role");
            }

            step.Messages.Add(new ValidationMessage
            {
                Id = $"{stepId}.{step.Messages.Count + 1}",
                Code = element.GetAttribute("id") is { Length: > 0 } code ? code : "UNSPECIFIC",
                Level = NormalizeLevel(level, element.LocalName == "successful-report"),
                Text = element.SelectSingleNode("svrl:text", namespaces)?.InnerText?.Trim() ?? string.Empty,
                XPathLocation = element.GetAttribute("location")
            });
        }

        step.IsValid = !step.Messages.Any(message => message.Level is "error" or "warning");
        report.Steps.Add(step);
        report.IsValid = report.Steps.All(validationStep => validationStep.IsValid);
    }

    private static string NormalizeLevel(string level, bool successfulReport)
    {
        return level.ToLowerInvariant() switch
        {
            "information" or "info" => "information",
            "warning" or "warn" => "warning",
            "error" or "fatal" => "error",
            _ => successfulReport ? "information" : "error"
        };
    }
}

/// <summary>One logical validation step in a validation report.</summary>
public sealed class ValidationStepReport
{
    /// <summary>Gets or sets the KoSIT-compatible step identifier.</summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>Gets or sets the human-readable step name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets whether this step passed.</summary>
    public bool IsValid { get; set; }

    /// <summary>Gets the messages emitted by this step.</summary>
    public List<ValidationMessage> Messages { get; } = new();

    /// <summary>Gets the number of error messages.</summary>
    public int ErrorCount => Messages.Count(message => message.Level == "error");

    /// <summary>Gets the number of warning messages.</summary>
    public int WarningCount => Messages.Count(message => message.Level == "warning");

    /// <summary>Gets the number of information messages.</summary>
    public int InformationCount => Messages.Count(message => message.Level == "information");
}

/// <summary>A single validation message with its rule and source location.</summary>
public sealed class ValidationMessage
{
    /// <summary>Gets or sets the message identifier.</summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>Gets or sets the rule code.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the KoSIT-compatible severity level.</summary>
    public string Level { get; set; } = "error";

    /// <summary>Gets or sets the human-readable message text.</summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>Gets or sets the XPath location, when supplied by the validator.</summary>
    public string XPathLocation { get; set; } = string.Empty;

    /// <summary>Gets or sets the source line number, when available.</summary>
    public int? LineNumber { get; set; }

    /// <summary>Gets or sets the source column number, when available.</summary>
    public int? ColumnNumber { get; set; }
}