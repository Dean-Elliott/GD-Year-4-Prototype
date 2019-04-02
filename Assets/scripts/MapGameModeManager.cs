using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameModeNames { FreeForAll, KingOfTheHill }
public class MapGameModeManager : MonoBehaviour
{
    public Scene thisMap;

    public FreeForAllGameMode freeForAll;
    public KingOfTheHillGameMode kingOfTheHill;

    private void Start()
    {

    }
}
