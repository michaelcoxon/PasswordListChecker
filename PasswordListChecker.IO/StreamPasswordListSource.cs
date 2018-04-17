using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public class StreamPasswordListSource : IPasswordListSource
    {
        private readonly Stream _stream;
        private readonly IPasswordListDeserializer _passwordListDeserializer;

        public StreamPasswordListSource(Stream stream, IPasswordListDeserializer passwordListDeserializer)
        {
            this._stream = stream;
            this._passwordListDeserializer = passwordListDeserializer;
        }

        public StreamPasswordListSource(string path, IPasswordListDeserializer passwordListDeserializer)
            : this(File.Open(path, FileMode.Open), passwordListDeserializer)
        { }

        public virtual async Task<IEnumerable<string>> FetchAsync()
        {
            using (var sr = new StreamReader(this._stream))
            {
                return await this._passwordListDeserializer.DeserializeAsync(sr);
            }
        }
    }
}
