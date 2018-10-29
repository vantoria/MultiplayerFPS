using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] DisableComponent;
    [SerializeField]
    private int remoteLayer = 9;

    Camera SceneCamera;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        } else
        {
            SceneCamera = Camera.main;
            if (SceneCamera != null) SceneCamera.gameObject.SetActive(false);
        }

        // register player
        string ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = ID;

    }

    private void OnDisable()
    {
        if (SceneCamera != null) SceneCamera.gameObject.SetActive(true);
    }
    
    void AssignRemoteLayer()
    {
        gameObject.layer = remoteLayer;
    }

    void DisableComponents()
    {
        for (int i = 0; i < DisableComponent.Length; i++)
        {
            DisableComponent[i].enabled = false;
        }
    }
}
	

