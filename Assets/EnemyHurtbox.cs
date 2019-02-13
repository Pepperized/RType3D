using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    public float maxHP = 10;

    public GameObject OnDeathEffect;
    public AudioSource OnDeathSFX;

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
            BulletInfo info = other.GetComponent<BulletInfo>();
            HP -= info.damage;
            if (info.destroyOnHit) Destroy(other.gameObject);
        }else if (other.tag == "Force")
        {
            HP -= 100 * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Force")
        {
            HP -= 100 * Time.deltaTime;
        }
    }

    private void OnDeath()
    {
        GameObject particle = Instantiate(OnDeathEffect, transform.position, Quaternion.identity);
        OnDeathSFX.Play();
        Destroy(particle, 5);
        Destroy(gameObject);
    }
}
