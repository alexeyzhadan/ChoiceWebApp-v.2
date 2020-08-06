using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChoiceWebApp.Services.Interfaces
{
    public interface IGroupsJsonIOService
    {
        Task WriteAsync(List<string> groups);
        Task<List<string>> ReadAsync();
        bool FileIsEmptyOrNotExists();
    }
}
