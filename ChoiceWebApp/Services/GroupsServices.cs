using ChoiceWebApp.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ChoiceWebApp.Services
{
    public class GroupsServices : IGroupsService
    {
        public List<string> Groups { get; }

        public GroupsServices(IOptions<List<string>> options)
        {
            Groups = options.Value;
        }
    }
}