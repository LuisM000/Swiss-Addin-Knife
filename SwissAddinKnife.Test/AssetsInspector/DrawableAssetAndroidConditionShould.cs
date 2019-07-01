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
        public void ReturnsFalseIfContainsAllAssets()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("ldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("hdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("xhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("xxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("xxxhdpi");


            DrawableAssetAndroidCondition drawableAssetAndroidCondition = new DrawableAssetAndroidCondition(assetAndroid.Object);

            Assert.IsFalse(drawableAssetAndroidCondition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsTrueIfNotContainsDrawableAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns((string)null);
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("ldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("hdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("xhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("xxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("xxxhdpi");


            DrawableAssetAndroidCondition drawableAssetAndroidCondition = new DrawableAssetAndroidCondition(assetAndroid.Object);

            Assert.IsTrue(drawableAssetAndroidCondition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsTrueIfNotContainsAnyAsset()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("ldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns((string)null);
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("hdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("xhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("xxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("xxxhdpi");


            DrawableAssetAndroidCondition drawableAssetAndroidCondition = new DrawableAssetAndroidCondition(assetAndroid.Object);

            Assert.IsTrue(drawableAssetAndroidCondition.Verify().All(c => c.IsFulfilled));
        }
    }
}
