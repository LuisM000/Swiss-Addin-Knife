using System;
using System.IO;
using MonoDevelop.Core;
using SwissAddinKnife.Features.JsonToClass.Model;

namespace SwissAddinKnife.Features.JsonToClass.Services
{
    public static class SettingsService
    {
        static readonly FilePath settingsFilePath = new FilePath(string.Concat(UserProfile.Current.ConfigDir,
                                                            "/", "MonoDevelop.JsonToClass.config"));

        public static QuicktypeProperties LoadQuicktypeProperties()
        {
            EnsureSettingsFileExists();
            var settings = File.ReadAllText(settingsFilePath);
            QuicktypeProperties quicktypeProperties = null;
            if (!string.IsNullOrEmpty(settings))
                quicktypeProperties = Newtonsoft.Json.JsonConvert.DeserializeObject<QuicktypeProperties>(settings);
            return quicktypeProperties;
        }

        public static void SaveQuicktypeProperties(QuicktypeProperties quicktypeProperties)
        {
            EnsureSettingsFileExists();
            var settings = Newtonsoft.Json.JsonConvert.SerializeObject(quicktypeProperties);
            File.WriteAllText(settingsFilePath, settings);
        }


        private static void EnsureSettingsFileExists()
        {
            if (!File.Exists(settingsFilePath))
                File.Create(settingsFilePath);
        }
    }
}
