namespace NetDocuments.GameOfLife.Application.Commands;

public sealed record CreteNewBoardCommand
{
    public bool[,] BoardInitialState { get; }

    public CreteNewBoardCommand(bool[,] boardInitialState)
    {
        BoardInitialState = boardInitialState;
    }

    public bool IsValid()
    {
        if(BoardInitialState == null)
            return false;

        var rows = BoardInitialState.GetLength(0);
        var columns = BoardInitialState.GetLength(1);

        return rows >= 1 && columns >= 1;
    }
}
