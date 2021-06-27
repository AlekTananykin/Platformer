using Assets.Code.Configs;
using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    public class StalkerAIModel
    {
        private readonly AIConfig _config;
        private Path _path;
        private int _currentPointIndex;

        public StalkerAIModel(AIConfig config)
        {
            _config = config;
        }


        public void UpdatePath(Path p)
        {
            _path = p;
            _currentPointIndex = 0;
        }

        public Vector2 CalculateVelocity(Vector2 fromPosition)
        {
            if (_path == null)
                return Vector2.zero;

            if (_currentPointIndex >= _path.vectorPath.Count)
                return Vector2.zero;

            var direction = ((Vector2)_path.vectorPath[_currentPointIndex] -
                fromPosition).normalized;

            var result = _config.speed * direction;
            var sqrDistance = Vector2.SqrMagnitude((Vector2)_path.vectorPath[_currentPointIndex] - fromPosition);

            if (sqrDistance <= _config.minSqrDistanceToTarget)
            {
                _currentPointIndex++;
            }

            return result;
        }


    }
}
