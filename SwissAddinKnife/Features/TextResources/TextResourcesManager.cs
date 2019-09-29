using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace SwissAddinKnife.Features.TextResources
{
    public class TextResourcesManager
    {
        XDocument resourcesDocument;


        public IDictionary<string, string> Values { get; set;}

        public TextResourcesManager(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                resourcesDocument = XDocument.Load(fs);
            }
        }


        public void LoadResources()
        {
            Values = new Dictionary<string,string>();
            foreach (var data in resourcesDocument.Root.Elements("data"))
            {
                var key = data.FirstAttribute.Value;
                var value = data.Element("value").Value;
                Values.Add(key, value);
            }
        }

    }
}
