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
    public float rotationSpeed = 250;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += Vector3.right * pathDevianceX.Evaluate(devianceOffset) * devianceMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        float evaluated = pathDevianceX.Evaluate((Time.time * devianceRate) + devianceOffset) * devianceMultiplier;
        transform.position += Vector3.back * Time.deltaTime * speed;
        transform.position += Vector3.right * evaluated * speed * Time.deltaTime;
        Vector3 newRot = transform.rotation.eulerAngles;
        newRot.z += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(newRot);

    }
}
