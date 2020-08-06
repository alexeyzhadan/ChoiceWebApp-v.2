using ChoiceWebApp.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChoiceWebApp.Services.Implementations
{
    public class GroupsJsonIOService : IGroupsJsonIOService
    {
        private const string FILE_NAME = "groups.json";

        public async Task<List<string>> ReadAsync()
        {
            using (FileStream fileStream = new FileStream(FILE_NAME, FileMode.OpenOrCreate))
            {
                return await JsonSerializer.DeserializeAsync<List<string>>(fileStream);
            }
        }

        public async Task WriteAsync(List<string> groups)
        {
            using (FileStream fileStream = new FileStream(FILE_NAME, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fileStream, groups);
            }
        }

        public bool FileIsEmptyOrNotExists()
        {
            if (!File.Exists(FILE_NAME) || new FileInfo(FILE_NAME).Length == 0)
            {
                return true;
            }
            return false;
        }
    }
}