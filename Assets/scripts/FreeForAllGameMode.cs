using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAllGameMode : GameMode
{
    [Header("specific to this mode")]
    public int[] playerScores;
    public HitBox hitboxPlayerCollision;

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
                player.Value.activeCharacterInScene = Instantiate(player.Value.characterSelectionPrefab, initialSpawnPoints[player.Value.playerID].transform.position, Quaternion.identity);
                player.Value.InitializeCharacter();
            }
        }

        //HACK
        // subscribe to collision events
        foreach (KeyValuePair<int, Player> player in players)
        {
            if (player.Value.activeCharacterInScene != null)
            {
                player.Value.activeCharacterInScene.GetComponentInChildren<HitBox>().OnPlayerCollision.AddListener(CharacterCollision);
            }
        }
    }

    public void RespawnPlayer(int PlayerToRespawnID)
    {
        GameObject furthestAwaySpawnPoint = FindNodeFarthestFromAnyActivePlayer(spawnPoints);
        players[PlayerToRespawnID].activeCharacterInScene = Instantiate(players[PlayerToRespawnID].characterSelectionPrefab, furthestAwaySpawnPoint.transform.position, Quaternion.identity);
        players[PlayerToRespawnID].InitializeCharacter();
        //HACK
        // subscribe to collision events
        players[PlayerToRespawnID].activeCharacterInScene.GetComponentInChildren<HitBox>().OnPlayerCollision.AddListener(CharacterCollision);
    }

    public override void CharacterCollision(int attackerPlayerID, int VictimPlayerID)
    {
        playerScores[attackerPlayerID] += 1;

        players[VictimPlayerID].activeCharacterInScene.GetComponentInChildren<HitBox>().OnPlayerCollision.RemoveListener(CharacterCollision);
        Destroy(players[VictimPlayerID].activeCharacterInScene);

        RespawnPlayer(VictimPlayerID);

        print(playerScores[0] + "  " +  playerScores[1] + "  " + playerScores[2] + "  " + playerScores[3]);
    }
}
