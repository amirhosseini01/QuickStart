using FileSignatures;
using FileSignatures.Formats;

namespace Api.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddScrutorRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IGenericRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddScrutorGenericServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IGenericService))
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericService)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddFileFormatLocator(this IServiceCollection services)
    {
        var recognized = FileFormatLocator.GetFormats().OfType<Image>();
        var inspector = new FileFormatInspector(recognized);
        services.AddSingleton<IFileFormatInspector>(inspector);

        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScrutorRepositories();
        services.AddScrutorGenericServices();

        services.AddScoped<FileUploader>();
        
        services.AddFileFormatLocator();

        return services;
    }
}