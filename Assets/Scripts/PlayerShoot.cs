using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour

{    private const string PLAYERTAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask shotMask;

    // Use this for initialization
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, weapon.range, shotMask))
        {
            if (hit.collider.tag == PLAYERTAG)
            {
                CmdPlayerShot(hit.collider.name);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string playerID)
    {
        Debug.Log(playerID + "Has been shot");
    }

}
