using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.PlatformGenerator
{
    public class GenerateLevelController
    {
        private const int CountWall = 4;

        private Tilemap _tilemapGround;
        private Tile _tileGround;
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly int _factorSmooth;
        private readonly int _randomFillPercent;

        private int[,] _map;

        public GenerateLevelController(GenerateLevelView  generateLevelView)
        {
            _tilemapGround = generateLevelView.TileMapGround;
            _tileGround = generateLevelView.TileGround;
            _mapWidth = generateLevelView.MapWidth;
            _mapHeight = generateLevelView.MapHeight;
            _factorSmooth = generateLevelView.FactorSmooth;
            _randomFillPercent = generateLevelView.RandomFillPercent;
            _map = new int[_mapWidth, _mapHeight];
        }

        public void Awake()
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            RandomFillLevel();

            //for (var i = 0; i < _factorSmooth; ++i)
             //   SmoothMap();

            DrawTilesOnMap();
        }

        private void DrawTilesOnMap()
        {
            if (null == _map)
                return;

            for (var x = 0; x < _mapWidth; ++x)
            {
                for (var y = 0; y < _mapHeight; ++y)
                {
                    var positionTile = new Vector3Int(
                        -_mapWidth / 2 + x, -_mapHeight + y - 10, 0);

                    if (1 == _map[x, y])
                        _tilemapGround.SetTile(positionTile, _tileGround);
                }
            }
        }

        private void SmoothMap()
        {
            for (var x = 0; x < _mapWidth; ++x)
            {
                for (var y = 0; y < _mapHeight; ++y)
                {
                    var neighbourWallTiles = GetSurroundingWallcount(x, y);

                    if (neighbourWallTiles > CountWall)
                        _map[x, y] = 1;
                    else
                        _map[x, y] = 0;
                }
            }
        }

        private int GetSurroundingWallcount(int gridX, int gridY)
        {
            var wallCount = 0;

            for (var neighbourX = gridX - 1; neighbourX <= gridX + 1; ++neighbourX)
            {
                for (var neighbourY = gridY - 1; neighbourY <= gridY + 1; ++neighbourY)
                {
                    if (neighbourX >= 0 && neighbourX < _mapWidth &&
                        neighbourY >= 0 && neighbourY < _mapHeight)
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                            wallCount += _map[neighbourX, neighbourY];
                    }
                    else
                        ++wallCount;
                }
            }
            return wallCount;
        }

        private void RandomFillLevel()
        {
            var seed = Time.time.ToString();
            var pseudoRandom = new System.Random(seed.GetHashCode());

            for (var x = 0; x < _mapWidth; ++x)
            {
                for (var y = 0; y < _mapHeight; ++y)
                {
                    if (0 == x || _mapWidth - 1 == x ||
                        0 == y)
                        _map[x, y] = 1;
                    else
                    {
                        var r = pseudoRandom.Next(0, 100);
                        _map[x, y] = (r > _randomFillPercent) ? 1 : 0;
                    }

                }
            }
        }
    }
}
