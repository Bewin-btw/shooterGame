using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;

    public bool isDead; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void Die()
    {
        if (!isDead) 
        {
            isDead = true;
            animator.SetTrigger("DIE"); 
            navAgent.isStopped = true; 
            Destroy(gameObject, 3f); 
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; 

        HP -= damageAmount;

        if (HP <= 0)
        {
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2"); 
            }

            Die(); 

            SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.zombieDeath);

            GameManager.Instance.AddScore(1);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
            SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.zombieHurt);
        }
    }
}