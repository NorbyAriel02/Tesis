using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleAnimation : MonoBehaviour
{
    public Animator animator;
    private bool isWalk = false;
    void Start()
    {
        
    }

    public void StartWalk()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Attack", false);
        isWalk = true;
    }
    public void StartIdle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
        isWalk = false;
    }
    public void StartAttack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);
        isWalk = false;
    }
}
