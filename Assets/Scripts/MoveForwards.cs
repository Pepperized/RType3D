using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float speed = 1;

    private Transform me;

    // Start is called before the first frame update
    void Start()
    {
        me = transform;
    }

    // Update is called once per frame
    void Update()
    {
        me.position += me.up * speed * Time.fixedDeltaTime;
    }
}
