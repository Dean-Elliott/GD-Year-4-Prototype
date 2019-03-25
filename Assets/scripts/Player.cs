using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public bool isActive;
    public string inputButton; 
    public int playerID;
    public int teamID;
    public GameObject characterSelectionPrefab;

    public GameObject activeCharacterInScene;
    public BaseCharacter activeCharacterInSceneCharacterScript;

    //HACK
    public void InitializeCharacter(GameMode activeGameMode)
    {
        activeCharacterInSceneCharacterScript = activeCharacterInScene.GetComponent<BaseCharacter>();
        activeCharacterInSceneCharacterScript.inputButton = inputButton;
        activeCharacterInSceneCharacterScript.myPlayerInfo = this;
        activeCharacterInScene.GetComponent<PlayerVisuals>().myPlayerInfo = this;
        activeCharacterInScene.GetComponentInChildren<HitBox>().myPlayerID = playerID;
        activeCharacterInScene.GetComponentInChildren<HitBox>().activeGameMode = activeGameMode;
    }
}
