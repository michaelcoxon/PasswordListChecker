using System;
using Xunit;

namespace PasswordListChecker.UnitTests
{
    public class PasswordListTests
    {
        [Fact]
        public void PasswordList_DefaultConstructor_Test()
        {
            var passwordList = new PasswordList();
        }

        [Fact]
        public void PasswordList_EnumerableConstructor_Test()
        {
            var passwords = new[]
            {
                "password",
                "123456",
                "qwerty"
            };

            var passwordList = new PasswordList(passwords);

            foreach (var password in passwords)
            {
                Assert.Contains(password, passwordList);
            }
        }
    }
}
