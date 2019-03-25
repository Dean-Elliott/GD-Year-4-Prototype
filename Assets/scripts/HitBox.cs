using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerCollisionEvent : UnityEvent<int, int> { }

public class HitBox : MonoBehaviour
{
    public OnPlayerCollisionEvent OnPlayerCollision = new OnPlayerCollisionEvent();
    public int myPlayerID;
    GameMode activeGameMode;

    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            //HACK BIG HACK FIX FIX FIX
            //get id of other player
            int otherPartyID = collision.gameObject.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID;
            if (otherPartyID != myPlayerID)
            {
                OnPlayerCollision.Invoke(otherPartyID, myPlayerID);
            }
        }
    }
}
