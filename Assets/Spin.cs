using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 4;
    public Vector3 dir;

    private Transform me;

    // Start is called before the first frame update
    void Start()
    {
        me = transform;
    }

    // Update is called once per frame
    void Update()
    {
        me.Rotate(dir, speed * Time.deltaTime);
    }
}
