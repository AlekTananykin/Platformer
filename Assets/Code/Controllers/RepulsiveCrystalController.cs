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
    internal sealed class RepulsiveCrystalController: IInitialization
    {

        private LevelObjectView _view;
        private GameObjectFabric _fabric;

        private Vector2 _position;

        internal RepulsiveCrystalController(GameObjectFabric fabric, Vector2 position)
        {
            _position = position;
            _fabric = fabric;
        }

        public void Initialize()
        {
            GameObject crystal = _fabric.CreateRepulsiveCrystal();
            Rigidbody2D rigidBody = crystal.GetComponent<Rigidbody2D>();

            _view = new LevelObjectView() { 
                Transform = rigidBody.transform, Rigidbody2D = rigidBody};

            _view.Transform.position = _position;
        }
    }
}
