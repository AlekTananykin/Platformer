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
    class OgreController : CharController, IExecute, IInitialization
    {
        private OgreView _view;
        private OgreModel _model;
        private SpriteAnimationConfig _animationsConfig;
        private SpriteAnimator _spriteAnimator;
        private GameObjectFabric _gameObjectFabric;

        internal OgreController(GameObjectFabric gameObjectFabric, Vector2 initPosition)
        {
            _gameObjectFabric = gameObjectFabric;
            _model = new OgreModel();

            _model.InitPosition = initPosition;
        }

        public void Execute(float deltaTime)
        {
            _spriteAnimator.Update(deltaTime);
        }

        public void Initialize()
        {
            GameObject ogre = _gameObjectFabric.CreateOgre();
            SpriteRenderer renderer = ogre.GetComponentInChildren<SpriteRenderer>();

            _view = ogre.AddComponent<OgreView>();
            _view.SpriteRenderer = ogre.GetComponentInChildren<SpriteRenderer>();

            _view.RidgidBody = AddRigidBody(ogre, 60f, "Ogre");
            _view.Transform = _view.RidgidBody.transform;
            _view.Transform.position = new Vector3(8f, 2f, 0f);

            Vector2 colliderOffset = new Vector2(0.1f, -0.88f);
            Vector2 colliderSize = new Vector2(1.3f, 1.9f);
            AddCapsuleCollider(
                ogre, colliderOffset, colliderSize);

            SpriteAnimationConfig config =
                (SpriteAnimationConfig)Resources.Load("OgreSpriteAnimationConfig");

            _spriteAnimator = new SpriteAnimator(config);

            _spriteAnimator.StartAnimation(
                _view.SpriteRenderer, Track.walk, true, _model.AnimationSpeed);
        }
    }
}
