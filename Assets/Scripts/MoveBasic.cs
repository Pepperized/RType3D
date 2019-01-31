using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasic : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }
}
