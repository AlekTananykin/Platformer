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
    class BackGroundController : IInitialization
    {
        GameObjectFabric _fabric;
        GameObject _backView;
        float _length;
        Vector2 _position;
        bool _isFirstSet;

        public BackGroundController(GameObjectFabric fabric, Vector2 initPostion)
        {
            _fabric = fabric;
            _length = -1;
            _position = initPostion;

        }

        public void Initialize()
        {
            _backView =
                _fabric.CreateForestBackground();

            SpriteRenderer renderer = _backView.GetComponent<SpriteRenderer>();
            _length = renderer.size.x;


        }

        public void SetPosition(Vector2 position)
        {
        }
    }
}
