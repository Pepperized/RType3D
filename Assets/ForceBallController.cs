using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBallController : MonoBehaviour
{
    public float speed = 5;
    public GameObject forceShot;
    public float fireDelay = 0.5f;

    GameObject level1parts;
    GameObject level2parts;
    GameObject level3parts;

    private float tToNextShot = 0;

    bool isAttached = false;
    bool isRequested = true;
    ForceBallLevel level = ForceBallLevel.Level1;

    public bool IsAttached
    {
        get { return isAttached; }
        set {
            isAttached = value;
            if (isAttached)
            {
                me.parent = player;
                Vector3 newPos = Vector3.zero;
                newPos.x = 6;
                newPos.y = -5;
                newPos.z = 0.2f;
                me.localPosition = newPos;
                
            } else
            {
                me.parent = null;
            }
        }
    }

    public ForceBallLevel Level
    {
        get { return level; }
        set { level = value;
            switch (level)
            {
                case ForceBallLevel.Level1:
                    level1parts.SetActive(true);
                    level2parts.SetActive(false);
                    level3parts.SetActive(false);
                    break;
                case ForceBallLevel.Level2:
                    level1parts.SetActive(false);
                    level2parts.SetActive(true);
                    level3parts.SetActive(false);
                    break;
                case ForceBallLevel.Level3:
                    level1parts.SetActive(false);
                    level2parts.SetActive(false);
                    level3parts.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    private Transform me;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        me = transform;
        player = FindObjectOfType<PlayerController>().transform;
        level1parts = me.Find("Level1").gameObject;
        level2parts = me.Find("Level2").gameObject;
        level3parts = me.Find("Level3").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttached && isRequested)
        {
            if (me.position.z > player.position.z + 5.5f)
            {
                Vector3 pos = player.position;
                pos.y -= 5;
                me.position = Vector3.MoveTowards(me.position, pos, Time.fixedDeltaTime * speed);
            }
        }

        if (Input.GetAxisRaw("Fire1") > 0 && Input.GetAxisRaw("Fire2") == 0 && tToNextShot <= 0)
        {
            tToNextShot = fireDelay;
            switch (level)
            {
                case ForceBallLevel.Level1:
                    Vector3 rot1, rot2;
                    rot1 = rot2 = Vector3.zero;
                    rot1.x = rot2.x = 90;
                    rot1.z += 20;
                    rot2.z += -20;
                    GameObject shot1 = Instantiate(forceShot, me.position, Quaternion.Euler(rot1));
                    GameObject shot2 = Instantiate(forceShot, me.position, Quaternion.Euler(rot2));
                    break;
                case ForceBallLevel.Level2:
                    break;
                case ForceBallLevel.Level3:
                    break;
                default:
                    break;
            }
        }
        tToNextShot -= Time.deltaTime;
    }
}

public enum ForceBallLevel
{
    Level1,
    Level2,
    Level3
}