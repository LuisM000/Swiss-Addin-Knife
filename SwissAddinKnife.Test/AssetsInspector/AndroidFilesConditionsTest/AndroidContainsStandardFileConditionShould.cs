using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.AndroidFilesConditionsTest
{
    [TestFixture]
    public class AndroidContainsStandardFileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsStandardAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");


            AndroidContainsStandardFileCondition condition = new AndroidContainsStandardFileCondition(assetAndroid.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfStandardAssetIsNull()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns((string)null);


            AndroidContainsStandardFileCondition condition = new AndroidContainsStandardFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfStandardAssetIsEmpty()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns(string.Empty);


            AndroidContainsStandardFileCondition condition = new AndroidContainsStandardFileCondition(assetAndroid.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}
