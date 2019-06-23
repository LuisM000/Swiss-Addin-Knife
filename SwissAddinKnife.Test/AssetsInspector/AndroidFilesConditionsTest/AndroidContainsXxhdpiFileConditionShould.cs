using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.AndroidFilesConditionsTest
{
    [TestFixture]
    public class AndroidContainsXxhdpiFileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsXxhdpiAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("dummy");


            AndroidContainsXxhdpiFileCondition condition = new AndroidContainsXxhdpiFileCondition(assetAndroid.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfXhdpiAssetIsNull()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns((string)null);


            AndroidContainsXxhdpiFileCondition condition = new AndroidContainsXxhdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfXhdpiAssetIsEmpty()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns(string.Empty);


            AndroidContainsXxhdpiFileCondition condition = new AndroidContainsXxhdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}
