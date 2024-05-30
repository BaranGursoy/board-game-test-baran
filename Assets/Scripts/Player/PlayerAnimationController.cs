using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Jumping = Animator.StringToHash("Jumping");
    
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        GameActions.PlayerCanMove += PlayJumpingAnimation;
        GameActions.PlayerStopped += PlayIdleAnimation;
    }

    private void OnDestroy()
    {
        GameActions.PlayerCanMove -= PlayJumpingAnimation;
        GameActions.PlayerStopped -= PlayIdleAnimation;
    }

    private void PlayIdleAnimation()
    {
        playerAnimator.Play(Idle);
    }

    private void PlayJumpingAnimation(int dummyTileCount, bool dummyForwards)
    {
        playerAnimator.Play(Jumping);
    }
}
