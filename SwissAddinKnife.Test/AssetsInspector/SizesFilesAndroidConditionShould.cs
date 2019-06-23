using System;
using System.Drawing;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;
using SwissAddinKnife.Features.AssetsInspector.Services;

namespace SwissAddinKnife.Test.AssetsInspector
{
    [TestFixture]
    public class SizesFilesAndroidConditionShould
    {
        [Test]
        public void ReturnsTrueIfAllAssetsContainsCorrectResolutions()
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("ldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("hdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("xhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("xxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("xxxhdpi");


            Mock<IImageService> imageService = new Mock<IImageService>();
            imageService.Setup(c => c.GetImageSize("dummy")).Returns(new Size(12, 4));
            imageService.Setup(c => c.GetImageSize("ldpi")).Returns(new Size(9, 3));
            imageService.Setup(c => c.GetImageSize("mdpi")).Returns(new Size(12, 4));
            imageService.Setup(c => c.GetImageSize("hdpi")).Returns(new Size(18, 6));
            imageService.Setup(c => c.GetImageSize("xhdpi")).Returns(new Size(24, 8));
            imageService.Setup(c => c.GetImageSize("xxhdpi")).Returns(new Size(36, 12));
            imageService.Setup(c => c.GetImageSize("xxxhdpi")).Returns(new Size(48, 16));

            SizesFilesAndroidCondition sizesFilesCondition = new SizesFilesAndroidCondition(assetAndroid.Object, 0, imageService.Object);

            Assert.IsTrue(sizesFilesCondition.Verify().All(c => c.IsFulfilled));
        }


        [TestCase(1)]
        [TestCase(3)]
        public void ReturnsTrueIfAllAssetsContainsCorrectResolutionsWithSpecifiedPixelsOfMarginExceptMdpi(int margin)
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("ldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("hdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("xhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("xxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("xxxhdpi");


            Mock<IImageService> imageService = new Mock<IImageService>();
            imageService.Setup(c => c.GetImageSize("dummy")).Returns(new Size(12, 4));
            imageService.Setup(c => c.GetImageSize("ldpi")).Returns(new Size(9 + margin, 3 + margin));
            imageService.Setup(c => c.GetImageSize("mdpi")).Returns(new Size(12, 4));
            imageService.Setup(c => c.GetImageSize("hdpi")).Returns(new Size(18 + margin, 6 + margin));
            imageService.Setup(c => c.GetImageSize("xhdpi")).Returns(new Size(24 + margin, 8 + margin));
            imageService.Setup(c => c.GetImageSize("xxhdpi")).Returns(new Size(36 + margin, 12 + margin));
            imageService.Setup(c => c.GetImageSize("xxxhdpi")).Returns(new Size(48 + margin, 16 + margin));

            SizesFilesAndroidCondition sizesFilesCondition = new SizesFilesAndroidCondition(assetAndroid.Object, margin, imageService.Object);

            Assert.IsTrue(sizesFilesCondition.Verify().All(c => c.IsFulfilled));
        }


        [TestCase(1)]
        [TestCase(3)]
        public void ReturnsFalseIfAllAssetsContainsCorrectResolutionsWithSpecifiedPixelsOfMarginIncludeMdpi(int margin)
        {
            Mock<AssetAndroid> assetAndroid = new Mock<AssetAndroid>("dummy");
            assetAndroid.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetAndroid.SetupGet(a => a.LdpiFilePath).Returns("ldpi");
            assetAndroid.SetupGet(a => a.MdpiFilePath).Returns("mdpi");
            assetAndroid.SetupGet(a => a.HdpiFilePath).Returns("hdpi");
            assetAndroid.SetupGet(a => a.XhdpiFilePath).Returns("xhdpi");
            assetAndroid.SetupGet(a => a.XxhdpiFilePath).Returns("xxhdpi");
            assetAndroid.SetupGet(a => a.XxxhdpiFilePath).Returns("xxxhdpi");


            Mock<IImageService> imageService = new Mock<IImageService>();
            imageService.Setup(c => c.GetImageSize("dummy")).Returns(new Size(12, 4));
            imageService.Setup(c => c.GetImageSize("ldpi")).Returns(new Size(9 + margin, 3 + margin));
            imageService.Setup(c => c.GetImageSize("mdpi")).Returns(new Size(12 + margin, 4 + margin));
            imageService.Setup(c => c.GetImageSize("hdpi")).Returns(new Size(18 + margin, 6 + margin));
            imageService.Setup(c => c.GetImageSize("xhdpi")).Returns(new Size(24 + margin, 8 + margin));
            imageService.Setup(c => c.GetImageSize("xxhdpi")).Returns(new Size(36 + margin, 12 + margin));
            imageService.Setup(c => c.GetImageSize("xxxhdpi")).Returns(new Size(48 + margin, 16 + margin));

            SizesFilesAndroidCondition sizesFilesCondition = new SizesFilesAndroidCondition(assetAndroid.Object, 1, imageService.Object);

            Assert.IsFalse(sizesFilesCondition.Verify().All(c => c.IsFulfilled));
        }

      
    }
}
