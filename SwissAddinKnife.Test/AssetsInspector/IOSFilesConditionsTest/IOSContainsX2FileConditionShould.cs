using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions;

namespace SwissAddinKnife.Test.AssetsInspector.IOSFilesConditionsTest
{
    [TestFixture]
    public class IOSContainsX2FileConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsX2Asset()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns("dummy");

            IOSContainsX2FileCondition condition = new IOSContainsX2FileCondition(assetiOS.Object);

            Assert.IsTrue(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfX2AssetIsNull()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns((string)null);

            IOSContainsX2FileCondition condition = new IOSContainsX2FileCondition(assetiOS.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }

        [Test]
        public void ReturnsFalseIfX2AssetIsEmpty()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns(string.Empty);

            IOSContainsX2FileCondition condition = new IOSContainsX2FileCondition(assetiOS.Object);

            Assert.IsFalse(condition.Verify().All(c => c.IsFulfilled));
        }
    }
}

