using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    // only playerManager class can read the bool    playerManagerだけ読める
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }   

    [SerializeField]
    private int maxHealth = 100;
    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    [SerializeField]
    private float respawnTime = 3f;
    private bool[] wasEnabled;

    // loop through disabled component and put a bool on them so we can enable them when respawn.
    //ループでか
    public void Setup()
    {
        // use behavoir to enable/disable component. 
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefault();
    }

    // will send command to all the computer connected to network サーバーからクライアント側に情報に送りします
    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead) return;

        currentHealth -= _amount;
        Debug.Log(transform.name + "now HP is " + currentHealth);

        if(currentHealth <= 0)
        {
            PlayerDie();
        }
    }
    //when player die　プレーヤー死にます
    private void PlayerDie()
    {
        isDead = true;

        //disable all the component in array
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null) _col.enabled = false;

        Debug.Log(transform.name + " is dead!");

        StartCoroutine(Respawn(respawnTime));
    }

    private IEnumerator Respawn(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);

        SetDefault();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
        Debug.Log(transform.name + " respanwed");
    }

    public void SetDefault()
    {
        isDead = false;
        currentHealth = maxHealth;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }
        // special code to eanble collider
        Collider _col = GetComponent<Collider>();
        if (_col != null) _col.enabled = true;
            
    }
}
