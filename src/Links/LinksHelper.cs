using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Links
{
    public class LinksHelper
    {
        public static IList<string> ExtractLinks(string body)
        {
            var html = new HtmlDocument();
            try
            {
                html.LoadHtml(body);
                var links = from node in html.DocumentNode.Descendants("a")
                            let href = node.Attributes["href"]
                            where href != null && !string.IsNullOrEmpty(href.Value)
                            select href;
                return links.Select(li => li.Value).ToList();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
