using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Transform FromBehindPos;
    public Transform TopDownPos;
    public Transform SidePos;
    public Transform CameraTrans;

    public float transitionTime = 2.0f;
    public float rotationSpeed = 30;

    public AnimationCurve positionTransition;
    public AnimationCurve rotationTransition;

    private Vector3 velocity = Vector3.zero;
    private bool changedThisFrame = false;

    private float startTime = 0;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 targetPos;
    private Quaternion targetRot;

    // Start is called before the first frame update
    void Start()
    {
        SceneManagerScript.instance.PerspectiveChanged += ChangePerspective;
        startPosition = targetPos = CameraTrans.position;
        startRotation = targetRot = CameraTrans.rotation;

        switch (SceneManagerScript.instance.PlayerPerspective)
        {
            case PlayerPerspective.TopDown:
                CameraTrans.position = TopDownPos.position;
                CameraTrans.rotation = TopDownPos.rotation;
                break;
            case PlayerPerspective.Side:
                CameraTrans.position = SidePos.position;
                CameraTrans.rotation = SidePos.rotation;
                break;
            case PlayerPerspective.FromBehind:
                CameraTrans.position = FromBehindPos.position;
                CameraTrans.rotation = FromBehindPos.rotation;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void ChangePerspective(object sender, EventArgs e)
    {
        targetRot = Quaternion.identity;
        targetPos = Vector3.zero;

        switch (SceneManagerScript.instance.PlayerPerspective)
        {
            case PlayerPerspective.TopDown:
                targetPos = TopDownPos.position;
                targetRot = TopDownPos.rotation;
                break;
            case PlayerPerspective.Side:
                targetPos = SidePos.position;
                targetRot = SidePos.rotation;
                break;
            case PlayerPerspective.FromBehind:
                targetPos = FromBehindPos.position;
                targetRot = FromBehindPos.rotation;
                break;
            default:
                break;
        }

        startTime = Time.time;
        startPosition = CameraTrans.position;
        startRotation = CameraTrans.rotation;
    }

    private void Update()
    {
        float timeFromStart = Time.time - startTime;
        float percentage = timeFromStart / transitionTime;
        if (percentage >= 0 && percentage <= 1)
        {
            float dampedTimePos = positionTransition.Evaluate(percentage);
            float dampedTimeRot = rotationTransition.Evaluate(percentage);

            CameraTrans.position = Vector3.Lerp(startPosition, targetPos, dampedTimePos);
            CameraTrans.rotation = Quaternion.Slerp(startRotation, targetRot, dampedTimeRot);
        }
    }
}
