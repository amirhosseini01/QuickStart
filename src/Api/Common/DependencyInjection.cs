using FileSignatures;
using FileSignatures.Formats;

namespace Api.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddScrutor(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IGenericRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<FileUploader>();
        
        var recognised = FileFormatLocator.GetFormats().OfType<Image>();
        var inspector = new FileFormatInspector(recognised);
        services.AddSingleton<IFileFormatInspector>(inspector);

        return services;
    }
}