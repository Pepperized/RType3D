using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotSpeed = 1;
    public float fireDelay = 0.5f;
    public AudioSource shotSFX;
    public AudioSource chargingSFX;
    public AudioSource chargedShotSFX;
    public Transform cannon;
    public Slider chargeMeter;
    public GameObject shotPrefab;
    public GameObject chargedShotPrefab;
    public GameObject chargingEffect;
    public float chargeRate = 5;

    public Vector3 fromBehindPos;

    public float overrideDuration = 1;

    private Transform me;
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
        me = transform;
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

            Shot();

            ChargedShot();
           
        } else
        {
            float timeFromStart = Time.time - startTime;
            float percentage = timeFromStart / travelTime;
            if (percentage >= 0 && percentage <= 1)
            {

                me.position = Vector3.Lerp(startPos, endPos, percentage);
                me.rotation = Quaternion.Slerp(startRot, defaultRot, percentage);
            } else if (percentage > 1)
            {
                overridingControl = false;
            }
        }
    }

    void Shot()
    {
        if (Input.GetAxis("Fire1") > 0 && tToNextShot <= 0)
        {
            shotSFX.Play();
            tToNextShot = fireDelay;
            Transform bullet = Instantiate(shotPrefab, cannon.position, cannon.rotation).transform;
            bullet.GetComponent<MoveBasic>().direction = cannon.up;
        }

        tToNextShot -= Time.deltaTime;
    }

    private float charge = 0;
    private float chargeMax = 10;

    void ChargedShot()
    {
        chargeMeter.value = charge / chargeMax;

        if (Input.GetAxisRaw("Fire2") > 0)
        {
            chargingEffect.SetActive(true);
            if (charge == 0) chargingSFX.Play();
            charge += Time.deltaTime * chargeRate;
            if (charge > chargeMax) charge = chargeMax;
        } else if (charge >= chargeMax)
        {
            chargedShotSFX.Play();
            Transform bullet = Instantiate(chargedShotPrefab, cannon.position, cannon.rotation).transform;
            charge = 0;
        } else
        {
            chargingEffect.SetActive(false);
            charge = 0;
        }
    }

    void TopDownUpdate()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0 && me.position.x < 65)
        {
            me.position += Vector3.right * speed * Time.deltaTime;
            rotationTarget.x = -25;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && me.position.x > -65)
        {
            me.position += Vector3.left * speed * Time.deltaTime;
            rotationTarget.x = 25;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && me.position.z < 45)
        {
            me.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && me.position.z > -24)
        {
            me.position += Vector3.back * speed * Time.deltaTime;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        me.rotation = Quaternion.RotateTowards(me.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }

    void SideUpdate()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0 && me.position.z < 115)
        {
            me.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && me.position.z > -22)
        {
            me.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && me.position.y < 52)
        {
            me.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && me.position.y > -22)
        {
            me.position += Vector3.down * speed * Time.deltaTime;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        me.rotation = Quaternion.RotateTowards(me.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }

    void FromBehindUpdate()
    {
        Vector3 rotationTarget = new Vector3();
        rotationTarget.y = -90;

        if (Input.GetAxisRaw("Horizontal") > 0 && me.position.x < 14)
        {
            me.position += Vector3.right * speed * Time.deltaTime;
            rotationTarget.x = -25;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && me.position.x > -10)
        {
            me.position += Vector3.left * speed * Time.deltaTime;
            rotationTarget.x = 25;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && me.position.y < 12)
        {
            me.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && me.position.y > 0)
        {
            me.position += Vector3.down * speed * Time.deltaTime;
        }

        Quaternion targetQuat = Quaternion.Euler(rotationTarget);
        me.rotation = Quaternion.RotateTowards(me.rotation, targetQuat, Time.deltaTime * rotSpeed);
    }

    void ChangePerspective(object sender, EventArgs e)
    {
        overridingControl = true;
        overrideUntil = Time.time + overrideDuration;
            startTime = Time.time;
            
            startPos = me.position;
            endPos = fromBehindPos;
            startRot = me.rotation;
    }
}

public enum PlayerPerspective
{
    TopDown,
    Side,
    FromBehind
}
