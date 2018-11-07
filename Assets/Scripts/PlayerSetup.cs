using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] DisableComponent;
    [SerializeField]
    private int remoteLayer = 9;

    [SerializeField]
    string DontDrawLayerName = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;

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

            //disable player graphic for local
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(DontDrawLayerName));

            // create playerUI
            playerUIInstance = Instantiate(playerUIprefab);
            playerUIInstance.name = playerUIprefab.name;
        }

        GetComponent<PlayerManager>().Setup();

    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach(Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
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
	

