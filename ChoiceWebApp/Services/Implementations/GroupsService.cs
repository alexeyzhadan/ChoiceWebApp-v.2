using ChoiceWebApp.Services.Interfaces;
using System.Collections.Generic;

namespace ChoiceWebApp.Services.Implementations
{
    public class GroupsService : IGroupsService
    {
        public List<string> Groups { get; }

        public GroupsService(IGroupsJsonIOService groups)
        {
            Groups = groups.ReadAsync().Result;
        }
    }
}