using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public sealed class PasswordListBuilder
    {
        private readonly List<IPasswordListSource> _passwordListSources;

        public PasswordListBuilder()
        {
            this._passwordListSources = new List<IPasswordListSource>();
        }

        public PasswordListBuilder AddSource(IPasswordListSource passwordListSource)
        {
            this._passwordListSources.Add(passwordListSource);
            return this;
        }

        public async Task<PasswordList> BuildAsync()
        {
            var passwordList = new PasswordList();

            foreach (var source in this._passwordListSources)
            {
                passwordList.AddRange(await source.FetchAsync());
            }

            return passwordList;
        }
    }
}
