using MongoDB.Driver;

namespace NetDocuments.GameOfLife.Infrastructure.Configuration;

public sealed class MongoDbConfiguration
{
    public string? ConnectionString { get; set; }
    public string? Database { get; set; }
    public string? BoardCollection { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
            throw new MongoConfigurationException("Connection string is required.");

        if (string.IsNullOrWhiteSpace(Database))
            throw new MongoConfigurationException("Database is required.");

        if (string.IsNullOrWhiteSpace(BoardCollection))
            throw new MongoConfigurationException("BoardCollection is required.");
    }
}
