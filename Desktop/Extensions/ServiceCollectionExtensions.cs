using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Desktop.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPagesFromAssemblyContaining<T>(
        this IServiceCollection services)
    {
        var assembly = typeof(T).Assembly;

        foreach (var page in assembly
                     .GetTypes()
                     .Where(x => typeof(Page)
                         .IsAssignableFrom(x)))
        {
            services.AddTransient(page);
        }
        
        return services;
    }
    
    public static IServiceCollection AddWindowsFromAssemblyContaining<T>(
        this IServiceCollection services)
    {
        var assembly = typeof(T).Assembly;

        foreach (var window in assembly
                     .GetTypes()
                     .Where(x => typeof(Window)
                         .IsAssignableFrom(x)))
        {
            services.AddTransient(window);
        }
        
        return services;
    }
}