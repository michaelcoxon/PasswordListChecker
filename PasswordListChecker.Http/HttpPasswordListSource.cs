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
        private readonly StreamPasswordListSourceFactory _streamPasswordListSourceFactory;

        public HttpPasswordListSource(Uri uri, StreamPasswordListSourceFactory streamPasswordListSourceFactory)
        {
            this._uri = uri;
            this._streamPasswordListSourceFactory = streamPasswordListSourceFactory;
        }

        public HttpPasswordListSource(string uri, StreamPasswordListSourceFactory streamPasswordListSourceFactory) : this(new Uri(uri), streamPasswordListSourceFactory) { }

        /// <summary>
        /// Fetches the password list asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> FetchAsync()
        {
            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(this._uri))
            {
                var passwordListSource = this._streamPasswordListSourceFactory(stream);
                return await passwordListSource.FetchAsync();
            }
        }
    }
}
