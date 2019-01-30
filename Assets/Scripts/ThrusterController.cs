using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    Vector3 velocity;
    Vector3 previous;

    public GameObject DownThruster1;
    public GameObject DownThruster2;
    public GameObject UpThruster1;
    public GameObject UpThruster2;

    // Start is called before the first frame update
    void Start()
    {
        previous = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (transform.position - previous) / Time.deltaTime;
        previous = transform.position;

        if (velocity.y > 0)
        {
            UpThruster1.SetActive(true);
            UpThruster2.SetActive(true);
            DownThruster1.SetActive(false);
            DownThruster2.SetActive(false);
        } else if (velocity.y < 0)
        {
            UpThruster1.SetActive(false);
            UpThruster2.SetActive(false);
            DownThruster1.SetActive(true);
            DownThruster2.SetActive(true);
        } else
        {
            UpThruster1.SetActive(false);
            UpThruster2.SetActive(false);
            DownThruster1.SetActive(false);
            DownThruster2.SetActive(false);
        }
    }
}
