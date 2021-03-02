using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gravatar.Tests
{
    [TestClass]
    public class GravatarExtensionTests
    {
        [TestCategory("Gravatar Tests")]
        [TestMethod("Should validate Gravatar extension")]
        [DataRow(null, 200, false)]
        [DataRow("", 200, false)]
        [DataRow(" ", 200, false)]
        [DataRow("a@a", 200, false)]
        [DataRow("a@andre.com", 200, false)]
        [DataRow("andre@balta.io", null, true)]
        [DataRow("andre@balta.io", 200, true)]
        public void ShouldValidateGravatar(string email, int? size, bool status)
        {
            var imageSize = size.HasValue ? size.Value.ToString() : "80";
            var result = $"https://www.gravatar.com/avatar/8d9f6b0ffc9150878f1ae9e3ae9bfb07?s={imageSize}";
            Assert.AreEqual((email.ToGravatar(size ?? 80) == result), status);
        }
    }
}
