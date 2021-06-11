using Assets.Code.Animation;
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
    internal class HeroController: IExecute
    {
        private HeroView _view;
        private HeroModel _model;
        private SpriteAnimationConfig _animationsConfig;
        private SpriteAnimator _spriteAnimator;

        internal HeroController(GameObjectFabric gameObjectFabric)
        {
            _model = new HeroModel() { Speed = 10 };

            GameObject hero = gameObjectFabric.CreateCharecter();
            _view = hero.AddComponent<HeroView>();
            _view.SpriteRenderer = hero.GetComponentInChildren<SpriteRenderer>();


            _animationsConfig =
                Resources.Load<SpriteAnimationConfig>("SpriteAnimationConfig");
            _spriteAnimator = new SpriteAnimator(_animationsConfig);

            _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.walk, true, _model.Speed);
        }

        public void Execute(float deltaTime)
        {
            _spriteAnimator.Update();
        }
    }
}
