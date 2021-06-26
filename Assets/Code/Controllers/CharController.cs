using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal abstract class CharController
    {
        protected CapsuleCollider2D AddCapsuleCollider(
            GameObject charecter, Vector2 offset, Vector2 size)
        {
            var collider = charecter.AddComponent<CapsuleCollider2D>();
            collider.isTrigger = false;
            collider.usedByEffector = false;
            collider.offset = offset;
            collider.size = size;

            collider.direction = CapsuleDirection2D.Vertical;
            return collider;
        }

        protected Rigidbody2D AddRigidBody(GameObject hero, float mass, string name)
        {
            var rigidbody = hero.AddComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.centerOfMass = new Vector2(0, 0);
            rigidbody.freezeRotation = true;
            rigidbody.isKinematic = false;
            rigidbody.mass = mass;
            rigidbody.name = name;
            return rigidbody;
        }
    }
}
