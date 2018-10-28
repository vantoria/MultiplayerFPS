using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] DisableComponent;

    Camera SceneCamera;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < DisableComponent.Length; i++)
            {
                DisableComponent[i].enabled = false;
            }
        } else
        {
            SceneCamera = Camera.main;
            if (SceneCamera != null) SceneCamera.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (SceneCamera != null) SceneCamera.gameObject.SetActive(true);
    }
}
	

