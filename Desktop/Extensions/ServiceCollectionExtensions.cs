using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Desktop.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPages(
        this IServiceCollection services, 
        Func<IServiceProvider, IEnumerable<Page>> pages)
    {
        var bindings = new PageBindings();
        var provider = services.BuildServiceProvider();

        foreach (var page in pages(provider))
        {
            bindings.Add(page);
        }

        services.AddSingleton<PageBindings>();
        return services;
    }
}