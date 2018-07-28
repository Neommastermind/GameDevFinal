using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Sword")) { 
            if (other.gameObject.CompareTag("Enemy"))
            {
                int damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetFullDamage();
                Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();

                if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Light-Attack"))
                {
                    enemy.TakeDamage(damage);
                }
                else
                {
                    enemy.TakeDamage((int)Mathf.Floor(damage * 1.25f));
                }
            }
        }
        else if (gameObject.CompareTag("EnemyWeapon"))
        {
            Enemy enemy = gameObject.GetComponentInParent<Enemy>();

            if (other.gameObject.CompareTag("Player"))
            {
                enemy.DealDamage(false);   
            }
            else if(other.gameObject.CompareTag("Shield"))
            {
                enemy.DealDamage(true);
            }
        }
    }
}
