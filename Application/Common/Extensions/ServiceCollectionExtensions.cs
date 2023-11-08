using Application.Common.Interfaces.Mappings;
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Common.Mappings;
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Persistance.Repositories;
using Application.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAccountMapper, AccountMapper>();
        services.AddSingleton<IGroupMapper, GroupMapper>();
        services.AddSingleton<IMessageMapper, MessageMapper>();
        
        services.AddValidatorsFromAssembly(typeof(IAssemblyMark).Assembly);
        
        services.AddMediatR(
            x => x.RegisterServicesFromAssemblyContaining<IAssemblyMark>());

        return services;
    }

    public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Default");
        
        services.AddDbContext<IDataContext, DataContext>(
            x => x.UseSqlServer(connection));

        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();
        
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<IMessageService, MessageService>();

        return services;
    }
}
