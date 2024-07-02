using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public delegate IPasswordListSource StreamPasswordListSourceFactory(Stream stream);

    public class StreamPasswordListSource : IPasswordListSource
    {
        private static readonly Encoding _defaultEncoding;

        private readonly Stream _stream;
        private readonly IPasswordListDeserializer _passwordListDeserializer;
        private readonly Encoding _encoding;
        private readonly bool _detectEncodingFromByteOrderMarks;
        private readonly bool _leaveOpen;

        static StreamPasswordListSource()
        {
            var streamReader = new StreamReader(new MemoryStream());
            _defaultEncoding = streamReader.CurrentEncoding;
        }

        public StreamPasswordListSource(Stream stream, IPasswordListDeserializer passwordListDeserializer, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, bool leaveOpen = true)
        {
            this._stream = stream;
            this._passwordListDeserializer = passwordListDeserializer;
            this._encoding = encoding ?? _defaultEncoding;
            this._detectEncodingFromByteOrderMarks = detectEncodingFromByteOrderMarks;
            this._leaveOpen = leaveOpen;
        }

        /// <summary>
        /// Fetches the password list asynchronously.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<string>> FetchAsync()
        {
            using var streamReader = new StreamReader(this._stream, this._encoding, this._detectEncodingFromByteOrderMarks, 4096, this._leaveOpen);
            var passwordListSource = new TextReaderPasswordListSource(streamReader, this._passwordListDeserializer);
            return await passwordListSource.FetchAsync();
        }
    }
}
