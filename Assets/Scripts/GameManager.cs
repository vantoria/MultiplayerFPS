using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    private const string PLAYER_PREFIX = "Player ";

    private static Dictionary<string, PlayerController> players = new Dictionary<string, PlayerController>();

    public void RegisterPlayer(string _NetID, PlayerController _player)
    {
        string _playerID = PLAYER_PREFIX + _NetID;
     //   players.Add(_playerID)
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
