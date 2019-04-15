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

    public ChangeColorToWinningPlayer changeGlowBorderToWinningPlayer;
    public ChangeColorToWinningPlayer changeCapturePointBorderColorToWinningPlayer;

    List<GameObject> playersInCaptureZone;

    public enum ScoreCalculationMode { allPlayersInZoneScore, firstPlayerInControlsPointUntilRemoved, firstPLayerInScoresOnlyIfUncontested }

    private void OnEnable()
    {
        SpawnAllPlayers();
        playerScores = new float[players.Count];

        playersInCaptureZone = new List<GameObject>();
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

    private void Update()
    {
        int numberOfPlayersInZoneNow = 0;
        foreach (GameObject player in playersInCaptureZone)
        {
            numberOfPlayersInZoneNow += 1;
        }
        if (numberOfPlayersInZoneNow == 0 || numberOfPlayersInZoneNow >= 2)
        {
            ChangeCapturePointBorderColour(changeCapturePointBorderColorToWinningPlayer.defaultColor);
        }
        else if (numberOfPlayersInZoneNow == 1)
        {
            foreach (GameObject player in playersInCaptureZone)
            {
                ChangeCapturePointBorderColour(GameManager.gameManagerInstance.playerColors[player.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID]);
            }
        }
        playersInCaptureZone.Clear();
    }

    public void ChangeCapturePointBorderColour(Color newColor)
    {
        changeCapturePointBorderColorToWinningPlayer.setColor(newColor);
    }

    public void PlayerInCaptureZone(GameObject playerInZone)
    {
        if (isGameOver == false)
        {
            playerScores[playerInZone.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID] += Time.fixedDeltaTime;

            bool yes = true;
            foreach (GameObject player in playersInCaptureZone)
            {
                if (player == playerInZone)
                {
                    yes = false;
                }
            }
            if(yes == true)
            {
                playersInCaptureZone.Add(playerInZone);
            }

            //print("red: " + (int)playerScores[0] + "  blue: " + (int)playerScores[1] + "  green: " + (int)playerScores[2] + "  yellow: " + (int)playerScores[3]);
            Debug.Log(playerScores);
            CheckWinCondition(playerInZone.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID);
            UpdateUI();
            ChangeBackgroundColor();
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


    public void ChangeBackgroundColor()
    {
        //HACK
        //change bg color
        float highestScore = -1;
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] > highestScore)
            {
                highestScore = playerScores[i];
            }
        }
        int playersTiedForHighScore = 0;
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] == highestScore)
            {
                playersTiedForHighScore += 1;
            }
        }
        if (playersTiedForHighScore > 1)
        {
            changeGlowBorderToWinningPlayer.setColor(changeGlowBorderToWinningPlayer.defaultColor);
        }
        if (playersTiedForHighScore == 1)
        {
            Color winningPlayerColor;
            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] == highestScore)
                {
                    changeGlowBorderToWinningPlayer.setColor(GameManager.gameManagerInstance.playerColors[i]);
                }
            }
        }
    }
}
