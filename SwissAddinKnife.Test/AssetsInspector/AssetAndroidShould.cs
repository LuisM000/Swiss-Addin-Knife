using System;
using System.IO;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;

namespace SwissAddinKnife.Test.AssetsInspector
{
    [TestFixture]
    public class AssetAndroidShould
    {
        [Test]
        public void ThrowExceptionIfIsCreatedWithNullFilePath()
        {
            string filePath = null;

            Assert.Throws<ArgumentException>(() =>
            {
                AssetAndroid assetAndroid = new AssetAndroid(filePath);
            });
        }

        [Test]
        public void ThrowExceptionIfIsCreatedWithEmptyFilePath()
        {
            string filePath = string.Empty;

            Assert.Throws<ArgumentException>(() =>
            {
                AssetAndroid assetAndroid = new AssetAndroid(filePath);
            });
        }

        [Test]
        public void CreateAssetWithStandardFile()
        {
            string filePath = this.GivenAStandardFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.That(assetAndroid.StandardFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void CreateAssetWithLdpiFile()
        {
            string filePath = this.GivenALdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsNull(assetAndroid.StandardFilePath);
            Assert.That(assetAndroid.LdpiFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void CreateAssetWithMdpiFile()
        {
            string filePath = this.GivenAMdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsNull(assetAndroid.StandardFilePath);
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.That(assetAndroid.MdpiFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void CreateAssetWithHdpiFile()
        {
            string filePath = this.GivenAHdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsNull(assetAndroid.StandardFilePath);
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.That(assetAndroid.HdpiFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void CreateAssetWithXhdpiFile()
        {
            string filePath = this.GivenAXhdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsNull(assetAndroid.StandardFilePath);
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.That(assetAndroid.XhdpiFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void CreateAssetWithXxhdpiFile()
        {
            string filePath = this.GivenAXxhdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsNull(assetAndroid.StandardFilePath);
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.That(assetAndroid.XxhdpiFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void CreateAssetWithXxxhdpiFile()
        {
            string filePath = this.GivenAXxxhdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsNull(assetAndroid.StandardFilePath);
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.That(assetAndroid.XxxhdpiFilePath, Is.EqualTo(filePath));
        }
        
        [TestCase("resources/drawable/dummyfile.extension")]
        [TestCase("resources/drawable-ldpi/dummyfile.extension")]
        [TestCase("resources/drawable-mdpi/dummyfile.extension")]
        [TestCase("resources/drawable-hdpi/dummyfile.extension")]
        [TestCase("resources/drawable-xhdpi/dummyfile.extension")]
        [TestCase("resources/drawable-xxhdpi/dummyfile.extension")]
        [TestCase("resources/drawable-xxxhdpi/dummyfile.extension")]
        public void CreateIdentifierFromFilePath(string filePath)
        {
            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.That(assetAndroid.Identifier, Is.EqualTo("dummyfile"));
        }

        [Test]
        public void CanAddAnEqualFilePath()
        {
            string filePath = this.GivenAStandardFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsTrue(assetAndroid.CanBeAdded(filePath));
        }

        [Test]
        public void CanNotAddDifferentFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string otherFilePath = this.GivenAStandardFilePath("otherFile");

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsFalse(assetAndroid.CanBeAdded(otherFilePath));
        }

        [Test]
        public void CanAddAnotherSizeOfFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string x2FilePath = this.GivenALdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            Assert.IsTrue(assetAndroid.CanBeAdded(x2FilePath));
        }

        [Test]
        public void AddAnEqualFilePath()
        {
            string filePath = this.GivenAStandardFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            var result = assetAndroid.Add(filePath);
            Assert.IsTrue(result);

            Assert.That(assetAndroid.StandardFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void NotAddDifferentFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string otherFilePath = this.GivenAStandardFilePath("otherFile");

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            var result = assetAndroid.Add(otherFilePath);
            Assert.IsFalse(result);

            Assert.That(assetAndroid.StandardFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetAndroid.LdpiFilePath);
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void AddAnotherSizeOfFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string ldpiFilePath = this.GivenALdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            var result = assetAndroid.Add(ldpiFilePath);
            Assert.IsTrue(result);

            Assert.That(assetAndroid.StandardFilePath, Is.EqualTo(filePath));
            Assert.That(assetAndroid.LdpiFilePath, Is.EqualTo(ldpiFilePath));
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.IsNull(assetAndroid.XxhdpiFilePath);
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        [Test]
        public void AddMultipleSizesOfFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string ldpiFilePath = this.GivenALdpiFilePath();
            string xxHdpiFilePath = this.GivenAXxhdpiFilePath();

            AssetAndroid assetAndroid = new AssetAndroid(filePath);

            assetAndroid.Add(ldpiFilePath);
            var result = assetAndroid.Add(xxHdpiFilePath);
            Assert.IsTrue(result);
             
            Assert.That(assetAndroid.StandardFilePath, Is.EqualTo(filePath));
            Assert.That(assetAndroid.LdpiFilePath, Is.EqualTo(ldpiFilePath));
            Assert.IsNull(assetAndroid.MdpiFilePath);
            Assert.IsNull(assetAndroid.HdpiFilePath);
            Assert.IsNull(assetAndroid.XhdpiFilePath);
            Assert.That(assetAndroid.XxhdpiFilePath, Is.EqualTo(xxHdpiFilePath));
            Assert.IsNull(assetAndroid.XxxhdpiFilePath);
        }

        private string GivenAStandardFilePath(string fileNameWithoutExtension = null)
        { 
            return Path.Combine("resources/drawable", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
        private string GivenALdpiFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("resources/drawable-ldpi", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
        private string GivenAMdpiFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("resources/drawable-mdpi", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
        private string GivenAHdpiFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("resources/drawable-hdpi", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
        private string GivenAXhdpiFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("resources/drawable-xhdpi", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
        private string GivenAXxhdpiFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("resources/drawable-xxhdpi", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
        private string GivenAXxxhdpiFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("resources/drawable-xxxhdpi", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }
    }
}
