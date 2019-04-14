using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    private void Awake()
    {
        winnerText = GetComponent<TextMeshProUGUI>();      
    }

    private void Start()
    {
        winnerText.text = GameManager.gameManagerInstance.winningPlayerID.ToString();
        winnerText.color = GameManager.gameManagerInstance.playerColors[GameManager.gameManagerInstance.winningPlayerID];
        StartCoroutine(GoToSelectionScene());
    }

    IEnumerator GoToSelectionScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("menuTest");
    }
}
