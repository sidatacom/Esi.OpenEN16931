using System.Xml;

namespace Esi.OpenEN16931.Validation.Interfaces;

public interface IXsdValidator
{
    /// <summary>
    /// Validates the provided XML document against the configured XSDs. Returns true when valid.
    /// </summary>
    bool Validate(XmlDocument document, out string[]? errors);
}
