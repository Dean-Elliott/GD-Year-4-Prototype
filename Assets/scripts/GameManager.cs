using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject[] allCharacterPrefabs;

    public Color[] playerColors;

    public static GameManager gameManagerInstance;

    public GameObject spoofedPlayerData;

    public enum GameMode { freeForAll, koth};

    public int winningPlayerID;

    private void Awake()
    {
        if (gameManagerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(this);

        }

        spoofedPlayerData = FindObjectOfType<TempGameModeInitializer>().gameObject;
        DontDestroyOnLoad(spoofedPlayerData);
    }
}
