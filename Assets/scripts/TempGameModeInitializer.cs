using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameModeInitializer : MonoBehaviour
{
    public Player[] spoofedPlayerData;
    //Auston Change
    public static TempGameModeInitializer instance; 

    private void Awake()
    {
        ///
        CheckForPrevious();
    }

    //Aushton Change
    public void CheckForPrevious()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    ///
}
