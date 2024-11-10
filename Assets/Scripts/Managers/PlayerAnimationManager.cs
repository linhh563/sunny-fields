using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator {get; private set;}

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        animator.Play(PlayerAnimation.PlayerIdle);
    }

    public void PlayHorizontalAnimation()
    {
        animator.Play(PlayerAnimation.PlayerMoving);
    }

    public void PlayMovingDownAnimation()
    {
        animator.Play(PlayerAnimation.PlayerMovingDown);
    }

    public void PlayMovingUpAnimation()
    {
        animator.Play(PlayerAnimation.PlayerMovingUp);
    }

    public void PlayIdleDownAnimation()
    {
        animator.Play(PlayerAnimation.PlayerIdleDown);
    }

    public void PlayIdleUpAnimation()
    {
        animator.Play(PlayerAnimation.PlayerIdleUp);
    }
}
