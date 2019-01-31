using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100;
    private float hp;
    public int collisionLayer = 10;
    public GameObject destroyEffect;
    public AudioSource destroySFX;

    private Transform children;
    private CapsuleCollider collider;
    private PlayerController controls;

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
        controls = transform.GetComponent<PlayerController>();
        collider = transform.GetComponent<CapsuleCollider>();
        children = transform.Find("Children");
        HP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == collisionLayer)
        {
            OnDeath();
        }
        else if (other.tag == "EnemyBullet")
        {
            float damage = other.GetComponent<BulletInfo>().damage;
            HP -= damage;
        } 
    }

    void OnDeath()
    {
        collider.enabled = false;
        controls.enabled = false;
        children.gameObject.SetActive(false);
        GameObject particle = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        destroySFX.Play();
        Destroy(particle, 5);
    }
}
