using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectViewTrigger : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Transform Transform;
    public Rigidbody2D RidgidBody;

    public event EventHandler<LevelObjectViewTrigger> OnLevelObjectContact;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out LevelObjectViewTrigger levelObject))
        {
            OnLevelObjectContact?.Invoke(this, levelObject);
        }
    }
}
