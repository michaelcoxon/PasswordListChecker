using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public class NewLineSeparatedPasswordListDeserializer : IPasswordListDeserializer
    {
        public Task<IEnumerable<string>> DeserializeAsync(TextReader textReader)
        {
            return DeserializeAsync2(textReader);
        }

        private static async Task<IEnumerable<string>> DeserializeAsync1(TextReader textReader)
        {
            var passwordList = new PasswordList();

            string line;
            while ((line = await textReader.ReadLineAsync()) != null)
            {
                passwordList.Add(line.Trim());
            }

            return passwordList;
        }

        private static async Task<IEnumerable<string>> DeserializeAsync2(TextReader textReader)
        {
            var strList = await textReader.ReadToEndAsync();
            var result = strList.Split(Environment.NewLine);
            return result;
        }
    }
}
