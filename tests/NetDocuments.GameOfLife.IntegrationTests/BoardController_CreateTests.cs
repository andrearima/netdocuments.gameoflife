using NetDocuments.GameOfLife.Domain.Entities;
using System.Net;

namespace NetDocuments.GameOfLife.IntegrationTests;

public class BoardController_CreateTests : IClassFixture<GameOfLifeFactory>
{
    private readonly GameOfLifeFactory _factory;
    private readonly HttpClient _httpClient;
    public BoardController_CreateTests(GameOfLifeFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Post_ShouldReturn_Ok_WithId()
    {
        // arrange
        var array2d = new bool[,]
        {
            { true, false, false },
            { false, false, false },
            { false, false, false }
        };
        var board = new Board(array2d);
        var content = _factory.CreateContent(board);

        // act
        var response = await _httpClient.PostAsync("/board", content);

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(board.Id, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Post_ShouldReturn_BadRequest_WhenEmpty()
    {
        // arrange
        var board = new Board(new bool[0, 0]);
        var content = _factory.CreateContent(board);

        // act
        var response = await _httpClient.PostAsync("/board", content);

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Invalid Board", await response.Content.ReadAsStringAsync());
    }
}