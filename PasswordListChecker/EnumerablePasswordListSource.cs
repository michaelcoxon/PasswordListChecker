using System.Collections.Generic;
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
            return Task.FromResult(this._passwords);
        }
    }
}
