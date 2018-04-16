using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public sealed class HttpPasswordListSource : IPasswordListSource
    {
        private readonly Uri _uri;
        private readonly IPasswordListDeserializer _passwordListDeserializer;

        public HttpPasswordListSource(Uri uri, IPasswordListDeserializer passwordListDeserializer)
        {
            this._uri = uri;
            this._passwordListDeserializer = passwordListDeserializer;
        }

        public HttpPasswordListSource(string uri, IPasswordListDeserializer passwordListDeserializer) : this(new Uri(uri), passwordListDeserializer) { }

        public async Task<IEnumerable<string>> FetchAsync()
        {
            using (var client = new HttpClient())
            using (var sr = new StreamReader(await client.GetStreamAsync(this._uri)))
            {
                return await this._passwordListDeserializer.DeserializeAsync(sr);
            }
        }
    }
}
