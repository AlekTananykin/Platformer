using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Views
{
    class BulletView
    {
        public Transform Transform;
        public Rigidbody2D Rigidbody;

        internal void SetVisible(bool isVisible)
        {
            Transform.gameObject.SetActive(isVisible);
        }
    }
}
