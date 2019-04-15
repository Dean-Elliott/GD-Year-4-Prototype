using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    FMOD.Studio.EventInstance music;

    private void Awake()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/ambience");
        music.start();
    }

    private void OnDisable()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
