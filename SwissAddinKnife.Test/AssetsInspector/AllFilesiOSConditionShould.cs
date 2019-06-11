using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;

namespace SwissAddinKnife.Test.AssetsInspector
{
    [TestFixture]
    public class AllFilesiOSConditionShould
    {
        [Test]
        public void ReturnsTrueIfContainsAllAssets()
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns("dummy");
            assetiOS.SetupGet(a => a.X2FilePath).Returns("dummyX2");
            assetiOS.SetupGet(a => a.X3FilePath).Returns("dummyX3");

            AllFilesiOSCondition allFilesCondition = new AllFilesiOSCondition(assetiOS.Object);

            Assert.IsTrue(allFilesCondition.Verify().All(c => c.IsFulfilled));
        }
       
        [TestCase("dummy",null,null)]
        [TestCase("dummy","dummyX2",null)]
        [TestCase(null,"dummyX2",null)]
        [TestCase(null,null,"dummyX3")]
        [TestCase("dummy",null,"dummyX3")]
        [TestCase(null,"dummyX2","dummyX3")]
        [TestCase("dummy","","")]
        [TestCase("dummy","dummyX2","")]
        [TestCase("","dummyX2","")]
        [TestCase("","","dummyX3")]
        [TestCase("dummy","","dummyX3")]
        [TestCase("","dummyX2","dummyX3")]
        public void ReturnsFalseIfNotContainsAllAsset(string standard, string x2, string x3)
        {
            Mock<AssetiOS> assetiOS = new Mock<AssetiOS>("dummy");
            assetiOS.SetupGet(a => a.StandardFilePath).Returns(standard);
            assetiOS.SetupGet(a => a.X2FilePath).Returns(x2);
            assetiOS.SetupGet(a => a.X3FilePath).Returns(x3);

            AllFilesiOSCondition allFilesCondition = new AllFilesiOSCondition(assetiOS.Object);

            Assert.IsFalse(allFilesCondition.Verify().All(c => c.IsFulfilled));
        }
    }
}
