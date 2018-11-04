using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] DisableComponent;
    [SerializeField]
    private int remoteLayer = 9;

    [SerializeField]
    GameObject playerUIprefab;
    private GameObject playerUIInstance;

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

            // create playerUI
            playerUIInstance = Instantiate(playerUIprefab);
            playerUIInstance.name = playerUIprefab.name;
        }

        GetComponent<PlayerManager>().Setup();

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerManager _player = GetComponent<PlayerManager>();
        GameManager.RegisterPlayer(_netID, _player);
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);

        if (SceneCamera != null) SceneCamera.gameObject.SetActive(true);
        GameManager.DeRegisterPlayer(transform.name);
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
	

