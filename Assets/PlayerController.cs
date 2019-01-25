using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            rotationTarget.x = -25;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            rotationTarget.x = 25;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }
}
