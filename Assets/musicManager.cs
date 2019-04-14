using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class musicManager : MonoBehaviour
{
    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/music");
    }
}
