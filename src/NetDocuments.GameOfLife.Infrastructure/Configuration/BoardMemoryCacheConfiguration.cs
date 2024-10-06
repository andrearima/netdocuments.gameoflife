namespace NetDocuments.GameOfLife.Infrastructure.Configuration;

public class BoardMemoryCacheConfiguration
{
    public int ExpirationInMs { get; set; }

    public void Validate()
    {
        if (ExpirationInMs <= 0)
            throw new ArgumentOutOfRangeException(nameof(ExpirationInMs), "ExpirationInMs must be greater than 0");
    }
}
