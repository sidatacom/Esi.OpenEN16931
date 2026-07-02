using System.Collections.Generic;
using System.IO;
using Esi.OpenEN16931.Validation.Interfaces;
using Esi.OpenEN16931.Models;

namespace Esi.OpenEN16931.Validation.Providers;

public class ZipSchemaProvider : ISchemaProvider
{
    private readonly Stream _zipStream;

    public ZipSchemaProvider(Stream zipStream)
    {
        _zipStream = zipStream;
    }

    public IDictionary<string, Stream> GetSchemas(XRechnungConformance conformance, Syntax syntax)
    {
        // TODO: Extract required files from KoSIT ZIP (scenarios.xml) and return streams.
        return new Dictionary<string, Stream>();
    }
}
