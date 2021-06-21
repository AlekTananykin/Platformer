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
    class CameraController : IInitialization
    {

        private GameObjectFabric _gameObjectFabric;
        private Transform _cameraTransform;

        public CameraController(GameObjectFabric gameObjectFabric)
        {
            _gameObjectFabric = gameObjectFabric;
        }

        public void Initialize()
        {
            GameObject camera = _gameObjectFabric.CreateMainCamera();
            _cameraTransform = camera.transform; 
        }

        public void SetPosition(Vector2 position)
        {
            _cameraTransform.position = new Vector3(
                position.x, position.y, _cameraTransform.position.z);
        }
    }
}
