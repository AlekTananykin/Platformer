using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectViewTrigger : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Transform Transform;

    public event EventHandler<HeroView> OnLevelObjectContact;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out HeroView heroView))
        {
            OnLevelObjectContact?.Invoke(this, heroView);
        }
    }
}
