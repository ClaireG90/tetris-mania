using NUnit.Framework;

public class BoardGridTests
{
    [Test]
    public void ClearsRowsAndColumns()
    {
        BoardGrid grid = new BoardGrid();
        PlacementValidator validator = new PlacementValidator();
        bool[,] single = { { true } };
        int linesCleared = 0;
        grid.LinesCleared += count => linesCleared += count;

        for (int x = 0; x < BoardGrid.Size; x++)
        {
            Assert.IsTrue(validator.CanPlace(grid, single, x, 0));
            grid.PlacePiece(single, x, 0);
        }

        Assert.AreEqual(1, linesCleared);
        for (int x = 0; x < BoardGrid.Size; x++)
        {
            Assert.IsFalse(grid.GetCell(x, 0));
        }

        for (int y = 0; y < BoardGrid.Size; y++)
        {
            Assert.IsTrue(validator.CanPlace(grid, single, 0, y));
            grid.PlacePiece(single, 0, y);
        }

        Assert.AreEqual(2, linesCleared);
        for (int y = 0; y < BoardGrid.Size; y++)
        {
            Assert.IsFalse(grid.GetCell(0, y));
        }
    }

    [Test]
    public void AwardsScoreOnClear()
    {
        BoardGrid grid = new BoardGrid();
        bool[,] single = { { true } };

        for (int x = 0; x < BoardGrid.Size; x++)
        {
            grid.PlacePiece(single, x, 0);
        }

        Assert.AreEqual(10, grid.Score);
    }

    [Test]
    public void DetectsGameOverWhenNoPlacementsRemain()
    {
        BoardGrid grid = new BoardGrid();
        bool[,] single = { { true } };

        for (int x = 0; x < BoardGrid.Size; x++)
        {
            for (int y = 0; y < BoardGrid.Size; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                grid.SetCell(x, y, true);
            }
        }

        PieceSpawner spawner = new PieceSpawner(0);
        PlacementValidator validator = new PlacementValidator();
        bool possible = validator.HasAnyValidPlacement(grid, spawner.AllPieces);
        Assert.IsFalse(possible);
    }
}

