using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;

namespace SwissAddinKnife.Test.AssetsInspector
{
    [TestFixture]
    public class DrawableAssetAndroidConditionShould
    {

        [Test]
        public void ReturnsFalseIfContainsDrawableAndMdpiAssets()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");

            DrawableAssetAndroidCondition drawableAssetAndroidCondition = new DrawableAssetAndroidCondition(assetAndroid.Object);

            Assert.IsFalse(drawableAssetAndroidCondition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsTrueIfNotContainsDrawableAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns((string)null);
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");            

            DrawableAssetAndroidCondition drawableAssetAndroidCondition = new DrawableAssetAndroidCondition(assetAndroid.Object);

            Assert.IsTrue(drawableAssetAndroidCondition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsTrueIfNotContainsMdpiAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns((string)null);

            DrawableAssetAndroidCondition drawableAssetAndroidCondition = new DrawableAssetAndroidCondition(assetAndroid.Object);

            Assert.IsTrue(drawableAssetAndroidCondition.Verify().All(c => c.IsFulfilled));
        }
    }
}
