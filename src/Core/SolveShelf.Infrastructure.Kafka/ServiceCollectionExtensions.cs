using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SolveShelf.Infrastructure.Kafka;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSolveShelfKafka(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<KafkaOptions>(cfg.GetSection("Kafka"));
        services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();
        services.AddSingleton<IKafkaProducer, KafkaJsonProducer>();
        return services;
    }
}
