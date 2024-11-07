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
        animator.Play("player_idle");
    }

    public void PlayHorizontalAnimation()
    {
        animator.Play("player_moving");
    }

    public void PlayMovingDownAnimation()
    {
        animator.Play("player_moving_down");
    }

    public void PlayMovingUpAnimation()
    {
        animator.Play("player_moving_up");
    }
}
