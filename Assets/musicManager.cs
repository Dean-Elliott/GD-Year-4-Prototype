using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class musicManager : MonoBehaviour
{
    FMOD.Studio.EventInstance music;

    private void Awake()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/music");
        music.start();
    }

    private void OnDisable()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
