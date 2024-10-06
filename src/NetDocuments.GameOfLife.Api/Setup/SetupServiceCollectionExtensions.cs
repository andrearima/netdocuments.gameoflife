using MongoDB.Driver;
using NetDocuments.GameOfLife.Application;
using NetDocuments.GameOfLife.Application.Configuration;
using NetDocuments.GameOfLife.Domain.Entities;
using NetDocuments.GameOfLife.Domain.Repositories;
using NetDocuments.GameOfLife.Infrastructure.Configuration;
using NetDocuments.GameOfLife.Infrastructure.Decorators;
using NetDocuments.GameOfLife.Infrastructure.Repository;

namespace NetDocuments.GameOfLife.Api.Setup;

internal static class SetupServiceCollectionExtensions
{
    public static IServiceCollection AddGameOfLifeServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<BoardConfiguration>(p =>
        {
            const string AppSection = "App";
            var section = configuration.GetSection(AppSection);
            var boardConfig = new BoardConfiguration();
            section.Bind(boardConfig);
            boardConfig.Validate();

            return boardConfig;
        });

        services.AddMemoryCache();
        services.AddSingleton<BoardMemoryCacheConfiguration>(p =>
        {
            var memoryCacheConfig = new BoardMemoryCacheConfiguration()
            {
                ExpirationInMs = configuration.GetValue<int>("BoardCache:ExpirationInMs")
            };
            memoryCacheConfig.Validate();

            return memoryCacheConfig;
        });

        services.AddScoped<IBoardApp, BoardApp>();

        services.AddScoped<IBoardRepository, BoardRepository>();
        services.Decorate<IBoardRepository, BoardRepositoryCacheDecorator>();

        services.AddSingleton<MongoDbConfiguration>(p =>
        {
            const string MongoDbSection = "MongoDb";
            var section = configuration.GetSection(MongoDbSection);
            var mongoConfig = new MongoDbConfiguration();
            section.Bind(mongoConfig);
            mongoConfig.Validate();

            return mongoConfig;
        });

        services.AddSingleton<MongoClient>(provider =>
        {
            var mongoConfig = provider.GetRequiredService<MongoDbConfiguration>();

            return new MongoClient(mongoConfig.ConnectionString);
        });

        services.AddSingleton<IMongoCollection<Board>>(provider =>
        {
            var mongoConfig = provider.GetRequiredService<MongoDbConfiguration>();
            var mongoClient = provider.GetRequiredService<MongoClient>();

            var database = mongoClient.GetDatabase(mongoConfig.Database);
            return database.GetCollection<Board>(mongoConfig.BoardCollection);
        });

        

        return services;
    }
}
