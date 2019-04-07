using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShield : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    public BaseCharacter myCharacter;
    public bool isTimerStarted = true;
    public float shieldDuration;
    private float shieldTimer = 0f;

    private void Awake()
    {
        myRigidbody = GetComponentInParent<Rigidbody2D>(); 
    }

    private void Start()
    {
        myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        myCharacter.isVulnerable = false;
    }

    void Update()
    {
        if (isTimerStarted)
        {
            shieldTimer += Time.deltaTime;
        }

        if(shieldTimer > shieldDuration)
        {
            PopShield();
        }

        if((myCharacter.isButtonDownThisFrame || myCharacter.isButtonHeldDown) && myCharacter.canUseInputs == true)
        {
            PopShield();
        }
    }

    public void PopShield()
    {
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        myCharacter.isVulnerable = true;
        Destroy(gameObject);
    }
}
