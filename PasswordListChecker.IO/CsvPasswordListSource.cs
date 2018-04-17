using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public sealed class CsvPasswordListSource : IPasswordListSource
    {
        public const string DEFAULT_COLUMN_NAME = "password";

        private static readonly Encoding _defaultEncoding;

        private readonly Stream _stream;
        private readonly string _columnName;
        private readonly IPasswordListDeserializer _passwordListDeserializer;
        private readonly int _columnNumber;
        private readonly Encoding _encoding;
        private readonly bool _detectEncodingFromByteOrderMarks;
        private readonly bool _leaveOpen;

        static CsvPasswordListSource()
        {
            var streamReader = new StreamReader(new MemoryStream());
            _defaultEncoding = streamReader.CurrentEncoding;
        }

        public CsvPasswordListSource(Stream stream, string columnName = null, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, bool leaveOpen = true)
        {
            this._stream = stream;
            this._columnName = columnName ?? DEFAULT_COLUMN_NAME;
            this._encoding = encoding ?? _defaultEncoding;
            this._detectEncodingFromByteOrderMarks = detectEncodingFromByteOrderMarks;
            this._leaveOpen = leaveOpen;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CsvPasswordListSource"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="columnNumber">The zero-indexed column number.</param>
        public CsvPasswordListSource(Stream stream, int columnNumber, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, bool leaveOpen = true)
        {
            this._stream = stream;
            this._columnNumber = columnNumber;
            this._encoding = encoding ?? _defaultEncoding;
            this._detectEncodingFromByteOrderMarks = detectEncodingFromByteOrderMarks;
            this._leaveOpen = leaveOpen;
        }

        /// <summary>
        /// Fetches the password list asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<PasswordList> FetchAsync()
        {
            using (var streamReader = new StreamReader(this._stream, this._encoding, this._detectEncodingFromByteOrderMarks, 4096, this._leaveOpen))
            {
                if (string.IsNullOrEmpty(this._columnName))
                {
                    return await FetchByColumnNumberAsync(streamReader, this._columnNumber);
                }
                else
                {
                    var headers = await streamReader.ReadLineAsync();
                    var columnNumber = headers.Split(new[] { ',' })
                        .ToList()
                        .FindIndex(header => header.Equals(this._columnName, StringComparison.CurrentCultureIgnoreCase));

                    return await FetchByColumnNumberAsync(streamReader, columnNumber);
                }
            }
        }

        private static async Task<PasswordList> FetchByColumnNumberAsync(TextReader textReader, int columnNumber)
        {
            var passwordList = new PasswordList();

            string line;
            while ((line = await textReader.ReadLineAsync()) != null)
            {
                var items = line.Split(new[] { ',' });

                if (items.Length <= columnNumber)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnNumber));
                }

                passwordList.Add(items[columnNumber].Trim('"', '\''));
            }

            return passwordList;
        }
    }
}
