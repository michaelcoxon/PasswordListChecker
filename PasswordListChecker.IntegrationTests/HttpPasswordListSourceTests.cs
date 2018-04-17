using System;
using System.Threading.Tasks;
using Xunit;

namespace PasswordListChecker.IntegrationTests
{
    public class HttpPasswordListSourceTests
    {
        [Fact]
        public async Task HttpPasswordListSource_NewLineSeparatedPasswordList_Test()
        {
            var uri = "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/darkweb2017-top1000.txt";

            var deserializer = new NewLineSeparatedPasswordListDeserializer();
            var source = new HttpPasswordListSource(uri, (stream) => new StreamPasswordListSource(stream, deserializer));
            var builder = new PasswordListBuilder();

            builder.AddSource(source);

            var passwordList = await builder.BuildAsync();

            Assert.Contains("qwerty", passwordList);
        }

        [Fact]
        public async Task HttpPasswordListSource_CsvPasswordListSource_DefaultColumnName_Test()
        {
            var uri = "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/Default-Credentials/default-passwords.csv";

            var source = new HttpPasswordListSource(uri, (stream) => new CsvPasswordListSource(stream));
            var builder = new PasswordListBuilder();

            builder.AddSource(source);

            var passwordList = await builder.BuildAsync();

            Assert.Contains("pbxk1064", passwordList);
        }

        [Fact]
        public async Task HttpPasswordListSource_CsvPasswordListSource_ColumnName_Test()
        {
            var uri = "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/Default-Credentials/default-passwords.csv";

            var source = new HttpPasswordListSource(uri, (stream) => new CsvPasswordListSource(stream, "PASSWORD"));
            var builder = new PasswordListBuilder();

            builder.AddSource(source);

            var passwordList = await builder.BuildAsync();

            Assert.Contains("pbxk1064", passwordList);
        }

        [Fact]
        public async Task HttpPasswordListSource_CsvPasswordListSource_ColumnNumber_Test()
        {
            var uri = "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/Default-Credentials/default-passwords.csv";

            var source = new HttpPasswordListSource(uri, (stream) => new CsvPasswordListSource(stream, 2));
            var builder = new PasswordListBuilder();

            builder.AddSource(source);

            var passwordList = await builder.BuildAsync();

            Assert.Contains("pbxk1064", passwordList);
        }
    }
}
