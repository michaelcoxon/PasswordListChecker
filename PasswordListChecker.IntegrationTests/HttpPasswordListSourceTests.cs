using System;
using System.Threading.Tasks;
using Xunit;

namespace PasswordListChecker.IntegrationTests
{
    public class HttpPasswordListSourceTests
    {
        [Fact]
        public async Task Test1()
        {
            var uri = "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/darkweb2017-top1000.txt";

            var deserializer = new NewLineSeparatedPasswordListDeserializer();
            var source = new HttpPasswordListSource(uri, deserializer);
            var builder = new PasswordListBuilder();

            builder.AddSource(source);

            var passwordList = await builder.BuildAsync();

            Assert.Contains("qwerty", passwordList);
        }
    }
}
