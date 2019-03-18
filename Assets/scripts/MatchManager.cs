using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public GameObject player1GameObject;
    public GameObject player2GameObject;
    public GameObject player1Explosion;
    public GameObject player2Explosion;
    public GameObject[] spawnNodeGameObjects;
    public CameraShake camShake;
    int player1Score = 0;
    int player2Score = 0;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    private void Awake()
    {
        player1GameObject.GetComponent<PlayerController>().myMatchManager = this;
        player2GameObject.GetComponent<PlayerController>().myMatchManager = this;
    }

    public void teleportPlayer(GameObject player)
    {
        CreateExplosion(player);

        GameObject otherPlayer = null;
        if(player == player1GameObject)
        {
            otherPlayer = player2GameObject;
            player2Score += 1;
        }
        if(player == player2GameObject)
        {
            otherPlayer = player1GameObject;
            player1Score += 1;
        }
        p1ScoreText.text = player1Score.ToString();
        p2ScoreText.text = player2Score.ToString();

        int farthestSpawn = 0;
        float distanceToFarthestSpawn = 0f;
        for (int i = 0; i < spawnNodeGameObjects.Length; i++)
        {
            float thisSpawnsDistance = Vector2.Distance(spawnNodeGameObjects[i].transform.position, otherPlayer.transform.position);
            if(thisSpawnsDistance > distanceToFarthestSpawn)
            {
                farthestSpawn = i;
                distanceToFarthestSpawn = thisSpawnsDistance;
            }
        }
        player.transform.position = spawnNodeGameObjects[farthestSpawn].transform.position;
    }

    void CreateExplosion(GameObject player)
    {
        camShake.shakeDuration = camShake.maxDuration; 

        int explosionID = player.GetComponent<PlayerController>().myID;
        if (explosionID == 1)
        {
            GameObject explosion = Instantiate(player1Explosion, player.transform.position, Quaternion.identity);
            explosion.transform.Translate(Vector3.forward * -1f);
        }
        else if (explosionID == 2)
        {
            GameObject explosion = Instantiate(player2Explosion, player.transform.position, Quaternion.identity);
            explosion.transform.Translate(Vector3.forward * -1f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            SceneManager.LoadScene("combat variant");
        }
        if (Input.GetKeyDown("c"))
        {
            SceneManager.LoadScene("test variant");
        }
    }
}
