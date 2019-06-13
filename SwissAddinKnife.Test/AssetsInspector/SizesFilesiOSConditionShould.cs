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
    public class SizesFilesiOSConditionShould
    {
        [Test]
        public void ReturnsTrueIfX2AssetIsDoubleAndX3IsTriple()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns("dummyX2");
            assetiOS.SetupGet(a => a.X3FilePath).Returns("dummyX3");


            Mock<IImageService> imageService = new Mock<IImageService>();
            imageService.Setup(c => c.GetImageSize("dummy")).Returns(new Size(5, 8));
            imageService.Setup(c => c.GetImageSize("dummyX2")).Returns(new Size(10, 16));
            imageService.Setup(c => c.GetImageSize("dummyX3")).Returns(new Size(15, 24));

            SizesFilesiOSCondition sizesFilesCondition = new SizesFilesiOSCondition(assetiOS.Object, imageService.Object);

            Assert.IsTrue(sizesFilesCondition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsTrueIfX2AssetIsDoubleAndX3IsTripleWithOnePixelOfMargin()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns("dummyX2");
            assetiOS.SetupGet(a => a.X3FilePath).Returns("dummyX3");


            Mock<IImageService> imageService = new Mock<IImageService>();
            imageService.Setup(c => c.GetImageSize("dummy")).Returns(new Size(5, 8));
            imageService.Setup(c => c.GetImageSize("dummyX2")).Returns(new Size(10 + 1, 16 + 1));
            imageService.Setup(c => c.GetImageSize("dummyX3")).Returns(new Size(15 + 1, 24 + 1));

            SizesFilesiOSCondition sizesFilesCondition = new SizesFilesiOSCondition(assetiOS.Object, imageService.Object);

            Assert.IsTrue(sizesFilesCondition.Verify().All(c => c.IsFulfilled));
        }


        [TestCase(1, 1, 323, 344, 3, 3)]
        [TestCase(1, 1, 2, 2, 433, 455)]
        [TestCase(59, 64, 2, 2, 3, 3)]
        public void ReturnsFalseIfX2AssetIsNotDoubleOrX3IsNotTriple(int width, int height,
                                                                    int widthX2, int heightX2,
                                                                    int widthX3, int heightX3)
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns("dummyX2");
            assetiOS.SetupGet(a => a.X3FilePath).Returns("dummyX3");


            Mock<IImageService> imageService = new Mock<IImageService>();
            imageService.Setup(c => c.GetImageSize("dummy")).Returns(new Size(width, height));
            imageService.Setup(c => c.GetImageSize("dummyX2")).Returns(new Size(widthX2, heightX2));
            imageService.Setup(c => c.GetImageSize("dummyX3")).Returns(new Size(widthX3, heightX3));

            SizesFilesiOSCondition sizesFilesCondition = new SizesFilesiOSCondition(assetiOS.Object, imageService.Object);

            Assert.IsFalse(sizesFilesCondition.Verify().All(c => c.IsFulfilled));
        }
    }
}
