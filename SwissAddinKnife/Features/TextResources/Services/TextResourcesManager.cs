using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwissAddinKnife.Features.TextResources.Services
{
    public class TextResourcesManager
    {
        readonly List<TextResources> textResources = new List<TextResources>();
        public IReadOnlyList<TextResources> TextResources
        {
            get { return textResources; }
        }


        public TextResourcesManager(string folderPath)
        {
            var textResourcesFiles = Utils.FileUtils.GetTextResourcesFiles(folderPath);
            foreach (var textResourcesFile in textResourcesFiles.OrderBy(t=>t.Length))
            {
                textResources.Add(new TextResources(textResourcesFile));
            }
        }

        public void LoadResources()
        {
            textResources.ForEach(t => t.LoadResources());
        }

        public IEnumerable<string> GetTextResourcesNames()
        {
            return TextResources.Select(t => t.Name);
        }

        public IEnumerable<string> GetAllKeys()
        {
            return TextResources.SelectMany(t => t.Values.Keys).Distinct();
        }

        public string GetValue(string resourcesName, string key)
        {
            return TextResources.FirstOrDefault(f => f.Name == resourcesName).GetValue(key);
        } 
    }
}
