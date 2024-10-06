using NetDocuments.GameOfLife.Domain.Entities;

namespace NetDocuments.GameOfLife.Domain.Repositories;

public interface IBoardRepository
{
    Task<string> CreateBoard(Board board, CancellationToken token);
    Task<Board?> FindBoard(string boardId, CancellationToken token);
    Task UpdateBord(Board board, CancellationToken token);
}
