using System.Collections.Generic;
using System.Xml.Linq;

namespace FMG.NRT.Components.Mvc
{
    public interface IMvcSiteMapParser
    {
        IEnumerable<MvcSiteMapNode> GetNodeTree(XElement siteMap);
    }
}
