using System.Collections.Generic;
using System.IO;
using Esi.OpenEN16931.Models;

namespace Esi.OpenEN16931.Validation.Interfaces;

public interface ISchemaProvider
{
    /// <summary>
    /// Returns a mapping of schema resource name to a readable <see cref="Stream"/> for the given conformance and syntax.
    /// </summary>
    IDictionary<string, Stream> GetSchemas(XRechnungConformance conformance, Syntax syntax);
}
