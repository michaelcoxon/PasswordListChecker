using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    /// <summary>
    /// Builds a password list from multiple sources ensuring there are no duplicates
    /// </summary>
    public sealed class PasswordListBuilder
    {
        private readonly List<IPasswordListSource> _passwordListSources;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordListBuilder"/> class.
        /// </summary>
        public PasswordListBuilder()
        {
            this._passwordListSources = new List<IPasswordListSource>();
        }

        /// <summary>
        /// Adds a password list source to the build to be fetched.
        /// </summary>
        /// <param name="passwordListSource">The password list source.</param>
        /// <returns></returns>
        public PasswordListBuilder AddSource(IPasswordListSource passwordListSource)
        {
            this._passwordListSources.Add(passwordListSource);
            return this;
        }

        /// <summary>
        /// Fetches all password lists and returns a single password list asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<PasswordList> BuildAsync()
        {
            var passwordList = new List<string>();

            foreach (var source in this._passwordListSources)
            {
                passwordList.AddRange(await source.FetchAsync());
            }

            return new PasswordList(passwordList);
        }
    }
}
