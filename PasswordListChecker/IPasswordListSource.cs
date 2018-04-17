using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    /// <summary>
    /// Interface for a password list source
    /// </summary>
    public interface IPasswordListSource
    {
        /// <summary>
        /// Fetches the password list asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<PasswordList> FetchAsync();
    }
}