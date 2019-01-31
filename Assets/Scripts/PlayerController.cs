using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotSpeed = 1;
    public float fireDelay = 0.5f;
    public AudioSource shotSFX;
    public Transform cannon;
    public GameObject shotPrefab;

    public Vector3 fromBehindPos;

    public float overrideDuration = 1;

    private float tToNextShot = 0;
    private bool overridingControl = false;
    private float overrideUntil = 0;

    private float startTime;
    private Vector3 startPos;
    private Vector3 endPos;

    private Quaternion startRot;
    private Quaternion defaultRot;

    private float travelTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        defaultRot = transform.rotation;
        SceneManagerScript.instance.PerspectiveChanged += ChangePerspective;
    }

    // Update is called once per frame
    void Update()
    {
        if (!overridingControl)
        {
            switch (SceneManagerScript.instance.PlayerPerspective)
            {
                case PlayerPerspective.TopDown:
                    TopDownUpdate();
                    break;
                case PlayerPerspective.Side:
                    SideUpdate();
                    break;
                case PlayerPerspective.FromBehind:
                    FromBehindUpdate();
                    break;
                default:
                    break;
            }


            if (Input.GetAxis("Fire1") > 0 && tToNextShot <= 0)
            {
                shotSFX.Play();
                tToNextShot = fireDelay;
                Transform bullet = Instantiate(shotPrefab, cannon.position, cannon.rotation).transform;
                bullet.GetComponent<MoveBasic>().direction = cannon.up;
            }

            tToNextShot -= Time.deltaTime;
        } else
        {
            float timeFromStart = Time.time - startTime;
            float percentage = timeFromStart / travelTime;
            if (percentage >= 0 && percentage <= 1)
            {

                transform.position = Vector3.Lerp(startPos, endPos, percentage);
                transform.rotation = Quaternion.Slerp(startRot, defaultRot, percentage);
            } else if (percentage > 1)
            {
                overridingControl = false;
            }
        }
    }

    void TopDownUpdate()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0 && transform.position.x < 65)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            rotationTarget.x = -25;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && transform.position.x > -65)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            rotationTarget.x = 25;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && transform.position.z < 45)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && transform.position.z > -24)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }

    void SideUpdate()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0 && transform.position.z < 115)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && transform.position.z > -22)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && transform.position.y < 52)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && transform.position.y > -22)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }

    void FromBehindUpdate()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0 && transform.position.x < 14)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            rotationTarget.x = -25;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && transform.position.x > -10)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            rotationTarget.x = 25;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && transform.position.y < 12)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && transform.position.y > 0)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }

    void ChangePerspective(object sender, EventArgs e)
    {
        overridingControl = true;
        overrideUntil = Time.time + overrideDuration;
            startTime = Time.time;
            
            startPos = transform.position;
            endPos = fromBehindPos;
            startRot = transform.rotation;
    }
}

public enum PlayerPerspective
{
    TopDown,
    Side,
    FromBehind
}
