using Microsoft.Extensions.DependencyInjection;
using NetDocuments.GameOfLife.Domain.Entities;
using NetDocuments.GameOfLife.Domain.Repositories;
using Newtonsoft.Json;
using System.Net;

namespace NetDocuments.GameOfLife.IntegrationTests;

public class BoardController_GetNextStateTests : IClassFixture<GameOfLifeFactory>
{
    private readonly GameOfLifeFactory _factory;
    private readonly HttpClient _httpClient;
    public BoardController_GetNextStateTests(GameOfLifeFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetNextState_ShouldReturn_RightNextState()
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
        var response = await _httpClient.GetAsync($"{board.Id}/nextstate");
        var result = JsonConvert.DeserializeObject<bool[,]>( await response.Content.ReadAsStringAsync());

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