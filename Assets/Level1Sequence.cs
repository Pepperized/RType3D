using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Sequence : MonoBehaviour
{
    public GameObject group1;
    public GameObject group2;

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
    }
}
