using System.Xml;
using System.IO;

namespace Esi.OpenEN16931.Validation.Interfaces;

public interface IXsltEngine
{
    /// <summary>
    /// Applies the given XSLT stream to the input XML and returns the transformed XML document.
    /// </summary>
    XmlDocument Transform(XmlDocument input, Stream xsltStream);
}
