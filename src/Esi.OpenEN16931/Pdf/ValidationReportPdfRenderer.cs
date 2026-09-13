using System;
using System.IO;
using System.Threading.Tasks;
using Esi.OpenEN16931.Core;
using ModelsValidationResult = Esi.OpenEN16931.Models.ValidationResult;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace Esi.OpenEN16931.PDF;

/// <summary>
/// Renders invoice validation results as standalone PDF reports.
/// </summary>
public static class ValidationReportPdfRenderer
{
    static ValidationReportPdfRenderer()
    {
        if (GlobalFontSettings.FontResolver == null)
        {
            GlobalFontSettings.FontResolver = new SystemFontResolver();
        }
    }

    /// <summary>
    /// Renders a validation result to a PDF byte array.
    /// </summary>
    /// <param name="validationResult">The validation result to render.</param>
    /// <returns>The generated PDF bytes.</returns>
    public static byte[] Render(ValidationResult validationResult)
    {
        if (validationResult == null)
        {
            throw new ArgumentNullException(nameof(validationResult));
        }

        var report = new ValidationReport { IsValid = validationResult.IsValid };
        var step = new ValidationStepReport
        {
            Id = "val-legacy",
            Name = "Legacy validation result",
            IsValid = validationResult.IsValid
        };

        foreach (string message in validationResult.Messages ?? new())
        {
            step.Messages.Add(new ValidationMessage
            {
                Id = $"val-legacy.{step.Messages.Count + 1}",
                Code = "UNSPECIFIC",
                Level = validationResult.IsValid ? "information" : "error",
                Text = message ?? string.Empty
            });
        }

        report.Steps.Add(step);
        return Render(report);
    }

    /// <summary>
    /// Renders a structured KoSIT-style validation report to a PDF byte array.
    /// </summary>
    /// <param name="report">The structured validation report.</param>
    /// <returns>The generated PDF bytes.</returns>
    public static byte[] Render(ValidationReport report)
    {
        if (report == null)
        {
            throw new ArgumentNullException(nameof(report));
        }

        using var output = new MemoryStream();
        using var document = new PdfDocument();
        var titleFont = new XFont("Arial", 18, XFontStyleEx.Bold);
        var bodyFont = new XFont("Arial", 10, XFontStyleEx.Regular);
        var statusFont = new XFont("Arial", 12, XFontStyleEx.Bold);
        var page = document.AddPage();
        var graphics = XGraphics.FromPdfPage(page);
        double y = 42;
        const double margin = 42;
        double width = page.Width - (margin * 2);

        graphics.DrawString("Validation report", titleFont, XBrushes.Black, margin, y);
        y += 28;
        graphics.DrawString(
            report.IsValid ? "Conformance: compliant" : "Conformance: not compliant",
            statusFont,
            report.IsValid ? XBrushes.DarkGreen : XBrushes.DarkRed,
            margin,
            y);
        y += 24;
        graphics.DrawString($"Document type: {report.DocumentType ?? "unknown"}", bodyFont, XBrushes.Black, margin, y);
        y += 28;

        foreach (ValidationStepReport stepReport in report.Steps)
        {
            string summary = $"{stepReport.Name} ({stepReport.Id}): {stepReport.ErrorCount} errors, {stepReport.WarningCount} warnings, {stepReport.InformationCount} information";
            foreach (string line in Wrap(summary, bodyFont, graphics, width))
            {
                if (y > page.Height - margin)
                {
                    graphics.Dispose();
                    page = document.AddPage();
                    graphics = XGraphics.FromPdfPage(page);
                    y = margin;
                }

                graphics.DrawString("- " + line, bodyFont, XBrushes.Black, margin, y);
                y += 15;
            }

            foreach (ValidationMessage message in stepReport.Messages)
            {
                string detail = $"{message.Id} | {message.Code} | {message.Level} | {message.Text}";
                if (!string.IsNullOrWhiteSpace(message.XPathLocation))
                {
                    detail += $" | XPath: {message.XPathLocation}";
                }

                foreach (string line in Wrap(detail, bodyFont, graphics, width - 18))
                {
                    if (y > page.Height - margin)
                    {
                        graphics.Dispose();
                        page = document.AddPage();
                        graphics = XGraphics.FromPdfPage(page);
                        y = margin;
                    }

                    graphics.DrawString("  " + line, bodyFont, XBrushes.Black, margin, y);
                    y += 15;
                }
            }
        }

        if (y > page.Height - margin)
        {
            graphics.Dispose();
            page = document.AddPage();
            graphics = XGraphics.FromPdfPage(page);
            y = margin;
        }

        graphics.DrawString(report.Recommendation, statusFont, report.IsValid ? XBrushes.DarkGreen : XBrushes.DarkRed, margin, y);

        graphics.Dispose();
        document.Save(output, false);
        return output.ToArray();
    }

    /// <summary>
    /// Renders the validation result produced by the model validator to a PDF byte array.
    /// </summary>
    /// <param name="validationResult">The validation result to render.</param>
    /// <returns>The generated PDF bytes.</returns>
    public static byte[] Render(ModelsValidationResult validationResult)
    {
        if (validationResult == null)
        {
            throw new ArgumentNullException(nameof(validationResult));
        }

        return Render(new ValidationResult
        {
            IsValid = validationResult.IsValid,
            Messages = validationResult.Messages
        });
    }

    /// <summary>
    /// Renders a validation result asynchronously to a PDF byte array.
    /// </summary>
    /// <param name="validationResult">The validation result to render.</param>
    /// <returns>The generated PDF bytes.</returns>
    public static Task<byte[]> RenderAsync(ValidationResult validationResult)
    {
        return Task.FromResult(Render(validationResult));
    }

    /// <summary>Renders a structured validation report asynchronously.</summary>
    /// <param name="report">The structured validation report.</param>
    /// <returns>The generated PDF bytes.</returns>
    public static Task<byte[]> RenderAsync(ValidationReport report)
    {
        return Task.FromResult(Render(report));
    }

    /// <summary>
    /// Renders the model validator result asynchronously to a PDF byte array.
    /// </summary>
    /// <param name="validationResult">The validation result to render.</param>
    /// <returns>The generated PDF bytes.</returns>
    public static Task<byte[]> RenderAsync(ModelsValidationResult validationResult)
    {
        return Task.FromResult(Render(validationResult));
    }

    /// <summary>
    /// Writes a validation report to a PDF file.
    /// </summary>
    /// <param name="targetPath">The destination PDF path.</param>
    /// <param name="validationResult">The validation result to render.</param>
    public static void Save(string targetPath, ValidationResult validationResult)
    {
        if (string.IsNullOrWhiteSpace(targetPath))
        {
            throw new ArgumentException("A target path is required.", nameof(targetPath));
        }

        File.WriteAllBytes(targetPath, Render(validationResult));
    }

    /// <summary>Writes a structured validation report to a PDF file.</summary>
    /// <param name="targetPath">The destination PDF path.</param>
    /// <param name="report">The structured validation report.</param>
    public static void Save(string targetPath, ValidationReport report)
    {
        if (string.IsNullOrWhiteSpace(targetPath))
        {
            throw new ArgumentException("A target path is required.", nameof(targetPath));
        }

        File.WriteAllBytes(targetPath, Render(report));
    }

    /// <summary>
    /// Writes the model validator result to a PDF file.
    /// </summary>
    /// <param name="targetPath">The destination PDF path.</param>
    /// <param name="validationResult">The validation result to render.</param>
    public static void Save(string targetPath, ModelsValidationResult validationResult)
    {
        if (string.IsNullOrWhiteSpace(targetPath))
        {
            throw new ArgumentException("A target path is required.", nameof(targetPath));
        }

        File.WriteAllBytes(targetPath, Render(validationResult));
    }

    private static string[] Wrap(string value, XFont font, XGraphics graphics, double width)
    {
        if (string.IsNullOrEmpty(value))
        {
            return new[] { string.Empty };
        }

        var lines = new System.Collections.Generic.List<string>();
        string current = string.Empty;
        foreach (string word in value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
        {
            string candidate = string.IsNullOrEmpty(current) ? word : current + " " + word;
            if (graphics.MeasureString(candidate, font).Width > width && !string.IsNullOrEmpty(current))
            {
                lines.Add(current);
                current = word;
            }
            else
            {
                current = candidate;
            }
        }

        if (!string.IsNullOrEmpty(current))
        {
            lines.Add(current);
        }

        return lines.ToArray();
    }
}