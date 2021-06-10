using Assets.Code.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _back;
    SpriteAnimationConfig _animationsConfig;
    SpriteAnimator _spriteAnimator;

    void Start()
    {
        _animationsConfig = 
            Resources.Load<SpriteAnimationConfig>("SpriteAnimationConfig");
        _spriteAnimator = new SpriteAnimator(_animationsConfig);
    }

    
    void Update()
    {
        _spriteAnimator.Update();
    }

    void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
