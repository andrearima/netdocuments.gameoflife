namespace NetDocuments.GameOfLife.Application.Configuration;

public class BoardConfiguration
{
    public int MaxNumberOfAttempts { get; set; }
    public void Validate()
    {
        if(MaxNumberOfAttempts <=0)
            throw new ArgumentOutOfRangeException(nameof(MaxNumberOfAttempts), "MaxNumberOfAttempts must be greater than 0");
    }
}
