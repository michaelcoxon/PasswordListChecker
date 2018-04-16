using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public sealed class StaticPasswordListSource : IPasswordListSource
    {
        private readonly PasswordList _passwords;

        public StaticPasswordListSource(IEnumerable<string> passwords)
        {
            this._passwords = new PasswordList(passwords);
        }

        public Task<IEnumerable<string>> FetchAsync()
        {
            return Task.FromResult<IEnumerable<string>>(this._passwords);
        }
    }
}
