using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using Vector2Int = UnityEngine.Vector2Int;
#else
using Vector2Int = System.ValueTuple<int, int>;
#endif

namespace TetrisMania
{
    /// <summary>
    /// Manages the main 8x8 board grid, handling block placement and line clears.
    /// </summary>
    public class BoardGrid : MonoBehaviour
    {
        /// <summary>
        /// Size of the board on each axis.
        /// </summary>
        public const int Size = 8;

        private bool[,] _grid = null!;
        private readonly PlacementValidator _validator = new PlacementValidator();
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Prefab used to represent a single block visually.
        /// </summary>
        public GameObject BlockPrefab = null!;
#endif

#if UNITY_5_3_OR_NEWER
        private readonly Dictionary<Vector2Int, GameObject> _blockObjects = new Dictionary<Vector2Int, GameObject>();
#else
        private readonly Dictionary<Vector2Int, GameObject?> _blockObjects = new Dictionary<Vector2Int, GameObject?>();
#endif

        /// <summary>
        /// Raised whenever one or more lines are cleared.
        /// </summary>
        public event Action<int>? LinesCleared;

        private void Awake()
        {
            _grid = new bool[Size, Size];
        }

        /// <summary>
        /// Returns whether a specific cell is occupied.
        /// </summary>
        public bool IsCellOccupied(int x, int y) => _grid[y, x];

        /// <summary>
        /// Gets a copy of the current board state.
        /// </summary>
        public bool[,] GetSnapshot()
        {
            var copy = new bool[Size, Size];
            Array.Copy(_grid, copy, _grid.Length);
            return copy;
        }

        /// <summary>
        /// Restores the board state from a snapshot.
        /// </summary>
        public void SetSnapshot(bool[,] snapshot)
        {
            if (snapshot == null || snapshot.GetLength(0) != Size || snapshot.GetLength(1) != Size)
            {
                return;
            }

            Array.Copy(snapshot, _grid, _grid.Length);
        }

        /// <summary>
        /// Clears the board state and removes all spawned block objects.
        /// </summary>
        public void ClearAll()
        {
            _grid = new bool[Size, Size];
#if UNITY_5_3_OR_NEWER
            foreach (var obj in _blockObjects.Values)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
#endif
            _blockObjects.Clear();
        }

        /// <summary>
        /// Attempts to place a shape at the specified coordinates.
        /// </summary>
        public bool TryPlacePiece(BlockShape shape, int x, int y)
        {
            if (shape == null || !_validator.CanPlace(_grid, shape, x, y))
            {
                return false;
            }

            PlaceShape(shape, x, y);
            var cleared = ClearLines();
            if (cleared > 0)
            {
                LinesCleared?.Invoke(cleared);
            }

            return true;
        }

        /// <summary>
        /// Determines whether any of the provided shapes can be placed on the board.
        /// </summary>
        public bool HasAnyValidPlacement(IEnumerable<BlockShape> shapes)
        {
            if (shapes == null)
            {
                return false;
            }

            foreach (var shape in shapes)
            {
                if (shape == null)
                {
                    continue;
                }

                for (var y = 0; y < Size; y++)
                {
                    for (var x = 0; x < Size; x++)
                    {
                        if (_validator.CanPlace(_grid, shape, x, y))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Fills the entire grid to leave no valid placements. Used for tests.
        /// </summary>
        public void DebugFillNoMovesLeft()
        {
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    _grid[y, x] = true;
                }
            }
        }

        private void PlaceShape(BlockShape shape, int startX, int startY)
        {
            for (var y = 0; y < shape.Cells.GetLength(0); y++)
            {
                for (var x = 0; x < shape.Cells.GetLength(1); x++)
                {
                    if (!shape.Cells[y, x])
                    {
                        continue;
                    }

                    var gridX = startX + x;
                    var gridY = startY + y;
                    _grid[gridY, gridX] = true;
#if UNITY_5_3_OR_NEWER
                    if (BlockPrefab != null)
                    {
                        var worldPos = new Vector3(gridX, -gridY, 0f);
                        var obj = Instantiate(BlockPrefab, worldPos, Quaternion.identity);
                        _blockObjects[Pos(gridX, gridY)] = obj;
                    }
#endif
                }
            }
        }

        private static Vector2Int Pos(int x, int y)
        {
#if UNITY_5_3_OR_NEWER
            return new Vector2Int(x, y);
#else
            return (x, y);
#endif
        }

        private void ClearBlock(int x, int y)
        {
            var key = Pos(x, y);
            if (_blockObjects.TryGetValue(key, out var obj))
            {
#if UNITY_5_3_OR_NEWER
                if (obj != null)
                {
                    var renderer = obj.GetComponent<SpriteRenderer>();
                    if (renderer != null)
                    {
                        renderer.color = Color.yellow;
                    }
                    Destroy(obj);
                }
#endif
                _blockObjects.Remove(key);
            }
        }

        private int ClearLines()
        {
            var cleared = 0;

            // rows
            for (var y = 0; y < Size; y++)
            {
                var full = true;
                for (var x = 0; x < Size; x++)
                {
                    if (!_grid[y, x])
                    {
                        full = false;
                        break;
                    }
                }

                if (full)
                {
                    cleared++;
                    for (var x = 0; x < Size; x++)
                    {
                        _grid[y, x] = false;
                        ClearBlock(x, y);
                    }
                }
            }

            // columns
            for (var x = 0; x < Size; x++)
            {
                var full = true;
                for (var y = 0; y < Size; y++)
                {
                    if (!_grid[y, x])
                    {
                        full = false;
                        break;
                    }
                }

                if (full)
                {
                    cleared++;
                    for (var y = 0; y < Size; y++)
                    {
                        _grid[y, x] = false;
                        ClearBlock(x, y);
                    }
                }
            }

            return cleared;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    Gizmos.DrawWireCube(new Vector3(x, -y, 0), Vector3.one);
                }
            }
        }
    }
}
