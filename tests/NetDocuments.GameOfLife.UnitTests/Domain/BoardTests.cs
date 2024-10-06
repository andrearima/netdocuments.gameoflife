using NetDocuments.GameOfLife.Domain.Entities;

namespace NetDocuments.GameOfLife.UnitTests.Domain;

public class BoardTests
{
    [Fact]
    public void GetNextState_ShouldGenerateExpected_1x1()
    {
        // arrange
        var initialState = new bool[1, 1]
        {
            { true }
        };
        var board = new Board(initialState);

        var expectedResult = new bool[1, 1]
        {
            { false }
        };

        // act
        var result = board.GetNextState();

        // assert
        Assert.Equal(expectedResult.GetLength(0), result.GetLength(0)); // Check row count
        Assert.Equal(expectedResult.GetLength(1), result.GetLength(1)); // Check column count
        for (int row = 0; row < expectedResult.GetLength(0); row++)
            for (int col = 0; col < expectedResult.GetLength(1); col++)
                Assert.Equal(expectedResult[row, col], result[row, col]); // Compare individual elements
    }

    [Fact]
    public void GetNextState_ShouldGenerateExpected_2x2()
    {
        // arrange
        var initialState = new bool[2, 2]
        {
            { true, false },
            { true, false },
        };
        var board = new Board(initialState);

        var expectedResult = new bool[2, 2]
        {
            { false, false },
            { false, false }
        };

        // act
        var result = board.GetNextState();

        // assert
        Assert.Equal(expectedResult.GetLength(0), result.GetLength(0)); // Check row count
        Assert.Equal(expectedResult.GetLength(1), result.GetLength(1)); // Check column count
        for (int row = 0; row < expectedResult.GetLength(0); row++)
            for (int col = 0; col < expectedResult.GetLength(1); col++)
                Assert.Equal(expectedResult[row, col], result[row, col]); // Compare individual elements
    }

    [Fact]
    public void GetNextState_ShouldGenerateExpected_3x3()
    {
        // arrange
        var initialState = new bool[3, 3]
        {
            { true, true,  true },
            { true, false, true },
            { true, true,  true }
        };
        var board = new Board(initialState);

        var expectedResult = new bool[3, 3]
        {
            { true,  false, true  },
            { false, false, false },
            { true,  false, true  }
        };

        // act
        var result = board.GetNextState();

        // assert
        Assert.Equal(expectedResult.GetLength(0), result.GetLength(0)); // Check row count
        Assert.Equal(expectedResult.GetLength(1), result.GetLength(1)); // Check column count
        for (int row = 0; row < expectedResult.GetLength(0); row++)
            for (int col = 0; col < expectedResult.GetLength(1); col++)
                Assert.Equal(expectedResult[row, col], result[row, col]); // Compare individual elements
    }

    [Fact]
    public void GetNextState_ShouldGenerateExpected_5x5()
    {
        // arrange
        var initialState = new bool[5, 5]
        {
            { true,  true,  false, false, false },
            { true,  false, false, true,  true  },
            { false, false, false, false, false },
            { false, true,  true,  true,  false },
            { false, false, false, false, false }
        };
        var board = new Board(initialState);
        var expectedResult = new bool[5, 5]
        {
            { true,  true,  false, false, false },
            { true,  true,  false, false, false },
            { false, true,  false, false, true },
            { false, false, true,  false, false },
            { false, false, true,  false, false }
        };

        // act
        var result = board.GetNextState();

        // assert
        Assert.Equal(expectedResult.GetLength(0), result.GetLength(0)); // Check row count
        Assert.Equal(expectedResult.GetLength(1), result.GetLength(1)); // Check column count
        for (int row = 0; row < expectedResult.GetLength(0); row++)
            for (int col = 0; col < expectedResult.GetLength(1); col++)
                Assert.Equal(expectedResult[row, col], result[row, col]); // Compare individual elements
    }
}