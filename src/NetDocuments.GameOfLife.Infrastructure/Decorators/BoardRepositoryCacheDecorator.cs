using Microsoft.Extensions.Caching.Memory;
using NetDocuments.GameOfLife.Domain.Entities;
using NetDocuments.GameOfLife.Domain.Repositories;
using NetDocuments.GameOfLife.Infrastructure.Configuration;

namespace NetDocuments.GameOfLife.Infrastructure.Decorators;

public class BoardRepositoryCacheDecorator : IBoardRepository
{
    private readonly IBoardRepository _boardRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly BoardMemoryCacheConfiguration _memoryCacheConfiguration;
    public BoardRepositoryCacheDecorator(IBoardRepository boardRepository, IMemoryCache memoryCache, BoardMemoryCacheConfiguration memoryCacheConfiguration)
    {
        _boardRepository = boardRepository;
        _memoryCache = memoryCache;
        _memoryCacheConfiguration = memoryCacheConfiguration;
    }

    public Task<string> CreateBoard(Board board, CancellationToken token)
    {
        return _boardRepository.CreateBoard(board, token);
    }

    public async Task<Board?> FindBoard(string boardId, CancellationToken token)
    {
        if (_memoryCache.TryGetValue(boardId, out Board? board) && board != null)
            return board;

        board = await _boardRepository.FindBoard(boardId, token);
        if (board != null)
            SetCache(board);

        return board;
    }

    public async Task UpdateBord(Board board, CancellationToken token)
    {
        await _boardRepository.UpdateBord(board, token);
        SetCache(board);
    }

    private void SetCache(Board board)
    {
        var timespan = TimeSpan.FromMilliseconds(_memoryCacheConfiguration.ExpirationInMs);
        _memoryCache.Set(board.Id, board, timespan);
    }
}
