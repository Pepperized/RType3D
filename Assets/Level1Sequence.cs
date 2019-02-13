using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Sequence : MonoBehaviour
{
    public GameObject group1;
    public GameObject group2;
    public GameObject group3;
    public GameObject group4;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        group1.SetActive(true);
        yield return new WaitForSeconds(2);
        group2.SetActive(true);
        yield return new WaitForSeconds(4);
        //TODO: powerup
        yield return new WaitForSeconds(2);
        group3.SetActive(true);
        yield return new WaitForSeconds(2);
        group4.SetActive(true);

    }
}
