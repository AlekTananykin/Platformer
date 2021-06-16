using Assets.Code.Configs;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using Assets.Code.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal class HeroController : IExecute, IInitialization
    {
        private HeroView _view;
        private HeroModel _model;
        private SpriteAnimationConfig _animationsConfig;
        private SpriteAnimator _spriteAnimator;
        private GameObjectFabric _gameObjectFabric;
        private IPlayerInput _playerInput;

        private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _xAxisInput;
        private bool _doJump;
        private const float _jumpForce = 700;

        private readonly float _groundLevel = -2f;

        public Transform Transform => _view.transform;

        internal HeroController(GameObjectFabric gameObjectFabric, IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _gameObjectFabric = gameObjectFabric;
            _model = new HeroModel();
        }

        public void Execute(float deltaTime)
        {
            _xAxisInput = _playerInput.MoveX;

            _doJump = _playerInput.IsJump;
            bool isGoSideWay = Mathf.Abs(_xAxisInput) > _model.MovingEpsilon;

            if (isGoSideWay)
            {
                TurnToward();
                MoveToward(deltaTime, _model.WalkSpeed);
            }


            if (IsGrounded())
            {
                if (_doJump)
                {
                    _view.RidgidBody.AddForce(Vector2.up * _jumpForce);

                    _spriteAnimator.StartAnimation(
                        _view.SpriteRenderer, Track.idle, true,
                        _model.AnimationSpeed);
                }
                else if (0 > _model.YVelocity)
                {
                    _model.YVelocity = 0;
                    _view.Transform.position = new Vector3(
                        _view.Transform.position.x,
                        _groundLevel,
                        _view.Transform.position.z);

                    _spriteAnimator.StartAnimation(
                        _view.SpriteRenderer, Track.idle, true,
                        _model.AnimationSpeed);
                }
                else if (isGoSideWay)
                {
                    _spriteAnimator.StartAnimation(
                   _view.SpriteRenderer, Track.walk, true,
                   _model.AnimationSpeed);
                }
                else
                {
                    _spriteAnimator.StartAnimation(
                   _view.SpriteRenderer, Track.idle, true,
                   _model.AnimationSpeed);
                }
            }
            else
            {
                if (Mathf.Abs(_model.YVelocity) > _model.FlyEpsilon)
                {
                    _spriteAnimator.StartAnimation(
                        _view.SpriteRenderer, Track.jump, true, _model.AnimationSpeed);

                }
                _model.YVelocity += _model.Gravity * deltaTime;
                //_view.Transform.position += Vector3.up * deltaTime * _model.YVelocity;
            }

            _spriteAnimator.Update(deltaTime);
        }

        public void Initialize()
        {
            GameObject hero = _gameObjectFabric.CreateCharecter();

            hero.transform.position = new Vector3(-5f, 0f, 0);

            _view = hero.AddComponent<HeroView>();
            _view.SpriteRenderer = hero.GetComponentInChildren<SpriteRenderer>();
            _view.Transform = hero.transform;
            _view.Transform.position = new Vector3(-6f, _groundLevel, 0f);

            _view.RidgidBody = AddRigidBody(hero);
            AddCapsuleCollider(hero);


            _animationsConfig =
                Resources.Load<SpriteAnimationConfig>("HeroSpriteAnimationConfig");
            _spriteAnimator = new SpriteAnimator(_animationsConfig);

            _spriteAnimator.StartAnimation(
                _view.SpriteRenderer, Track.walk, true, _model.AnimationSpeed);
        }

        private void AddCapsuleCollider(GameObject hero)
        {
            var collider = hero.AddComponent<CapsuleCollider2D>();
            collider.isTrigger = false;
            collider.usedByEffector = false;
            collider.offset = new Vector2(-0.2f, 1.1f);
            collider.size = new Vector2(0.8f, 2.1f);
            
            collider.direction = CapsuleDirection2D.Vertical;
        }

        private Rigidbody2D AddRigidBody(GameObject hero)
        {
            var rigidbody = hero.AddComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.centerOfMass = new Vector2(0, 0);
            rigidbody.freezeRotation = true;
            rigidbody.isKinematic = false;
            rigidbody.mass = 60;
            rigidbody.name = "Hero";
            return rigidbody;
        }

        private void TurnToward()
        {
            _view.Transform.localScale =
                (_xAxisInput < 0) ? _leftScale : _rightScale;
        }

        private void MoveToward(float deltaTime, float speed)
        {
            _view.transform.position += Vector3.right *
                deltaTime * speed * ((_xAxisInput < 0) ? -1.0f : 1.0f);
        }

        private bool IsGrounded()
        {
            return _view.Transform.position.y < _groundLevel + 0.01
                && _model.YVelocity <= 0;
        }

    }
}
