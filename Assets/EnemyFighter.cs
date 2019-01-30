using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour
{
    public AnimationCurve pathDevianceX;
    public float devianceOffset = 0;
    public float devianceRate = 4;
    public float devianceMultiplier = 10;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float evaluated = pathDevianceX.Evaluate((Time.time * devianceRate) + devianceOffset) * devianceMultiplier;
        Debug.Log(evaluated);
        transform.position += Vector3.back * Time.deltaTime * speed;
        transform.position += Vector3.right * evaluated * speed * Time.deltaTime;
    }
}
