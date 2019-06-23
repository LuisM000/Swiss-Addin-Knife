using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.AndroidFilesConditionsTest
{
    [TestFixture]
    public class AndroidContainsHdpiFileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsHdpiAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("dummy");


            AndroidContainsHdpiFileCondition condition = new AndroidContainsHdpiFileCondition(assetAndroid.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfHdpiAssetIsNull()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns((string)null);


            AndroidContainsHdpiFileCondition condition = new AndroidContainsHdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfHdpiAssetIsEmpty()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns(string.Empty);


            AndroidContainsHdpiFileCondition condition = new AndroidContainsHdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}
