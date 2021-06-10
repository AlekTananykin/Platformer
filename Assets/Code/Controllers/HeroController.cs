using Assets.Code.Animation;
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
    internal class HeroController: IExecute
    {
        private HeroView _heroView;
        private SpriteAnimationConfig _animationsConfig;
        private SpriteAnimator _spriteAnimator;

        internal HeroController(GameObjectFabric gameObjectFabric)
        {
            GameObject hero = gameObjectFabric.CreateCharecter();
            _heroView = hero.AddComponent<HeroView>();
            _heroView.SpriteRenderer = hero.GetComponentInChildren<SpriteRenderer>();


            _animationsConfig =
                Resources.Load<SpriteAnimationConfig>("SpriteAnimationConfig");
            _spriteAnimator = new SpriteAnimator(_animationsConfig);

            _spriteAnimator.StartAnimation(_heroView.SpriteRenderer, Track.run, true, 100);
        }

        public void Execute(float deltaTime)
        {
            _spriteAnimator.Update();
        }
    }
}
