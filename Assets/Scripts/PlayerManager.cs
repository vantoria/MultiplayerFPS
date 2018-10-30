using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;
    [SyncVar]
    private int currentHealth;

    private void Awake()
    {
        SetDefault();
    }

    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + "now HP is " + currentHealth);
    }

    public void SetDefault()
    {
        currentHealth = maxHealth;
    }
}
