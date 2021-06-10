using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Auxiliary
{
    class ParalaxManager
    {
        private Transform _camera;
        private Transform _back;
        private Vector3 _backStartPosition;
        private Vector3 _cameraStartPosition;
        private const float _coef = 0.3f;


        public ParalaxManager(Transform camera, Transform back)
        {
            _camera = camera;
            _back = back;
            _backStartPosition = _back.transform.position;
            _cameraStartPosition = _camera.transform.position;
        }

        public void Update()
        {
            _back.position = _backStartPosition + (_camera.position -
                _cameraStartPosition) * _coef;
        }

    }
}
