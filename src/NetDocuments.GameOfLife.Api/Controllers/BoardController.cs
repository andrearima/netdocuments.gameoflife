using Microsoft.AspNetCore.Mvc;
using NetDocuments.GameOfLife.Application;
using NetDocuments.GameOfLife.Application.Commands;

namespace NetDocuments.GameOfLife.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardController : ControllerBase
{
    private readonly ILogger<BoardController> _logger;
    private readonly IBoardApp _boardApp;
    public BoardController(ILogger<BoardController> logger, IBoardApp boardApp)
    {
        _logger = logger;
        _boardApp = boardApp;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewBoard([FromBody] bool[,]? board, CancellationToken token)
    {
        var command = new CreteNewBoardCommand(board);
        if (!command.IsValid())
            return BadRequest("Invalid Board");

        var id = await _boardApp.CreateBoard(command, token);

        return Ok(new { Id = id });
    }

    [HttpGet("/[controller]/{boardId}/nextstate")]
    public async Task<IActionResult> GetNextState(string boardId, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(boardId))
            return BadRequest("Invalid Board Id");

        var state = await _boardApp.GetNexStateBoard(boardId, token);
        if (state == null)
            return NotFound();

        return Ok(state);
    }

    [HttpGet("/[controller]/{boardId}/state/{iterations}")]
    public async Task<IActionResult> GetStateAtIteration(string boardId, int? iterations, CancellationToken token)
    {
        if (iterations is null || iterations <= 0)
            return BadRequest("Invalid Iterations");

        if (string.IsNullOrWhiteSpace(boardId))
            return BadRequest("Invalid Board Id");

        var state = await _boardApp.GetBoardAtStateNumber(boardId, iterations.Value, token);
        if (state == null)
            return NotFound();

        return Ok(state);
    }

    [HttpGet("/[controller]/{boardId}/state/final")]
    public async Task<IActionResult> GetFinalState(string boardId, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(boardId))
            return BadRequest("Invalid Board Id.");

        var board = await _boardApp.GetFinalState(boardId, token);
        if (board == null)
            return NotFound();

        if (!board.HasFinalStep)
            return BadRequest("Could not find a final state.");

        return Ok(board.FinalState);
    }
}
