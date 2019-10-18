using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace SwissAddinKnife.Features.TextResources.Services
{
    public class TextResources
    {
        XDocument resourcesDocument;

        public IDictionary<string, string> Values { get; set; }
        public string Name { get; }

        public TextResources(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                resourcesDocument = XDocument.Load(fs);
            }
            Name = Path.GetFileNameWithoutExtension(filePath);
        }


        public void LoadResources()
        {
            Values = new Dictionary<string,string>();
            foreach (var data in resourcesDocument.Root.Elements("data"))
            {
                var key = data.FirstAttribute.Value;
                var value = data.Element("value").Value;
                Values[key] = value;
            }
        }

        public void SaveResources()
        {
            foreach (var data in resourcesDocument.Root.Elements("data"))
            {
                var key = data.FirstAttribute.Value;
                data.Element("value").SetValue(Values[key]);
            }
            resourcesDocument.Save(Name + ".resx");//ToDo:rewiew filepath..
        }

        public string GetValueOrEmpty(string key)
        {
            if (Values.TryGetValue(key, out string value))
            {
                return value;
            }
            return string.Empty;
        }

        public void SetValue(string key, string value)
        {
            Values[key] = value;
            SaveResources();
        }
    }

}
