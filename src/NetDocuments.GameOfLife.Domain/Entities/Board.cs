using MongoDB.Bson.Serialization.Attributes;
using NetDocuments.GameOfLife.Domain.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace NetDocuments.GameOfLife.Domain.Entities;

public class Board
{
    public Board(bool[,] boardInitialState)
    {
        BoardInitialState = boardInitialState;
        Rows = boardInitialState.GetLength(0);
        Cols = boardInitialState.GetLength(1);
        Id = GetBoardId();
    }

    [BsonId]
    public string Id { get; set; }

    [BsonRequired]
    public bool[,] BoardInitialState { get; set; }

    [BsonRequired]
    public int Rows { get; set; }

    [BsonRequired]
    public int Cols { get; set; }

    [BsonIgnoreIfNull]
    public int? FinalStep { get; set; }

    public bool HasFinalStep => FinalStep.HasValue;

    [BsonIgnoreIfNull]
    public bool[,]? FinalState { get; set; }


    public Board TryGetFinalState(int maxIterations)
    {
        if (HasFinalStep)
            return this;

        var nextstate = GetNextState();
        var counter = 1;

        while (counter < maxIterations)
        {
            var previoustate = nextstate;
            nextstate = GetNextState(previoustate);

            if (AreStatesEqual(previoustate, nextstate))
            {
                FinalState = nextstate;
                FinalStep = counter;
                break; //don´t need to keep calculating the next state
            }

            counter++;
        }

        return this;
    }
    
    public bool[,] GetState(int iterations)
    {
        var nextstate = GetNextState();
        var counter = 1;

        while (counter < iterations)
        {
            var previoustate = nextstate;
            nextstate = GetNextState(previoustate);

            if (AreStatesEqual(previoustate, nextstate))
            {
                FinalState = nextstate;
                break; //don´t need to keep calculating the next state
            }

            counter++;
        }

        return nextstate;
    }

    public bool[,] GetNextState(bool[,]? currentState = null)
    {
        currentState ??= BoardInitialState;
        var nextState = new bool[Rows, Cols];

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                var liveNeighbours = CountLiveNeighbours(row, col, currentState);
                var isAlive = currentState[row, col];

                nextState[row, col] = Cell.ShouldLive(isAlive, liveNeighbours);
            }
        }

        return nextState;
    }

    private int CountLiveNeighbours(int row, int col, bool[,] currentState)
    {
        var liveNeighbours = 0;

        // Check the surrounding cells
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) 
                    continue; // Skip the current cell itself

                int neighbourRow = row + i;
                int neighbourCol = col + j;

                // Ensure the neighbour is within bounds
                if (neighbourRow >= 0 && neighbourRow < Rows && neighbourCol >= 0 && neighbourCol < Cols)
                {
                    if (currentState[neighbourRow, neighbourCol])
                        liveNeighbours++;
                }
            }
        }

        return liveNeighbours;
    }

    private bool AreStatesEqual(bool[,] state1, bool[,] state2)
    {
        for (int row = 0; row < state1.GetLength(0); row++)
        {
            for (int col = 0; col < state1.GetLength(1); col++)
            {
                if (state1[row, col] != state2[row, col])
                    return false;
            }
        }

        return true;
    }

    private string GetBoardId()
    {
        var sb = new StringBuilder();

        // Convert board to a string representation
        for (int row = 0; row < Rows; row++)
            for (int col = 0; col < Cols; col++)
                sb.Append(BoardInitialState[row, col] ? '1' : '0');

        var inputBytes = Encoding.ASCII.GetBytes(sb.ToString());
        var hashBytes = MD5.HashData(inputBytes);

        var hashSb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashSb.Append(b.ToString("X2"));
        }

        return hashSb.ToString();
    }
}
