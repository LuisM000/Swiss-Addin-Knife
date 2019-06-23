using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.AndroidFilesConditionsTest
{
    [TestFixture]
    public class AndroidContainsLdpiFileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsLdpiAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("dummy");


            AndroidContainsLdpiFileCondition condition = new AndroidContainsLdpiFileCondition(assetAndroid.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfLdpiAssetIsNull()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns((string)null);


            AndroidContainsLdpiFileCondition condition = new AndroidContainsLdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfLdpiAssetIsEmpty()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns(string.Empty);


            AndroidContainsLdpiFileCondition condition = new AndroidContainsLdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}
