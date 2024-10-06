using MongoDB.Driver;
using NetDocuments.GameOfLife.Domain.Entities;
using NetDocuments.GameOfLife.Domain.Repositories;

namespace NetDocuments.GameOfLife.Infrastructure.Repository;

public class BoardRepository : IBoardRepository
{
    private readonly IMongoCollection<Board> _boardsCollection;

    public BoardRepository(IMongoCollection<Board> boardsCollection)
    {
        _boardsCollection = boardsCollection;
    }

    public async Task<string> CreateBoard(Board board, CancellationToken token)
    {
        var queryBoard = await FindBoard(board.Id, token);
        if(queryBoard != null)
            return queryBoard.Id;

        await _boardsCollection.InsertOneAsync(board, cancellationToken: token);

        return board.Id;
    }

    public async Task<Board?> FindBoard(string boardId, CancellationToken token)
    {
        var board = await _boardsCollection.FindAsync<Board>(filter => filter.Id == boardId, cancellationToken: token);

        return await board.FirstOrDefaultAsync(token);
    }

    public async Task UpdateBord(Board board, CancellationToken token)
    {
        var filter = Builders<Board>.Filter.Eq(b => b.Id, board.Id);
        var options = new UpdateOptions { IsUpsert = true };
        var update = Builders<Board>.Update
            .Set(b => b.FinalState, board.FinalState)
            .Set(b => b.FinalStep, board.FinalStep);

        var updateResult = await _boardsCollection.UpdateOneAsync<Board>(filter => filter.Id == board.Id, update, options, token);
    }
}
