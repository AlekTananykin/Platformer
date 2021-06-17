using Assets.Code.Auxiliary;
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

        private float _xAxisInput;
        private bool _doJump;

        private ContactsPoller _contactPoller;
        private float _groundLevel = -2f;

        public Transform Transform => _view.transform;

        internal HeroController(GameObjectFabric gameObjectFabric, IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _gameObjectFabric = gameObjectFabric;
            _model = new HeroModel();
        }

        public void Execute(float deltaTime)
        {
            _doJump = _playerInput.IsJump;
            _xAxisInput = _playerInput.MoveX;

            _contactPoller.Update();

            bool isGoSideWay = Mathf.Abs(_xAxisInput) > _model.MovingEpsilon;

            var newVelocity = 0f;
            if (isGoSideWay)
            {
                TurnToward();
                if ((_xAxisInput > _model.MovingEpsilon || !_contactPoller.HasLeftContacts) &&
                    (_xAxisInput < -_model.MovingEpsilon || !_contactPoller.HasRightContacts))
                    newVelocity = _model.WalkSpeed * /*deltaTime */ Math.Sign(_xAxisInput);
            }

            _view.RidgidBody.velocity =
                new Vector2(newVelocity, _view.RidgidBody.velocity.y);

            if (_contactPoller.IsGrounded && _doJump
                && Mathf.Abs(_view.RidgidBody.velocity.y) <= _model.JumpThreshuld)
            {
                _view.RidgidBody.AddForce(Vector2.up * _model.JumpForce);
            }

            if (_contactPoller.IsGrounded)
            {
                var track = isGoSideWay ? Track.walk : Track.idle;

                _spriteAnimator.StartAnimation(
                      _view.SpriteRenderer, track, true,
                      _model.AnimationSpeed);
            }
            else if (Math.Abs(_view.RidgidBody.velocity.y) > _model.FlyEpsilon)
            {
                _spriteAnimator.StartAnimation(
                    _view.SpriteRenderer, Track.jump, true, _model.AnimationSpeed);
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
            CapsuleCollider2D collider = AddCapsuleCollider(hero);
            _contactPoller = new ContactsPoller(collider);

            _animationsConfig =
                Resources.Load<SpriteAnimationConfig>("HeroSpriteAnimationConfig");
            _spriteAnimator = new SpriteAnimator(_animationsConfig);

            _spriteAnimator.StartAnimation(
                _view.SpriteRenderer, Track.walk, true, _model.AnimationSpeed);
        }

        private CapsuleCollider2D AddCapsuleCollider(GameObject hero)
        {
            var collider = hero.AddComponent<CapsuleCollider2D>();
            collider.isTrigger = false;
            collider.usedByEffector = false;
            collider.offset = new Vector2(-0.2f, 1.1f);
            collider.size = new Vector2(0.8f, 2.1f);

            collider.direction = CapsuleDirection2D.Vertical;
            return collider;
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
            _view.SpriteRenderer.flipX = _xAxisInput < 0;
        }

    }
}
