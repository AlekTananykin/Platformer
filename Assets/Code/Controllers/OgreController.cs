using Assets.Code.Configs;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.Views;
using Pathfinding;
using UnityEngine;

namespace Assets.Code.Controllers
{
    class OgreController : CharController, IExecute, IInitialization
    {
        private OgreView _view;
        private OgreModel _model;
        private SpriteAnimator _spriteAnimator;
        private GameObjectFabric _gameObjectFabric;

        private ITarget _target;

        Seeker _seeker;
        AIPath _aiPath;
        AIDestinationSetter _destianetionSetter;

        internal OgreController(GameObjectFabric gameObjectFabric, Vector2 initPosition, 
            ITarget target)
        {
            _gameObjectFabric = gameObjectFabric;
            _model = new OgreModel();

            _model.InitPosition = initPosition;

            _target = target;
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

            AddSeeker(ogre);
            AddAIPath2D(ogre);
            AddAiDestinationSetter(ogre);

            SpriteAnimationConfig config =
                (SpriteAnimationConfig)Resources.Load("OgreSpriteAnimationConfig");

            _spriteAnimator = new SpriteAnimator(config);

            _spriteAnimator.StartAnimation(
                _view.SpriteRenderer, Track.walk, true, _model.AnimationSpeed);
        }

        private void AddAiDestinationSetter(GameObject ogre)
        {
            _destianetionSetter = ogre.AddComponent<AIDestinationSetter>();
            _destianetionSetter.target = _target.Transform;
        }

        private void AddAIPath2D(GameObject ogre)
        {
            _aiPath =  ogre.AddComponent<AIPath>();
            _aiPath.orientation = OrientationMode.YAxisForward;
            _aiPath.maxSpeed = 2;
            _aiPath.canMove = true;
            _aiPath.alwaysDrawGizmos = true;
            _aiPath.whenCloseToDestination = CloseToDestinationMode.Stop;
            _aiPath.rotationSpeed = 0;
        }

        private void AddSeeker(GameObject ogre)
        {
            _seeker = ogre.AddComponent<Seeker>();
        }
    }
}
