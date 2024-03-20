using FileSignatures;
using FileSignatures.Formats;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Commons;

public static class DependencyInjection
{
    public static IServiceCollection AddScrutorRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IGenericRepository<>))
                // repositories
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                // services
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericService)))
                    .AsSelf()
                    .WithScopedLifetime()
                    );

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

        services.AddScoped<FileUploader>();

        services.AddFileFormatLocator();

        return services;
    }
}