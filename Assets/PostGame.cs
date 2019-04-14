using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    string text;

    private void Awake()
    {
        winnerText = GetComponent<TextMeshProUGUI>();      
    }

    private void Start()
    {
        winnerText.color = GameManager.gameManagerInstance.playerColors[GameManager.gameManagerInstance.winningPlayerID];
        StartCoroutine(GoToSelectionScene());

        if (GameManager.gameManagerInstance.winningPlayerID == 0)
        {
            text = "Red wins";
        }
        if (GameManager.gameManagerInstance.winningPlayerID == 1)
        {
            text = "Blue wins";
        }
        if (GameManager.gameManagerInstance.winningPlayerID == 2)
        {
            text = "Green wins";
        }
        if (GameManager.gameManagerInstance.winningPlayerID == 3)
        {
            text = "Yellow wins";
        }

        winnerText.text = text;
    }

    IEnumerator GoToSelectionScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("menuTest");
    }
}
