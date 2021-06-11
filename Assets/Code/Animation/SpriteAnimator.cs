using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Animation
{
    internal class SpriteAnimator : IDisposable
    {
        public void Dispose()
        {
            _activeAnimations.Clear();
        }

        private class Animation
        {
            public Track Trak;
            public List<Sprite> Sprites;
            public bool Loop = false;
            public float Speed = 0f;
            public float Counter = 1;
            public bool Sleeps;

            public void Update()
            {
                if (Sleeps)
                    return;

                Counter += Time.deltaTime* Speed;

                if (Loop)
                {
                    while (Counter > Sprites.Count)
                        Counter -= Sprites.Count;
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count - 1;
                    Sleeps = true;
                }
            }
        }

        private SpriteAnimationConfig _config;

        private Dictionary<SpriteRenderer, Animation> _activeAnimations =
            new Dictionary<SpriteRenderer, Animation>();

        internal SpriteAnimator(SpriteAnimationConfig config)
        {
            _config = config;
        }

        internal void StartAnimation(
            SpriteRenderer spriteRenderer,Track track,  bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(
                spriteRenderer, out Animation animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;

                if (track != animation.Trak)
                {
                    animation.Trak = track;
                    animation.Sprites = _config.Sequences.Find(
                            sequence => track == sequence.Track).Sprites;

                    animation.Counter = 0;
                }
            }
            else 
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                     Trak = track,
                    Sprites = _config.Sequences.Find(sequence=>sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed,
                    Counter = 0,
                    Sleeps = false
                });
            }
        }

        internal void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimations.ContainsKey(spriteRenderer))
                _activeAnimations.Remove(spriteRenderer);
        }

        internal void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();
                animation.Key.sprite = 
                    animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }

    }
}
