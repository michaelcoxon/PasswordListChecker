using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public class NewLineSeparatedPasswordListDeserializer : IPasswordListDeserializer
    {
        public async Task<PasswordList> DeserializeAsync(TextReader textReader)
        {
            var passwordList = new PasswordList();

            string line;
            while ((line = await textReader.ReadLineAsync()) != null)
            {
                passwordList.Add(line.Trim());
            }

            return passwordList;
        }
    }
}
