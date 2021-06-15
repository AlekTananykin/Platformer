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
    class OgreController : IExecute, IInitialization
    {
        private OgreView _view;
        private OgreModel _model;
        private GameObjectFabric _gameObjectFabric;

        SpriteAnimator _spriteAnimator;

        internal OgreController(GameObjectFabric gameObjectFabric)
        {
            _gameObjectFabric = gameObjectFabric;
            _model = new OgreModel();

            
        }

        public void Execute(float deltaTime)
        {
            _spriteAnimator.Update(deltaTime);
        }

        public void Initialize()
        {
            GameObject ogreView = _gameObjectFabric.CreateOgre();
            SpriteRenderer renderer = ogreView.GetComponentInChildren<SpriteRenderer>();

            _view = new OgreView() { SpriteRenderer = renderer };

            SpriteAnimationConfig config =
                (SpriteAnimationConfig)Resources.Load("OgreSpriteAnimationConfig");

            _spriteAnimator = new SpriteAnimator(config);

            _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.idle, true, _model.AnimationSpeed);
        }
    }
}
