using System;
using System.Collections.Generic;

/// <summary>
/// Provides random block pieces used in gameplay.
/// </summary>
public class PieceSpawner
{
    private readonly Random _random;
    private readonly List<bool[,]> _pieces = new List<bool[,]>();

    /// <summary>
    /// Initializes a new instance of <see cref="PieceSpawner"/>.
    /// </summary>
    /// <param name="seed">Optional random seed for deterministic results.</param>
    public PieceSpawner(int? seed = null)
    {
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        _pieces.Add(new bool[,]
        {
            { true, true },
            { true, true }
        }); // Square

        _pieces.Add(new bool[,]
        {
            { true, true, true },
            { false, true, false }
        }); // T

        _pieces.Add(new bool[,]
        {
            { true, false },
            { true, false },
            { true, true }
        }); // L

        _pieces.Add(new bool[,]
        {
            { true, true, true, true }
        }); // Line
    }

    /// <summary>
    /// Returns a random piece shape.
    /// </summary>
    public bool[,] GetRandomPiece()
    {
        int index = _random.Next(_pieces.Count);
        return (bool[,])_pieces[index].Clone();
    }

    /// <summary>
    /// Returns all available piece shapes.
    /// </summary>
    public IEnumerable<bool[,]> AllPieces
    {
        get
        {
            foreach (var piece in _pieces)
            {
                yield return (bool[,])piece.Clone();
            }
        }
    }
}

