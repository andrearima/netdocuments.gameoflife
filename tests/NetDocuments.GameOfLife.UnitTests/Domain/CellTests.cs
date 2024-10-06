using NetDocuments.GameOfLife.Domain.ValueObjects;

namespace NetDocuments.GameOfLife.UnitTests.Domain;

public class CellTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    public void ShouldLive_ShouldReturnFalse_WhenCellIsAlive_And_HasLessThanTwoLiveNeighbours(int liveNeighbours)
    {
        // arrange
        var isAlive = true;

        // act
        var result = Cell.ShouldLive(isAlive, liveNeighbours);

        // assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void ShouldLive_ShouldReturnTrue_WhenCellIsAlive_And_Has2or3LiveNeighbours(int liveNeighbours)
    {
        // arrange
        var isAlive = true;

        // act
        var result = Cell.ShouldLive(isAlive, liveNeighbours);

        // assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void ShouldLive_ShouldReturnFalse_WhenCellIsAlive_And_HasMoreThan3LiveNeighbours(int liveNeighbours)
    {
        // arrange
        var isAlive = true;

        // act
        var result = Cell.ShouldLive(isAlive, liveNeighbours);

        // assert
        Assert.False(result);
    }

    [Fact]
    public void ShouldLive_ShouldReturnTrue_WhenCellIsDead_And_HasMore3LiveNeighbours()
    {
        // arrange
        var isAlive = false;
        var liveNeighbours = 3;

        // act
        var result = Cell.ShouldLive(isAlive, liveNeighbours);

        // assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void ShouldLive_ShouldReturnFalse_WhenCellIsDead_And_LiveNeighbours_IsDifferentFrom3(int liveNeighbours)
    {
        // arrange
        var isAlive = false;

        // act
        var result = Cell.ShouldLive(isAlive, liveNeighbours);

        // assert
        Assert.False(result);
    }
}
