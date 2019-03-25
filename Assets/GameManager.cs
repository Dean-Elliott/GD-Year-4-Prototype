using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] allCharacterPrefabs;

    public Color[] playerColors;

    public static GameManager gameManagerInstance;



    /// Aushton Changes Beging

    public enum GameMode { freeForAll, oddBall }
    /// Aushton Changes End
    private void Awake()
    {
        if (gameManagerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManagerInstance = this;
        }
    }
}
