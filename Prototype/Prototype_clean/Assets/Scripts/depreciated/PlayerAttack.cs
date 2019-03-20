using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;

    private void Update()
    {
        if(timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange );

            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }









}


