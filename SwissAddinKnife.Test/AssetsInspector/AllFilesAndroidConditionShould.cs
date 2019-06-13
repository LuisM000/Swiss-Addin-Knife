using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;

namespace SwissAddinKnife.Test.AssetsInspector
{
    [TestFixture]
    public class AllFilesAndroidConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsAllAssets()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("dummyldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("dummymdpi");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("dummyhdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("dummyxhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("dummyxxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("dummyxxxhdpi");

            AllFilesAndroidCondition allFilesCondition = new AllFilesAndroidCondition(assetAndroid.Object);

            Assert.IsTrue(allFilesCondition.Verify().All(c => c.IsFulfilled));
        }

        [TestCase("dummy", "ldpi", "mdpi", "hdpi", "xhdpi", "xxhdpi", null)]
        [TestCase("dummy", "ldpi", "mdpi", "hdpi", "xhdpi", null, "xxxhdpi")]
        [TestCase("dummy", "ldpi", "mdpi", "hdpi", null, "xxhdpi", "xxxhdpi")]
        [TestCase("dummy", "ldpi", "mdpi", null, "xhdpi", "xxhdpi", "xxxhdpi")]
        [TestCase("dummy", "ldpi", null, "hdpi", "xhdpi", "xxhdpi", "xxxhdpi")]
        [TestCase("dummy", null, "mdpi", "hdpi", "xhdpi", "xxhdpi", "xxxhdpi")]
        [TestCase(null, "ldpi", "mdpi", "hdpi", "xhdpi", "xxhdpi", "xxxhdpi")]
        public void ReturnsFalseIfNotContainsAllAsset(string standard, string ldpi, string mdpi, string hdpi,
                                                        string xhdpi, string xxhdpi, string xxxhdpi)
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns(standard);
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns(ldpi);
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns(mdpi);
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns(hdpi);
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns(xhdpi);
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns(xxhdpi);
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns(xxxhdpi);

            AllFilesAndroidCondition allFilesCondition = new AllFilesAndroidCondition(assetAndroid.Object);

            Assert.IsFalse(allFilesCondition.Verify().All(c => c.IsFulfilled));
        }
    }
}
