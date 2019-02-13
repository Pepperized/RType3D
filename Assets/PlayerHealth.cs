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
    public GameObject forceBallObj;

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
        }else if (other.tag == "Force")
        {
            ForceBallController forceBallController = other.GetComponent<ForceBallController>();
            bool attached = forceBallController.IsAttached;
            if (!attached) forceBallController.IsAttached = true;
        } else if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);
            ForceBallController fbc = FindObjectOfType<ForceBallController>();
            if (fbc == null)
            {
                GameObject go = Instantiate(forceBallObj);

            } else
            {
                switch (fbc.Level)
                {
                    case ForceBallLevel.Level1:
                        fbc.Level = ForceBallLevel.Level2;
                        break;
                    case ForceBallLevel.Level2:
                        fbc.Level = ForceBallLevel.Level3;
                        break;
                    case ForceBallLevel.Level3:
                        fbc.Level = ForceBallLevel.Level3;
                        break;
                    default:
                        break;
                }
            }
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
