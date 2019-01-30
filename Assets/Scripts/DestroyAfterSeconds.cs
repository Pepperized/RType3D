using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float time;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, time);
    }
}