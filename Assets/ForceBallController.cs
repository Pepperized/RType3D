using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBallController : MonoBehaviour
{
    public float speed = 5;

    bool isAttached = false;
    bool isRequested = true;

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

    private Transform me;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        me = transform;
        player = FindObjectOfType<PlayerController>().transform;
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
    }
}
