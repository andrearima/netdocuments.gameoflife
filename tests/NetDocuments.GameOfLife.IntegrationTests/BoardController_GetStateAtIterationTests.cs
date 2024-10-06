using Microsoft.Extensions.DependencyInjection;
using NetDocuments.GameOfLife.Domain.Entities;
using NetDocuments.GameOfLife.Domain.Repositories;
using Newtonsoft.Json;
using System.Net;

namespace NetDocuments.GameOfLife.IntegrationTests;

public class BoardController_GetStateAtIterationTests : IClassFixture<GameOfLifeFactory>
{
    private readonly GameOfLifeFactory _factory;
    private readonly HttpClient _httpClient;
    public BoardController_GetStateAtIterationTests(GameOfLifeFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetStateAtIteration_ShouldReturn_RightNextState_1Iteration()
    {
        // arrange
        var boardRepository = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<IBoardRepository>();

        var initialState = new bool[3, 3]
        {
            { true, true, true },
            { true, false, true },
            { true, true, true }
        };
        var board = new Board(initialState);
        var expectedResult = new bool[3, 3]
        {
            { true,  false, true  },
            { false, false, false },
            { true,  false, true  }
        };

        await boardRepository.CreateBoard(board, CancellationToken.None);
        var content = _factory.CreateContent(board);

        // act
        var response = await _httpClient.GetAsync($"{board.Id}/state/1");
        var result = JsonConvert.DeserializeObject<bool[,]>(await response.Content.ReadAsStringAsync());

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(expectedResult.GetLength(0), result.GetLength(0)); // Check row count
        Assert.Equal(expectedResult.GetLength(1), result.GetLength(1)); // Check column count
        for (int row = 0; row < expectedResult.GetLength(0); row++)
            for (int col = 0; col < expectedResult.GetLength(1); col++)
                Assert.Equal(expectedResult[row, col], result[row, col]); // Compare individual elements
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public async Task GetStateAtIteration_ShouldReturn_RightNextState_Iterations(int iterations)
    {
        // arrange
        var boardRepository = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<IBoardRepository>();

        var initialState = new bool[3, 3]
        {
            { true, true, true },
            { true, false, true },
            { true, true, true }
        };
        var board = new Board(initialState);
        var expectedResult = new bool[3, 3]
        {
            { false,  false, false  },
            { false,  false, false  },
            { false,  false, false  }
        };

        await boardRepository.CreateBoard(board, CancellationToken.None);
        var content = _factory.CreateContent(board);

        // act
        var response = await _httpClient.GetAsync($"{board.Id}/state/{iterations}");
        var result = JsonConvert.DeserializeObject<bool[,]>(await response.Content.ReadAsStringAsync());

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(expectedResult.GetLength(0), result.GetLength(0)); // Check row count
        Assert.Equal(expectedResult.GetLength(1), result.GetLength(1)); // Check column count
        for (int row = 0; row < expectedResult.GetLength(0); row++)
            for (int col = 0; col < expectedResult.GetLength(1); col++)
                Assert.Equal(expectedResult[row, col], result[row, col]); // Compare individual elements
    }

}