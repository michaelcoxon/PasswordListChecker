using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public interface IPasswordListSource
    {
        Task<IEnumerable<string>> FetchAsync();
    }
}