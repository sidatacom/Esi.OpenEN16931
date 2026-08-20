using System.Collections.Generic;
using System.IO;
using Esi.OpenEN16931.Models;
using Esi.OpenEN16931.Validation.Interfaces;

namespace Esi.OpenEN16931.Validation.Providers;

/// <summary>
/// Default implementation of <see cref="ISchemaProvider"/>.
/// </summary>
public class DefaultSchemaProvider : ISchemaProvider
{
    /// <summary>
    /// Returns the schemas registered for the specified conformance and syntax.
    /// </summary>
    /// <param name="conformance">The XRechnung conformance profile.</param>
    /// <param name="syntax">The invoice syntax.</param>
    /// <returns>An empty schema collection until external schema discovery is configured.</returns>
    public IDictionary<string, Stream> GetSchemas(XRechnungConformance conformance, Syntax syntax)
    {
        return new Dictionary<string, Stream>();
    }
}
