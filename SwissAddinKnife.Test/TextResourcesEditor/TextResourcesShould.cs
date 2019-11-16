using System.IO;
using NUnit.Framework;
using SwissAddinKnife.Features.TextResources.Services;

namespace SwissAddinKnife.Test.TextResourcesEditor
{
    [TestFixture]
    public class TextResourcesEditorShould
    {
        string textResourcesWithTwoKeysPath;

       [OneTimeSetUp]
        public void CreateTextResources()
        {
            string originResourcesWithTwoKeysPath = string.Concat(Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar, "TextResourcesEditor/TextResourcesSample/TextResources.resx");
            textResourcesWithTwoKeysPath = string.Concat(Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar, System.Guid.NewGuid(), ".resx");
            File.Copy(originResourcesWithTwoKeysPath, textResourcesWithTwoKeysPath, true);
        }

        [Test]
        public void LoadAllResources()
        {
            TextResources textResourcesManager = new TextResources(textResourcesWithTwoKeysPath);
            textResourcesManager.LoadResources();

            Assert.AreEqual(2, textResourcesManager.Values.Count);
        }

        [Test]
        public void LoadKeys()
        {
            TextResources textResourcesManager = new TextResources(textResourcesWithTwoKeysPath);
            textResourcesManager.LoadResources();

            Assert.IsTrue(textResourcesManager.Values.ContainsKey("key1"));
            Assert.IsTrue(textResourcesManager.Values.ContainsKey("key2"));
        }

        [Test]
        public void LoadValues()
        {
            TextResources textResourcesManager = new TextResources(textResourcesWithTwoKeysPath);
            textResourcesManager.LoadResources();

            Assert.AreEqual("value 1", textResourcesManager.Values["key1"]);
            Assert.AreEqual("value 2", textResourcesManager.Values["key2"]);
        }

    }
}
