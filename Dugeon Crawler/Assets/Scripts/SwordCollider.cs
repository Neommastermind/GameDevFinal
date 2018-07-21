using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            int damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetFullDamage();
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();

            if (GameObject.FindGameObjectWithTag("Sword").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Light-Attack"))
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                enemy.TakeDamage((int)Mathf.Floor(damage * 1.25f));
            }
        }
    }
}
