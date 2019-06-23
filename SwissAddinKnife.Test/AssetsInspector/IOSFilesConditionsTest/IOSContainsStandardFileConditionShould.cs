using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.IOSFilesConditionsTest
{
    [TestFixture]
    public class IOSContainsStandardFileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsStandardAsset()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns("dummy");

            IOSContainsStandardFileCondition condition = new IOSContainsStandardFileCondition(assetiOS.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfStandardAssetIsNull()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns((string)null);

            IOSContainsStandardFileCondition condition = new IOSContainsStandardFileCondition(assetiOS.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfStandardAssetIsEmpty()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns(string.Empty);

            IOSContainsStandardFileCondition condition = new IOSContainsStandardFileCondition(assetiOS.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}

