using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundDesignStartGame : MonoBehaviour
{
    public void StartSoundScene()
    {
        StartCoroutine(WaitThenStartScene());
        FMODUnity.RuntimeManager.PlayOneShot("event:/button confirm");
    }

    IEnumerator WaitThenStartScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("soundDesignScene");
    }
}
