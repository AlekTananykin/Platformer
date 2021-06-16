using Assets.Code.Interfaces;
using Assets.Code.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class PlatformController : IInitialization
    {
        private GameObjectFabric _gameObjectFabric;

        internal PlatformController(GameObjectFabric gameObjectFabric)
        {
            _gameObjectFabric = gameObjectFabric;
        }

        public void Initialize()
        {
            GameObject platform =_gameObjectFabric.CreatePlatform();

            AddCollider(platform);
        }

        private void AddCollider(GameObject platform)
        {
            var collider = platform.AddComponent<BoxCollider2D>();
            collider.isTrigger = false;
            collider.offset = new Vector2(0, -0.7f);
            collider.size = new Vector2(6.25f, 1.1f);
        }
    }
}
