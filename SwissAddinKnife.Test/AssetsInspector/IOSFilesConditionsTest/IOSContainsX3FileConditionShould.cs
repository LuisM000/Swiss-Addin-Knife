using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.IOSFilesConditionsTest
{
    [TestFixture]
    public class IOSContainsX3FileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsX3Asset()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.X3FilePath).Returns("dummy");

            IOSContainsX3FileCondition condition = new IOSContainsX3FileCondition(assetiOS.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfX3AssetIsNull()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.X3FilePath).Returns((string)null);

            IOSContainsX3FileCondition condition = new IOSContainsX3FileCondition(assetiOS.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfX3AssetIsEmpty()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.X3FilePath).Returns(string.Empty);

            IOSContainsX3FileCondition condition = new IOSContainsX3FileCondition(assetiOS.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}

