using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "unnamedMap")]
public class Map : ScriptableObject 
{
    public string sceneName;

    public GameManager.GameMode[] supportedGameModes;

    public int minPlayers;
    public int maxPlayers;
}
