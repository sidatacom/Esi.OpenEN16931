using System.Collections.Generic;
using System.IO;
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;

namespace Esi.OpenEN16931.Validation.Providers;

/// <summary>
/// Provides embedded XRechnung schemas for validation purposes.
/// </summary>
public class EmbeddedSchemaProvider : ISchemaProvider
{
    /// <summary>
    /// Gets the embedded XRechnung schemas based on the specified conformance and syntax.
    /// </summary>
    /// <param name="conformance"></param>
    /// <param name="syntax"></param>
    /// <returns></returns>
    public IDictionary<string, Stream> GetSchemas(XRechnungConformance conformance, Syntax syntax)
    {
        // TODO: Return embedded resource streams based on conformance/syntax.
        return new Dictionary<string, Stream>();
    }
}
