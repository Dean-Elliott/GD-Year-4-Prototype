using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAllGameMode : GameMode
{
    [Header("specific to this mode")]
    public int[] playerScores;

    public GameObject[] initialSpawnPoints;
    public GameObject[] spawnPoints;

    public override void AtEndOfOnEnableOverride()
    {
        SpawnAllPlayers();
    }

    public void SpawnAllPlayers()
    {
        //always spawn at initial spawn corresponding to ID, never randomly
        foreach (KeyValuePair<int, Player> player in players)
        {
            if (player.Value.activeCharacterInScene == null)
            {
                SpawnPlayer(player.Key, initialSpawnPoints[player.Key].transform.position);
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
        playerScores[attackerPlayerID] += 1;
        Destroy(players[VictimPlayerID].activeCharacterInScene);

        RespawnPlayer(VictimPlayerID);

        print(playerScores[0] + "  " +  playerScores[1] + "  " + playerScores[2] + "  " + playerScores[3]);
    }
}
