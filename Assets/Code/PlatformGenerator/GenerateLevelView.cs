using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.PlatformGenerator
{
    public class GenerateLevelView : MonoBehaviour
    {
        [SerializeField]
        private Tilemap _tileMapGround;

        [SerializeField]
        private Tile _tileGround;

        [SerializeField]
        private int _mapWidth;

        [SerializeField]
        private int _mapHeight;

        [SerializeField]
        private int _factorSmooth;

        [SerializeField]
        [Range(0, 100)]
        private int _randomFillPercent;

        public Tilemap TileMapGround => _tileMapGround;

        public Tile TileGround => _tileGround;

        public int MapWidth => _mapWidth;
        public int MapHeight => _mapHeight;

        public int FactorSmooth => _factorSmooth;
        public int RandomFillPercent => _randomFillPercent;

    }
}
