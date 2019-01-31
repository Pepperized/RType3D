using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasic : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
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
        me.position += direction * speed * Time.fixedDeltaTime;
    }
}
