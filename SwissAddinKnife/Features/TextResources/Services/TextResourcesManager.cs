﻿using System;
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

        public void SaveValueInMainResource(string key, string value)
        {
            TextResources.OrderBy(t => t.Name.Length).FirstOrDefault().SaveValue(key, value);
        }

        public void UpdateKey(string oldKey, string newKey)
        {
            foreach (var resources in TextResources)
            {
                resources.UpdateKey(oldKey, newKey);
            }
        }

        public string CreateAvailableKey()
        {
            var allkeys = this.GetAllKeys();
            string increment = "";
            string key = "new_resources";
            int i = 1;
            while(allkeys.Contains(key + increment))
            {
                increment = "_" + i;
                i++;
            }

            return key + increment;
        }

      
    }
}
