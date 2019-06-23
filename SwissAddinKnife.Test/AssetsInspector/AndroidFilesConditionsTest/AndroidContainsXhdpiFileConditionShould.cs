using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.AndroidFilesConditionsTest
{
    [TestFixture]
    public class AndroidContainsXhdpiFileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsXhdpiAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("dummy");


            AndroidContainsXhdpiFileCondition condition = new AndroidContainsXhdpiFileCondition(assetAndroid.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfXhdpiAssetIsNull()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns((string)null);


            AndroidContainsXhdpiFileCondition condition = new AndroidContainsXhdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfXhdpiAssetIsEmpty()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns(string.Empty);


            AndroidContainsXhdpiFileCondition condition = new AndroidContainsXhdpiFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}
