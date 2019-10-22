using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SwissAddinKnife.Features.TextResources.Services
{
    public class TextResources
    {
        XDocument resourcesDocument;
        private readonly string filePath;

        public IDictionary<string, string> Values { get; set; }
        public string Name { get; }

        public TextResources(string filePath)
        {
            this.filePath = filePath;
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
            resourcesDocument.Save(filePath);
        }

        public string GetValue(string key)
        {
            Values.TryGetValue(key, out string value);
            return value;
        }

        public void UpdateKey(string oldKey, string newKey)
        {
            if (Values.ContainsKey(oldKey))
            {
                var value = Values[oldKey];
                Values.Remove(oldKey);
                Values.Add(newKey, value);

                resourcesDocument.Root.Elements("data").
                    FirstOrDefault(d => d.HasAttributes && d.Attribute("name")?.Value == oldKey).
                    SetAttributeValue("name", newKey);
                resourcesDocument.Save(filePath);
            }
        }


        public void SaveValue(string key, string value)
        {
            if(Values.ContainsKey(key))
            {
                UpdateValue(key, value);
            }
            else
            {
                CreateValue(key, value);
            }
        }

        private void UpdateValue(string key, string value)
        {
            Values[key] = value;
            foreach (var data in resourcesDocument.Root.Elements("data"))
            {
                if (data.FirstAttribute.Value == key)
                {
                    data.Element("value").SetValue(value);
                    resourcesDocument.Save(filePath);
                    return;
                }
            }
        }

        private void CreateValue(string key, string value)
        {
            Values[key] = value;
            var xData = new XElement("data");
            xData.SetAttributeValue("name", key);
            xData.SetAttributeValue(XNamespace.Xml + "space", "preserve");
            xData.Add(new XElement("value", value));

            resourcesDocument.Root.Add(xData);
            resourcesDocument.Save(filePath);
        }
    }

}
