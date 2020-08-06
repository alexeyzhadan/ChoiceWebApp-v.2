using ChoiceWebApp.Services.Implementations;
using ChoiceWebApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChoiceWebApp.Services.Extentions
{
    public static class GroupsServiceExtention
    {
        public static IServiceCollection AddGroups(this IServiceCollection services)
        {
            services.AddSingleton<IGroupsJsonIOService, GroupsJsonIOService>();
            services.AddScoped<IGroupsService, GroupsService>();

            return services;
        }      
    }
}