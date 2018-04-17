using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public sealed class EnumerablePasswordListSource : IPasswordListSource
    {
        private readonly IEnumerable<string> _passwords;

        public EnumerablePasswordListSource(IEnumerable<string> passwords)
        {
            this._passwords = passwords;
        }

        public Task<IEnumerable<string>> FetchAsync()
        {
            return Task.FromResult<IEnumerable<string>>(new PasswordList(this._passwords));
        }
    }
}
