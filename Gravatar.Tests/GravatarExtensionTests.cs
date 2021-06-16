using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gravatar.Tests
{
    [TestClass]
    public class GravatarExtensionTests
    {
        [TestCategory("Gravatar Tests")]
        [TestMethod("Should validate Gravatar extension")]
        [DataRow(null, 200, null, null, false)]
        [DataRow("", 200, null, null, false)]
        [DataRow(" ", 200, null, null, false)]
        [DataRow("a@a", 200, null, null, false)]
        [DataRow("a@andre.com", 200, null, null, false)]
        [DataRow("a@andre.com", 200, GravatarDefaults.NotFound, null, false)]
        [DataRow("andre@balta.io", null, null, null, true)]
        [DataRow("andre@balta.io", 200, null, null, true)]
        [DataRow("andre@balta.io", 200, GravatarDefaults.IdentIcon, null, true)]
        [DataRow("andre@balta.io", 200, GravatarDefaults.Custom, "https://via.placeholder.com/80.png/ddd/999", true)]
        public void ShouldValidateGravatar(string email, int? size, GravatarDefaults? defaultImage, string customDefaultImage, bool status)
        {
            var imageSize = size.HasValue ? size.Value.ToString() : "80";
            var result = $"https://www.gravatar.com/avatar/8d9f6b0ffc9150878f1ae9e3ae9bfb07?s={imageSize}";
            result = defaultImage switch
            {
                GravatarDefaults.NotFound => $"{result}&d=404",
                GravatarDefaults.IdentIcon => $"{result}&d=identicon",
                GravatarDefaults.Custom => $"{result}&d={Uri.EscapeUriString(customDefaultImage)}",
                _ => result
            };

                ;
            Assert.AreEqual((email.ToGravatar(size ?? 80, defaultImage ?? GravatarDefaults.None, customDefaultImage) == result), status);
        }
    }
}
