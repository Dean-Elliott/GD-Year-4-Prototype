using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Teleportation : MonoBehaviour
{
    [SerializeField]
    private Teleportation otherTeleporter; // Onced serialized, it will show in the inspector

    private List<GameObject> ignoredObjects = new List<GameObject>(); // List created for the players passing through the portal 

    private async void IgnoreTemporarily(GameObject otherObject) //Portal wont work a certain amount of time, players will be able to exit without being sucked back into the portal
    {
        ignoredObjects.Add(otherObject);

        await Task.Delay(1000); //Delay for 1 second

        ignoredObjects.Remove(otherObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!otherTeleporter)
        {
            throw new System.Exception("Other teleporter not assigned.");
        }

        if (IsPlayer(other.gameObject)) // Teleport the player if they enter the trigger
        {
            Teleport(other.gameObject);
        }
    }

    private bool IsPlayer(GameObject gameObject) //check if name or tag is player
    {

        return gameObject.GetComponentInParent<PlayerController>(); //Locate the object of the player with the Player Controller
    }

    private async void Teleport(GameObject otherObject)
    {
        await Task.Delay(100);
        otherObject = otherObject.GetComponentInParent<PlayerController>().gameObject;

       
        if (ignoredObjects.Contains(otherObject)) return;  //Dont teleport this object, its ignored

        otherTeleporter.IgnoreTemporarily(otherObject);
        Vector2 position = otherTeleporter.transform.position;
        otherObject.transform.position = position;
    }
}