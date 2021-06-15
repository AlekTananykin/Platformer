using Assets.Code.Configs;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    class CannonController : IExecute, IInitialization
    {
        private Transform _muzzleTransform;
        private Transform _aimTransform;
        private GameObjectFabric _fabric;
        private CannonView _view;
        private CannonModel _model;
        private CannonConfig _config;


        internal CannonController(GameObjectFabric fabric)
        {
            _fabric = fabric;
        }

        internal void SetAim(Transform aim)
        {
            _aimTransform = aim;
        }

        public void Execute(float deltaTime)
        {

            Vector3 dir = _aimTransform.position - _muzzleTransform.position;

            var angle = Vector3.Angle(Vector3.down, dir);
            var axis = Vector3.Cross(Vector3.down, dir);
            _muzzleTransform.rotation = Quaternion.AngleAxis(angle, axis);
        }

        public void Initialize()
        {
            _model = new CannonModel()
            {
                Position = new Vector3(0f, 2f, 0f)
            };

            GameObject cannonGameObject = _fabric.CreateCannon();
            _view = new CannonView()
            {
                Transform = cannonGameObject.transform,
                Muzzle = cannonGameObject.GetComponentInChildren<SpriteRenderer>()
            };
            _view.Transform.position = _model.Position;
            _muzzleTransform = _view.Transform;
            _config = Resources.Load<CannonConfig>("CannonConfig");


        }
    }
}
