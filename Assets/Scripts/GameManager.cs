 using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    private const string PLAYER_PREFIX = "Player ";

    private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();
    
    // register newly connected player into the library
    // サーバーに新しく繋がるプレーヤーをライブラリに登録します
    public static void RegisterPlayer(string _NetID, PlayerManager _player)
    {
        string _playerID = PLAYER_PREFIX + _NetID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    // get player ID プレーヤーのIDゲット
    public static PlayerManager GetPlayer(string _playerID)
    {
        if (!players.ContainsKey(_playerID))
        {
            Debug.Log("Bad game ID"+ _playerID);
            return null;
        }
        return players[_playerID];
    }

    // unregister player when disconnected
    // プレーヤーが切断時ライブラリから削除します
    public static void DeRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string _playerID in players.Keys)
        {
            GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

}
