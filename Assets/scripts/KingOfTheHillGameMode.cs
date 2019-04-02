﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillGameMode : GameMode
{
    public GameObject[] spawnPoints;

    public float[] playerScores;

    public KingOfTheHillCapturePoint capturePoint;

    public enum ScoreCalculationMode { allPlayersInZoneScore, firstPlayerInControlsPointUntilRemoved, firstPLayerInScoresOnlyIfUncontested}

    private void OnEnable()
    {
        SpawnAllPlayers();
        playerScores = new float[players.Count];
    }

    public void SpawnAllPlayers()
    {
        //always spawn at initial spawn corresponding to ID, never randomly
        foreach (KeyValuePair<int, Player> player in players)
        {
            if (player.Value.activeCharacterInScene == null)
            {
                SpawnPlayer(player.Key, spawnPoints[player.Key].transform.position);
            }
        }
    }

    public void RespawnPlayer(int PlayerToRespawnID)
    {
        Vector2 furthestAwaySpawnPoint = FindNodeFarthestFromAnyActivePlayer(spawnPoints).transform.position;
        SpawnPlayer(PlayerToRespawnID, furthestAwaySpawnPoint);
    }


    public override void CharacterCollision(int attackerPlayerID, int VictimPlayerID)
    {
        Destroy(players[VictimPlayerID].activeCharacterInScene);
        RespawnPlayer(VictimPlayerID);
    }


    public void PlayerInCaptureZone(GameObject playerInZone)
    {
        playerScores[playerInZone.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID] += Time.fixedDeltaTime;
        //print("red: " + (int)playerScores[0] + "  blue: " + (int)playerScores[1] + "  green: " + (int)playerScores[2] + "  yellow: " + (int)playerScores[3]);
    }
}
