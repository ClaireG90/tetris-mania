using System;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Generates and maintains the current set of offered block shapes.
    /// </summary>
    public class PieceSpawner : MonoBehaviour
    {
        private readonly System.Random _random = new System.Random();
        private readonly List<BlockShape> _shapeSet = new List<BlockShape>();
        private readonly List<BlockShape> _currentOffer = new List<BlockShape>();
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Prefab used to visualise blocks in the offer.
        /// </summary>
        public GameObject BlockPrefab = null!;

        /// <summary>
        /// Board used for placements via drag and drop.
        /// </summary>
        public BoardGrid Board = null!;

        private readonly List<GameObject> _containers = new List<GameObject>();
        private readonly List<Vector3> _originalPositions = new List<Vector3>();
        private Camera? _camera;
        private GameObject? _dragged;
#endif

        /// <summary>
        /// Gets the shapes currently offered to the player.
        /// </summary>
        public IReadOnlyList<BlockShape> CurrentOffer => _currentOffer;

        /// <summary>
        /// Gets a value indicating whether the current offer has no shapes left.
        /// </summary>
        public bool OfferEmpty => _currentOffer.Count == 0;

        private void Awake()
        {
            // Curated basic shapes
            _shapeSet.Add(new BlockShape(new bool[,] {{ true }})); // single block for tests
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, true },
                { true, true }
            }));
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, true, true },
                { false, true, false }
            }));
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, false },
                { true, false },
                { true, true }
            }));
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, true, true, true }
            }));
        }

        /// <summary>
        /// Generates a new offer of three shapes.
        /// </summary>
        public void GenerateOffer()
        {
            _currentOffer.Clear();
            for (var i = 0; i < 3; i++)
            {
                var index = _random.Next(_shapeSet.Count);
                _currentOffer.Add(_shapeSet[index]);
            }
#if UNITY_5_3_OR_NEWER
            RenderOffer();
#endif
        }

        /// <summary>
        /// Consumes the shape at the specified offer index.
        /// </summary>
        /// <param name="index">Index of the shape to remove.</param>
        public void ConsumeShape(int index)
        {
            if (index < 0 || index >= _currentOffer.Count)
            {
                return;
            }

            _currentOffer.RemoveAt(index);
#if UNITY_5_3_OR_NEWER
            if (index >= 0 && index < _containers.Count)
            {
                Destroy(_containers[index]);
                _containers.RemoveAt(index);
                _originalPositions.RemoveAt(index);
            }
#endif
            if (OfferEmpty)
            {
                GenerateOffer();
            }
        }

        /// <summary>
        /// Returns true if any shape from the current offer fits on the provided board grid.
        /// Convenience method used by tests.
        /// </summary>
        public bool AnyShapeFits(BoardGrid board)
        {
            return board.HasAnyValidPlacement(_currentOffer);
        }

#if UNITY_5_3_OR_NEWER
        private void Start()
        {
            _camera = Camera.main;
            RenderOffer();
        }

        private void RenderOffer()
        {
            foreach (var go in _containers)
            {
                Destroy(go);
            }
            _containers.Clear();
            _originalPositions.Clear();

            if (BlockPrefab == null)
            {
                return;
            }

            var baseY = -BoardGrid.Size - 2;
            var baseX = -1f * (_currentOffer.Count - 1);
            for (var i = 0; i < _currentOffer.Count; i++)
            {
                var container = new GameObject($"Offer{i}");
                var pos = new Vector3(baseX + i * 2f, baseY, 0f);
                container.transform.position = pos;
                _containers.Add(container);
                _originalPositions.Add(pos);

                var shape = _currentOffer[i];
                for (var y = 0; y < shape.Cells.GetLength(0); y++)
                {
                    for (var x = 0; x < shape.Cells.GetLength(1); x++)
                    {
                        if (!shape.Cells[y, x])
                        {
                            continue;
                        }
                        Instantiate(BlockPrefab, container.transform.position + new Vector3(x, -y, 0f), Quaternion.identity, container.transform);
                    }
                }
            }
        }

        private void Update()
        {
            if (_camera == null)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                var world = _camera.ScreenToWorldPoint(Input.mousePosition);
                for (var i = 0; i < _containers.Count; i++)
                {
                    var container = _containers[i];
                    if (Vector2.Distance(world, container.transform.position) < 1f)
                    {
                        _dragged = container;
                        break;
                    }
                }
            }
            else if (Input.GetMouseButton(0) && _dragged != null)
            {
                var world = _camera.ScreenToWorldPoint(Input.mousePosition);
                world.z = 0f;
                _dragged.transform.position = world;
            }
            else if (Input.GetMouseButtonUp(0) && _dragged != null)
            {
                var world = _dragged.transform.position;
                var gx = Mathf.RoundToInt(world.x);
                var gy = Mathf.RoundToInt(-world.y);
                var index = _containers.IndexOf(_dragged);
                if (index >= 0 && index < _currentOffer.Count && Board != null && Board.TryPlacePiece(_currentOffer[index], gx, gy))
                {
                    ConsumeShape(index);
                }
                else
                {
                    var orig = _originalPositions[index];
                    _dragged.transform.position = orig;
                }
                _dragged = null;
            }
        }
#endif
    }
}
