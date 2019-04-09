using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    // HACK :set up with spoofed data rn, integrate with aushton
    public Dictionary<int, Player> players;

    public float respawnTime = 1f;

    public GameObject []initialSpawnZones;

    //HACK
    public CameraShake camshake;

    //HACK
    [Header("temp hack")]
    public TempGameModeInitializer spoofedPlayerDataGameObject;
    private void Awake()
    {
        players = new Dictionary<int, Player>();
        foreach(Player player in spoofedPlayerDataGameObject.spoofedPlayerData)
        {
            players.Add(player.playerID, player);
        }
    }

    private void OnEnable()
    {
        //HACK
        AtEndOfOnEnable();
        MatchStart();
    }

    private void MatchStart()
    {
        SpawnAllPlayers(initialSpawnZones);
        foreach (KeyValuePair<int, Player> player in players)
        {
            player.Value.activeCharacterInScene.GetComponentInChildren<SpawnShield>().isTimerStarted = false;
            player.Value.activeCharacterInSceneCharacterScript.canUseInputs = false;
        }
            StartCoroutine(StartOfRoundCountdownTimer());
    }

    public virtual void AtEndOfOnEnable()
    {

    }

    IEnumerator StartOfRoundCountdownTimer()
    {
        yield return new WaitForSeconds(3);
        foreach (KeyValuePair<int, Player> player in players)
        {
            player.Value.activeCharacterInSceneCharacterScript.canUseInputs = true;
            player.Value.activeCharacterInScene.GetComponentInChildren<SpawnShield>().isTimerStarted = true;
        }
    }

    public void SpawnPlayer(int playerToSpawnID, Vector2 spawnLocation)
    {
        players[playerToSpawnID].activeCharacterInScene = Instantiate(players[playerToSpawnID].characterSelectionPrefab, spawnLocation, Quaternion.identity);
        players[playerToSpawnID].InitializeCharacter(this);
    }

    public void SpawnAllPlayers(GameObject[] initialSpawnNodes)
    {
        //always spawn at initial spawn corresponding to ID, never randomly
        foreach (KeyValuePair<int, Player> player in players)
        {
            if (player.Value.activeCharacterInScene == null)
            {
                SpawnPlayer(player.Key, initialSpawnNodes[player.Key].transform.position);
            }
        }
    }

    public virtual void UpdateUI(){}

    public virtual void CharacterCollision(int attackerPlayerID, int VictimPlayerID){}

    public virtual GameObject FindNodeFarthestFromAnyActivePlayer(GameObject[] nodes)
    {
        //find the distance to the closest live player for all spawn points
        float[] spawnPointClosestDistances = new float[nodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            float spawnPointDistanceToClosestLivingPlayer = Mathf.Infinity;
            foreach (KeyValuePair<int, Player> player in players)
            {
                if (player.Value.activeCharacterInScene != null)
                {
                    if (Vector2.Distance(nodes[i].transform.position, player.Value.activeCharacterInScene.transform.position) < spawnPointDistanceToClosestLivingPlayer)
                    {
                        spawnPointDistanceToClosestLivingPlayer = Vector2.Distance(nodes[i].transform.position, player.Value.activeCharacterInScene.transform.position);
                    }
                }
            }
            spawnPointClosestDistances[i] = spawnPointDistanceToClosestLivingPlayer;
        }

        //compare distances to closest live player for all spawn points, selecting the spawn point with the largest distance as the new spawn point
        float longestDistanceToLivingPlayer = 0f;
        GameObject furthestAwaySpawnPoint = null;
        for (int i = 0; i < spawnPointClosestDistances.Length; i++)
        {
            if (spawnPointClosestDistances[i] > longestDistanceToLivingPlayer)
            {
                longestDistanceToLivingPlayer = spawnPointClosestDistances[i];
                furthestAwaySpawnPoint = nodes[i];
            }
        }
        return furthestAwaySpawnPoint;
    }
}
