using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    public float damage;
    public bool hasHitEffect = false;
    public GameObject hitEffect;

    private void OnDestroy()
    {
        if (hasHitEffect)
        {
            GameObject particle = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(particle, 5);
        }
    }
}
