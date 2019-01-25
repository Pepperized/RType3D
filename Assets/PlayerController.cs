using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotSpeed = 1;
    public float fireDelay = 0.5f;
    public Transform cannon;
    public GameObject shotPrefab;
    public PlayerPerspective perspective = PlayerPerspective.TopDown;

    private float tToNextShot = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (perspective)
        {
            case PlayerPerspective.TopDown:
                TopDownUpdate();
                break;
            case PlayerPerspective.Side:
                break;
            case PlayerPerspective.FromBehind:
                break;
            default:
                break;
        }


        if (Input.GetAxis("Fire1") > 0 && tToNextShot <= 0)
        {
            tToNextShot = fireDelay;
            Transform bullet = Instantiate(shotPrefab, cannon.position, cannon.rotation).transform;
            bullet.GetComponent<MoveBasic>().direction = cannon.up;
        }

        tToNextShot -= Time.deltaTime;
    }

    void TopDownUpdate()
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

public enum PlayerPerspective
{
    TopDown,
    Side,
    FromBehind
}
