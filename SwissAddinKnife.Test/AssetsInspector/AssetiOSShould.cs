using System;
using System.IO;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;

namespace SwissAddinKnife.Test.AssetsInspector
{
    [TestFixture]
    public class AssetiOSShould
    {
        [Test]
        public void ThrowExceptionIfIsCreatedWithNullFilePath()
        {
            string filePath = null;

            Assert.Throws<ArgumentException>(() =>
            {
                AssetiOS assetiOS = new AssetiOS(filePath);
            });
        }

        [Test]
        public void ThrowExceptionIfIsCreatedWithEmptyFilePath()
        {
            string filePath = string.Empty;

            Assert.Throws<ArgumentException>(() =>
            {
                AssetiOS assetiOS = new AssetiOS(filePath);
            });
        }


        [Test]
        public void CreateAssetWithStandardFile()
        {
            string filePath = this.GivenAStandardFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.That(assetiOS.StandardFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetiOS.X2FilePath);
            Assert.IsNull(assetiOS.X3FilePath);
        }

        [Test]
        public void CreateAssetWithX2File()
        {
            string filePath = this.GivenAX2dFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.That(assetiOS.X2FilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetiOS.StandardFilePath);
            Assert.IsNull(assetiOS.X3FilePath);
        }

        [Test]
        public void CreateAssetWithX3File()
        {
            string filePath = this.GivenAX3dFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.That(assetiOS.X3FilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetiOS.StandardFilePath);
            Assert.IsNull(assetiOS.X2FilePath);
        }


        [Test]
        public void CreateIdentifierFromStandardFilePath()
        {
            string filePath = this.GivenAStandardFilePath("dummyFileName");

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.That(assetiOS.Identifier, Is.EqualTo("dummyFileName"));           
        }

        [Test]
        public void CreateIdentifierFromX2FilePath()
        {
            string filePath = this.GivenAX2dFilePath("dummyFileName");

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.That(assetiOS.Identifier, Is.EqualTo("dummyFileName"));
        }

        [Test]
        public void CreateIdentifierFromX3FilePath()
        {
            string filePath = this.GivenAX3dFilePath("dummyFileName");

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.That(assetiOS.Identifier, Is.EqualTo("dummyFileName"));
        }


        [Test]
        public void CanAddAnEqualFilePath()
        {
            string filePath = this.GivenAStandardFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.IsTrue(assetiOS.CanBeAdded(filePath));
        }

        [Test]
        public void CanNotAddDifferentFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string otherFilePath = this.GivenAStandardFilePath("otherFile");

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.IsFalse(assetiOS.CanBeAdded(otherFilePath));
        }


        [Test]
        public void CanAddAnotherSizeOfFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string x2FilePath = this.GivenAX2dFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            Assert.IsTrue(assetiOS.CanBeAdded(x2FilePath));
        }

        [Test]
        public void AddAnEqualFilePath()
        {
            string filePath = this.GivenAStandardFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            var result = assetiOS.Add(filePath);
            Assert.IsTrue(result);

            Assert.That(assetiOS.StandardFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetiOS.X2FilePath);
            Assert.IsNull(assetiOS.X3FilePath);
        }

        [Test]
        public void NotAddDifferentFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string otherFilePath = this.GivenAStandardFilePath("otherFile");

            AssetiOS assetiOS = new AssetiOS(filePath);

            var result = assetiOS.Add(otherFilePath);
            Assert.IsFalse(result);

            Assert.That(assetiOS.StandardFilePath, Is.EqualTo(filePath));
            Assert.IsNull(assetiOS.X2FilePath);
            Assert.IsNull(assetiOS.X3FilePath);
        }

        [Test]
        public void AddAnotherSizeOfFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string x2FilePath = this.GivenAX2dFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            var result = assetiOS.Add(x2FilePath);
            Assert.IsTrue(result);

            Assert.That(assetiOS.StandardFilePath, Is.EqualTo(filePath));
            Assert.That(assetiOS.X2FilePath, Is.EqualTo(x2FilePath));
            Assert.IsNull(assetiOS.X3FilePath);
        }

        [Test]
        public void AddMultipleSizesOfFilePath()
        {
            string filePath = this.GivenAStandardFilePath();
            string x2FilePath = this.GivenAX2dFilePath();
            string x3FilePath = this.GivenAX3dFilePath();

            AssetiOS assetiOS = new AssetiOS(filePath);

            assetiOS.Add(x2FilePath);
            var result = assetiOS.Add(x3FilePath);
            Assert.IsTrue(result);

            Assert.That(assetiOS.StandardFilePath, Is.EqualTo(filePath));
            Assert.That(assetiOS.X2FilePath, Is.EqualTo(x2FilePath));
            Assert.That(assetiOS.X3FilePath, Is.EqualTo(x3FilePath));
        }

        private string GivenAStandardFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("dummyFolder", fileNameWithoutExtension ?? "dummyFileName" + ".extension");
        }

        private string GivenAX2dFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("dummyFolder", fileNameWithoutExtension ?? "dummyFileName@2x" + ".extension");
        }

        private string GivenAX3dFilePath(string fileNameWithoutExtension = null)
        {
            return Path.Combine("dummyFolder", fileNameWithoutExtension ?? "dummyFileName@3x" + ".extension");
        }
    }
}
