using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    public float maxHP = 10;

    public GameObject OnDeathEffect;

    private float hp;

    private float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                OnDeath();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            float damage = other.GetComponent<BulletInfo>().damage;
            HP -= damage;
            Destroy(other.gameObject);
        }
    }

    private void OnDeath()
    {
        GameObject particle = Instantiate(OnDeathEffect, transform.position, Quaternion.identity);
        Destroy(particle, 5);
        Destroy(gameObject);
    }
}
