using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public List<GameObject> enemiesTemplates = new List<GameObject>();
    public Animator animator;
    private void OnEnable()
    {
    
    }
    private void OnDisable()
    {
        
    }
    public void SetViewEnemy(int index)
    {
        GameObject go = Instantiate(enemiesTemplates[index], transform);
        animator = go.GetComponent<Animator>();        
    }
    public void Walk()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Death", false);
        animator.SetBool("Idle", false);
    }
    public void Idle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Death", false);
        animator.SetBool("Idle", true);
    }
    public void Attack()
    {        
        animator.SetTrigger("Attack");        
    }
    public void Hurt()
    {       
        animator.SetTrigger("Hurt");     
    }

    public void Death()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Death", true); 
    }
}
