using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAllGameMode : GameMode
{
    [Header("specific to this mode")]
    public int[] playerScores;

    public GameObject[] initialSpawnPoints;
    public GameObject[] spawnPoints;

    public override void AtEndOfOnEnable()
    {
        playerScores = new int[players.Count];
    }

    public void RespawnPlayer(int PlayerToRespawnID)
    {
        Vector2 furthestAwaySpawnPoint = FindNodeFarthestFromAnyActivePlayer(spawnPoints).transform.position;
        SpawnPlayer(PlayerToRespawnID, furthestAwaySpawnPoint);
    }

    public override void CharacterCollision(int attackerPlayerID, int VictimPlayerID)
    {
        playerScores[attackerPlayerID] += 1;
        Destroy(players[VictimPlayerID].activeCharacterInScene);
        RespawnPlayer(VictimPlayerID);
    }
}
