using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillGameMode : GameMode
{
    public GameObject[] spawnPoints;

    public float[] playerScores;

    public float pointsToWin = 20f;

    public KingOfTheHillCapturePoint capturePoint;

    public GameObject explosionPrefab;

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
        //HACK
        //explosion
        GameObject newExplosion;
        newExplosion = Instantiate(explosionPrefab, players[VictimPlayerID].activeCharacterInScene.transform.position, Quaternion.identity);
        ParticleSystem ps = newExplosion.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = GameManager.gameManagerInstance.playerColors[VictimPlayerID];

        Destroy(players[VictimPlayerID].activeCharacterInScene);
        RespawnPlayer(VictimPlayerID);

        //HACK
        camshake.Shake();    
    }


    public void PlayerInCaptureZone(GameObject playerInZone)
    {
        if(isGameOver == false)
        {
        playerScores[playerInZone.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID] += Time.fixedDeltaTime;
        //print("red: " + (int)playerScores[0] + "  blue: " + (int)playerScores[1] + "  green: " + (int)playerScores[2] + "  yellow: " + (int)playerScores[3]);
        Debug.Log(playerScores);
        CheckWinCondition(playerInZone.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID);
        UpdateUI();
        }
    }

    public override void CheckWinCondition(int scoringPlayer)
    {
        if (playerScores[scoringPlayer] >= pointsToWin)
        {
            GameManager.gameManagerInstance.winningPlayerID = scoringPlayer;
            isGameOver = true;
            StartCoroutine(GameWon());
        }
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < playerScoresTextMesh.Length; i++)
        {
            playerScoresTextMesh[i].text = ((int)playerScores[i]).ToString();
        }
    }
}
