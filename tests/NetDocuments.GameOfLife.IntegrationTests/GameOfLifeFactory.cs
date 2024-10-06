using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using NetDocuments.GameOfLife.Domain.Entities;
using System.Text;
using Newtonsoft.Json;

namespace NetDocuments.GameOfLife.IntegrationTests;

public class GameOfLifeFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbRunner _runner;
    private readonly MongoClient _mongoClient;
    public IMongoCollection<Board> BoardCollection
    {
        get => Services.GetRequiredService<IMongoCollection<Board>>();
    }

    public GameOfLifeFactory()
    {
        _runner = MongoDbRunner.Start();
        _mongoClient = new MongoClient(_runner.ConnectionString);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Replace(new ServiceDescriptor(typeof(MongoClient), _mongoClient));
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _runner.Dispose();
        }
        base.Dispose(disposing);
    }

    public StringContent CreateContent(Board board)
    {
        var jsonContent = JsonConvert.SerializeObject(board.BoardInitialState);
        return new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }
}
