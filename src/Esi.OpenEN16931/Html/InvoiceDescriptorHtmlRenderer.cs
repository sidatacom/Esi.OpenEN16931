/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Esi.OpenEN16931.Core;
using ModelsValidationResult = Esi.OpenEN16931.Models.ValidationResult;
using Scriban;

namespace Esi.OpenEN16931.Html
{
    public class InvoiceDescriptorHtmlRenderer
    {
        /// <summary>
        /// Renders an invoice as HTML.
        /// </summary>
        /// <param name="invoice">The invoice to render.</param>
        /// <returns>The rendered HTML document.</returns>
        public static async Task<string> RenderAsync(InvoiceDescriptor invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice));
            }

            string templateContent = _LoadEmbeddedResource("Simple.scriban");
            var template = Template.Parse(templateContent);
            var model = new
            {
                Invoice = invoice
            };

            string result = await template.RenderAsync(model, memberRenamer: member => member.Name);
                return result;
            } // !RenderAsync()

        /// <summary>
        /// Renders a validation result as an HTML report.
        /// </summary>
        /// <param name="validationResult">The validation result to render.</param>
        /// <returns>The rendered HTML report.</returns>
        public static async Task<string> RenderValidationReportAsync(ValidationResult validationResult)
        {
            if (validationResult == null)
            {
                throw new ArgumentNullException(nameof(validationResult));
            }

            return await RenderValidationReportAsync(CreateLegacyReport(validationResult));
        }

        /// <summary>
        /// Renders a structured KoSIT-style validation report as HTML.
        /// </summary>
        /// <param name="report">The structured validation report.</param>
        /// <returns>The rendered HTML report.</returns>
        public static async Task<string> RenderValidationReportAsync(ValidationReport report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            string templateContent = _LoadEmbeddedResource("ValidationReport.scriban");
            var template = Template.Parse(templateContent);
            var model = new
            {
                Report = report,
                ErrorCount = report.Messages.Count(message => message.Level == "error"),
                WarningCount = report.Messages.Count(message => message.Level == "warning"),
                InformationCount = report.Messages.Count(message => message.Level == "information")
            };

            return await template.RenderAsync(model, memberRenamer: member => member.Name);
        }

        /// <summary>
        /// Renders the validation result produced by the model validator as an HTML report.
        /// </summary>
        /// <param name="validationResult">The validation result to render.</param>
        /// <returns>The rendered HTML report.</returns>
        public static Task<string> RenderValidationReportAsync(ModelsValidationResult validationResult)
        {
            if (validationResult == null)
            {
                throw new ArgumentNullException(nameof(validationResult));
            }

            return RenderValidationReportAsync(CreateLegacyReport(new ValidationResult
            {
                IsValid = validationResult.IsValid,
                Messages = validationResult.Messages
            }));
        }

        private static ValidationReport CreateLegacyReport(ValidationResult validationResult)
        {
            var report = new ValidationReport { IsValid = validationResult.IsValid };
            var step = new ValidationStepReport
            {
                Id = "val-legacy",
                Name = "Legacy validation result",
                IsValid = validationResult.IsValid
            };

            foreach (string message in validationResult.Messages ?? new List<string>())
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
            return report;
        }


        private static string _LoadEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetManifestResourceNames()
                                       .FirstOrDefault(r => r.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));

            if (resourcePath == null)
            {
                throw new FileNotFoundException($"The embedded resource '{resourceName}' was not found.");
            }

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"The embedded resource '{resourceName}' was not found.");
                }

                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        } // !_LoadEmbeddedResource()
    }
}
