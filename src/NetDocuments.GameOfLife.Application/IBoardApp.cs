using NetDocuments.GameOfLife.Application.Commands;
using NetDocuments.GameOfLife.Domain.Entities;

namespace NetDocuments.GameOfLife.Application;

public interface IBoardApp
{
    Task<string> CreateBoard(CreteNewBoardCommand command, CancellationToken token);
    Task<bool[,]?> GetNexStateBoard(string boardId, CancellationToken token);
    Task<bool[,]?> GetBoardAtStateNumber(string boardId, int stateNumber, CancellationToken token);
    Task<Board?> GetFinalState(string boardId, CancellationToken token);
}