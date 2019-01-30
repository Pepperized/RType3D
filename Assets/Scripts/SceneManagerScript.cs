using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    public float SkyboxRotation;
    public PlayerPerspective startPerspective;

    private PlayerPerspective playerPerspective;

    public event System.EventHandler PerspectiveChanged;

    protected virtual void OnPerspectiveChanged()
    {
        if (PerspectiveChanged != null) PerspectiveChanged(this, EventArgs.Empty);
    }

    public PlayerPerspective PlayerPerspective
    {
        get
        {
            return playerPerspective;
        }

        set
        {
            PlayerPerspective oldValue = playerPerspective;
            playerPerspective = value;
            if (oldValue != value)
            {
                OnPerspectiveChanged();
            }
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        RenderSettings.skybox.SetFloat("_Rotation", SkyboxRotation);
    }

    private float frombehindtime = 10.0f;
    private float sideTime = 15;

    // Update is called once per frame
    void Update()
    {
        PlayerPerspective = startPerspective;

        /*
        if (Time.time > sideTime)
        {
            PlayerPerspective = PlayerPerspective.Side;
            frombehindtime = 20;
        } else if (Time.time > frombehindtime)
        {
            PlayerPerspective = PlayerPerspective.FromBehind;
        }*/
    }
}
