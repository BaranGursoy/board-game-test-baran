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
        ActionHandler.PlayerCanMove += PlayJumpingAnimation;
        ActionHandler.PlayerStopped += PlayIdleAnimation;
    }

    private void PlayIdleAnimation()
    {
        playerAnimator.Play(Idle);
    }

    private void PlayJumpingAnimation(int dummy)
    {
        playerAnimator.Play(Jumping);
    }
}
