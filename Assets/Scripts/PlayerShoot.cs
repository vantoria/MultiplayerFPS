using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour

{    private const string PLAYERTAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask shotMask;

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
    // client side only function　クライアント側だけ
    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, weapon.range, shotMask))
        {
            if (hit.collider.tag == PLAYERTAG)
            {
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }
        }
    }
    
    // send the data to server サーバに情報を送りします。
    [Command]
    void CmdPlayerShot(string playerID, int _damage)
    {
        Debug.Log(playerID + "has been shot.");
        PlayerManager _player = GameManager.GetPlayer(playerID);
        _player.RpcTakeDamage(_damage);
    }

}
