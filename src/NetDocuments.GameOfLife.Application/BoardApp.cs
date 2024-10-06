using NetDocuments.GameOfLife.Application.Commands;
using NetDocuments.GameOfLife.Application.Configuration;
using NetDocuments.GameOfLife.Domain.Entities;
using NetDocuments.GameOfLife.Domain.Repositories;

namespace NetDocuments.GameOfLife.Application;

public sealed class BoardApp : IBoardApp
{
    private readonly IBoardRepository _boardRepository;
    private readonly BoardConfiguration _boardConfiguration;
    public BoardApp(IBoardRepository boardRepository, BoardConfiguration boardConfiguration)
    {
        _boardRepository = boardRepository;
        _boardConfiguration = boardConfiguration;
    }

    public async Task<string> CreateBoard(CreteNewBoardCommand command, CancellationToken token)
    {
        var board = new Board(command.BoardInitialState);

        var boardFromDb = await _boardRepository.FindBoard(board.Id, token);
        if (boardFromDb is not null)
            return boardFromDb.Id;

        _ = await _boardRepository.CreateBoard(board, token);

        return board.Id;
    }

    public async Task<bool[,]?> GetNexStateBoard(string boardId, CancellationToken token)
    {
        var board = await _boardRepository.FindBoard(boardId, token);

        return board?.GetNextState();
    }

    public async Task<bool[,]?> GetBoardAtStateNumber(string boardId, int iterations, CancellationToken token)
    {
        var board = await _boardRepository.FindBoard(boardId, token);

        return board?.GetState(iterations);
    }

    public async Task<Board?> GetFinalState(string boardId, CancellationToken token)
    {
        var board = await _boardRepository.FindBoard(boardId, token);

        if (board == null)
            return board;

        var finalState = board.TryGetFinalState(_boardConfiguration.MaxNumberOfAttempts);
        if(board.HasFinalStep)
            await _boardRepository.UpdateBord(board, token);

        return finalState;
    }
}
