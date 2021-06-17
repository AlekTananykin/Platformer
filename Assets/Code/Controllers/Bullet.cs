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
    class Bullet
    {
        private BulletView _view;

        public Bullet(BulletView view)
        {
            _view = view;
            _view.SetVisible(false);
        }

        public void Throw(Vector2 position, Vector2 velocity)
        {
            _view.SetVisible(false);
            _view.Transform.position = position;
            _view.Rigidbody.velocity = Vector2.zero;
            _view.Rigidbody.angularVelocity = 0;
            _view.Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
            _view.SetVisible(true);
        }
    }
}
