using Assets.Code.Auxiliary;
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

        BulletsEmitter _bulletsEmitter;

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

            _bulletsEmitter.Update(deltaTime, dir);
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

            List<BulletView> bullets = CreateBullets(5);
            _bulletsEmitter = new BulletsEmitter(bullets, _view.Transform);
        }

        private List<BulletView> CreateBullets(int bulletsCount)
        {
            GameObject bullet = _fabric.CreateBullet();
            List<BulletView> bullets = new List<BulletView>();
            for (int i = 0; i < bulletsCount; ++i)
            {
                bullets.Add(CreateBullet());
            }

            return bullets;
        }

        private BulletView CreateBullet()
        {
            GameObject bullet = _fabric.CreateBullet();
            BulletView bulletView = new BulletView()
            {
                Transform = bullet.transform,
                Rigidbody = CreateBulletRigidBody(bullet)
            };
            return bulletView;
        }

        private Rigidbody2D CreateBulletRigidBody(GameObject bullet)
        {
            Rigidbody2D rigidbody = bullet.AddComponent<Rigidbody2D>();

            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.centerOfMass = new Vector2(0, 0);
            rigidbody.freezeRotation = true;
            rigidbody.isKinematic = false;
            rigidbody.mass = 1;

            return rigidbody;
        }
    }
}
