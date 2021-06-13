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
    internal class HeroController: IExecute, IInitialization
    {
        private HeroView _view;
        private HeroModel _model;
        private SpriteAnimationConfig _animationsConfig;
        private SpriteAnimator _spriteAnimator;
        private GameObjectFabric _gameObjectFabric;

        private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _xAxisInput;
        private bool _doJump;
        

        internal HeroController(GameObjectFabric gameObjectFabric)
        {
            
            _gameObjectFabric = gameObjectFabric;
            _model = new HeroModel();
        }

        public void Execute(float deltaTime)
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _doJump = Input.GetAxis("Vertical") > 0;

            if (IsGrounded())
            {
                if (_doJump)
                {
                    
                }

                if (0 == _xAxisInput)
                {
                    _spriteAnimator.StartAnimation(
                        _view.SpriteRenderer, Track.idle, true, _model.AnimationSpeed);
                }
                else
                {
                    TurnToward();
                    MoveToward(deltaTime, _model.WalkSpeed);

                    _spriteAnimator.StartAnimation(
                        _view.SpriteRenderer, Track.walk, true,
                        _model.AnimationSpeed);
                }
            }


            _spriteAnimator.Update(deltaTime);
        }

        public void Initialize()
        {
            GameObject hero = _gameObjectFabric.CreateCharecter();
            _view = hero.AddComponent<HeroView>();
            _view.SpriteRenderer = hero.GetComponentInChildren<SpriteRenderer>();
            _view.Transform = hero.transform;
            _view.Transform.position = new Vector3(-6f, _model.GroundLevel, 0f);


            _animationsConfig =
                Resources.Load<SpriteAnimationConfig>("HeroSpriteAnimationConfig");
            _spriteAnimator = new SpriteAnimator(_animationsConfig);

            _spriteAnimator.StartAnimation(
                _view.SpriteRenderer, Track.walk, true, _model.AnimationSpeed);
        }

        private void TurnToward()
        {
            _view.Transform.localScale = 
                (_xAxisInput < 0) ? _leftScale : _rightScale;
        }

        private void MoveToward(float deltaTime, float speed)
        {
            _view.transform.position += Vector3.right * 
                deltaTime * speed * ((_xAxisInput < 0)? -1.0f: 1.0f);
        }

        private bool IsGrounded()
        {
            return _view.Transform.position.y < _model.GroundLevel + float.Epsilon && _model.YVelocity <= 0;
        }

    }
}
