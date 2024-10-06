namespace NetDocuments.GameOfLife.Domain.ValueObjects;

public static class Cell
{
    public static bool ShouldLive(bool isAlive, int liveNeighbours)
    {
        if (isAlive)
        {
            // 1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
            if (liveNeighbours < 2)
                return false;

            // 2. Any live cell with two or three live neighbours lives on to the next generation.
            if (liveNeighbours == 2 || liveNeighbours == 3)
                return true;

            // 3. Any live cell with more than three live neighbours dies, as if by overpopulation.
            if (liveNeighbours > 3)
                return false;

            return true; // will not reach
        }
        else
        {
            // Reproduction
            if (liveNeighbours == 3)
                return true; // Cell becomes alive

            return false;
        }
    }
}
