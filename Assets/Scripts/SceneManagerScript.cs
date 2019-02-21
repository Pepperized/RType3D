using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    public bool perspOverride = false;

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

    private void Update()
    {
        if (perspOverride)
        {
            PlayerPerspective = startPerspective;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        RenderSettings.skybox.SetFloat("_Rotation", SkyboxRotation);
    }
}
