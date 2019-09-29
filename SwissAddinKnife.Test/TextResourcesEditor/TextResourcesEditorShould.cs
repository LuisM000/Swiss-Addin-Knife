using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using SwissAddinKnife.Features.TextResources;

namespace SwissAddinKnife.Test.TextResourcesEditor
{
    [TestFixture]
    public class TextResourcesEditorShould
    {
        string textResourcesPath = string.Concat(Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar, "TextResourcesEditor/TextResourcesSample/TextResources.resx");


        [Test]
        public void LoadAllResources()
        {
            TextResourcesManager textResourcesManager = new TextResourcesManager(textResourcesPath);
            textResourcesManager.LoadResources();

            Assert.AreEqual(2, textResourcesManager.Values.Count);
        }

        [Test]
        public void LoadKeys()
        {
            TextResourcesManager textResourcesManager = new TextResourcesManager(textResourcesPath);
            textResourcesManager.LoadResources();

            Assert.IsTrue(textResourcesManager.Values.ContainsKey("key1"));
            Assert.IsTrue(textResourcesManager.Values.ContainsKey("key2"));
        }

        [Test]
        public void LoadValues()
        {
            TextResourcesManager textResourcesManager = new TextResourcesManager(textResourcesPath);
            textResourcesManager.LoadResources();

            Assert.AreEqual("value 1", textResourcesManager.Values["key1"]);
            Assert.AreEqual("value 2", textResourcesManager.Values["key2"]);
        }

    }
}
