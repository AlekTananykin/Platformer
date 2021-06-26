using Assets.Code.Models;
using Assets.Code.Views;
using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    class StalkerAI
    {
        private readonly LevelObjectView _view;
        private readonly StalkerAIModel _model;
        private readonly Seeker _seeker;
        private readonly Transform _target;

        public StalkerAI(LevelObjectView view, StalkerAIModel model, 
            Seeker seeker, Transform target)
        {
            _view = null != view ? 
                view : throw new ArgumentNullException(nameof(view));
            _model = null != model ? 
                model : throw new ArgumentNullException(nameof(model));
            _seeker = null != seeker ? 
                seeker : throw new ArgumentNullException(nameof(seeker));
            _target = (Transform)target ?? throw new ArgumentNullException(nameof(target));
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * 
                Time.fixedDeltaTime;

            _view.Rigidbody2D.velocity = newVelocity;
        }

        public void RecalculatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(
                    _view.Rigidbody2D.position, _target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;
            _model.UpdatePath(p);
        }

    }
}
