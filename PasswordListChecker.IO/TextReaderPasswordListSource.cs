using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public class TextReaderPasswordListSource : IPasswordListSource
    {
        private readonly TextReader _textReader;
        private readonly IPasswordListDeserializer _passwordListDeserializer;

        public TextReaderPasswordListSource(TextReader textReader, IPasswordListDeserializer passwordListDeserializer)
        {
            this._textReader = textReader;
            this._passwordListDeserializer = passwordListDeserializer;
        }

        /// <summary>
        /// Fetches the password list asynchronously.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<string>> FetchAsync()
        {
            return await this._passwordListDeserializer.DeserializeAsync(this._textReader);
        }
    }
}
