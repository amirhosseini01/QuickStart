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
}