using Assets.Code.Controllers;
using Assets.Code.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Auxiliary
{
    internal sealed class BulletsEmitter
    {
        private const float _delay = 1;
        private const float _startSpeed = 50.0f;

        private List<Bullet> _bullets = new List<Bullet>();

        private Transform _transform;
        private int _currentIndex;
        private float _timeTillNextBullet;

        public BulletsEmitter(List<BulletView> bullets, Transform transform)
        {
            _transform = transform;
            foreach (var bulletView in bullets)
                _bullets.Add(new Bullet(bulletView));
            
        }

        public void Update(float deltaTime, Vector2 direction)
        {
            if (_timeTillNextBullet > 0)
            {
                _timeTillNextBullet -= deltaTime;
            }
            else 
            {
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position,
                    direction * _startSpeed);

                ++ _currentIndex;

                if (_currentIndex >= _bullets.Count) _currentIndex = 0;
            }
        }

    }
}
