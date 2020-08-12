using ChoiceWebApp.Services;
using ChoiceWebApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChoiceWebApp.Extensions
{
    public static class GroupsServiceExtension
    {
        public static IServiceCollection AddGroups(this IServiceCollection services)
            => services.AddSingleton<IGroupsService, GroupsServices>();
    }
}