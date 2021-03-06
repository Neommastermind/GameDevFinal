﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {

    private Enemy enemy;
    private Player player;
    private AudioSource playerAudio;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAudio = player.gameObject.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Sword"))
        {
            playerAudio.PlayOneShot(SoundManager.Instance.swordHit);
            enemy = other.gameObject.GetComponent<Enemy>();
        }
        else if (gameObject.CompareTag("EnemyWeapon") && !other.gameObject.CompareTag("Sword"))
        {
            enemy = gameObject.GetComponentInParent<Enemy>();

            if (!enemy.GetHasHit() && other.gameObject.CompareTag("Shield"))
            {
                enemy.SetHasHit(true);
                enemy.DealDamage(true);
            }
            else if (!enemy.GetHasHit() && other.gameObject.CompareTag("Player"))
            {
                enemy.SetHasHit(true);
                enemy.DealDamage(false);
            }
        }
    }

    public void DamageEnemy()
    {
        if (enemy != null)
        {
            int damage = player.GetFullDamage();

            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Light-Attack"))
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                enemy.TakeDamage((int)Mathf.Floor(damage * 1.5f));
            }
            //Make sure we reset the enemy
            enemy = null;
        }
    }
}
