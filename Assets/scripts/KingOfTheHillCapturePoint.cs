using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillCapturePoint : MonoBehaviour
{
    public KingOfTheHillGameMode activeGameMode;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeGameMode.PlayerInCaptureZone(collision.gameObject);
        }
    }
}
