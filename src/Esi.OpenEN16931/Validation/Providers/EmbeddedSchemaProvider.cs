using System.Collections.Generic;
using System.IO;
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;

namespace Esi.OpenEN16931.Validation.Providers;

public class EmbeddedSchemaProvider : ISchemaProvider
{
    public IDictionary<string, Stream> GetSchemas(XRechnungConformance conformance, Syntax syntax)
    {
        // TODO: Return embedded resource streams based on conformance/syntax.
        return new Dictionary<string, Stream>();
    }
}
