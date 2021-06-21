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
    class BackGroundController : IExecute, IInitialization
    {
        GameObjectFabric _fabric;
        GameObject _backView;

        public BackGroundController(GameObjectFabric fabric)
        {
            _fabric = fabric;
        }

        public void Execute(float deltaTime)
        {
            
        }

        public void Initialize()
        {
            _backView =
                _fabric.CreateForestBackground();


        }
    }
}
