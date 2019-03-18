using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int myID;
    public Character thisCharacter;
    public string inputButton;

    [Header("movement")]
    public float rotationSpeed;
    public float dashSpeed;

    [Header("visuals")]
    public SpriteRenderer buttonIndicatorSprite;
    public GameObject dustParticlesPrefab;
    public SpriteRenderer[] baseColourSprites;
    public SpriteRenderer[] secondaryColourSprites;
    public GameObject weapon;

    Rigidbody2D rb;
    public MatchManager myMatchManager;
    float rotationDirecion = 1f;
    bool buttonDownInput;
    bool buttonUpInput;
    bool buttonInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buttonIndicatorSprite.enabled = false;
    }

    private void Start()
    {
        //apply alternate character attributes from scriptable object
        if (thisCharacter != null)
        {
            for (int i = 0; i < baseColourSprites.Length; i++) { baseColourSprites[i].color = thisCharacter.mainColour; }
            for (int i = 0; i < secondaryColourSprites.Length; i++) { secondaryColourSprites[i].color = thisCharacter.secondaryColour; }
            dashSpeed = thisCharacter.dashSpeed;
            rotationSpeed = thisCharacter.rotationSpeed;
            transform.localScale = new Vector2(thisCharacter.bodyScale, thisCharacter.bodyScale);
            weapon.transform.localScale = thisCharacter.weaponSize;
            rotationDirecion = thisCharacter.rotationDirection;
        }
    }

    void Update()
    {
        //get input
        //if statements prevents missing inputs by ensuring that a press will not be overwritten false if the input is not used by the next update loop
        if (Input.GetButtonDown(inputButton))
        {
            buttonDownInput = true;
        }
        if (Input.GetButtonUp(inputButton))
        {
            buttonUpInput = true;
        }
        buttonInput = Input.GetButton(inputButton);
    }

    //
    //float heldUpTimer = 0f;
    //float heldDownTimer = 0f;
    //public float heldUpActivationTime = 1f;
    //public float heldDownActivationTime = 1f;
    //public void swapGravity()
    //{
    //    if (buttonDownInput == false && buttonUpInput == false && buttonInput == false)
    //    {
    //        heldUpTimer += Time.fixedDeltaTime;
    //    }
    //    if(heldUpTimer >= heldDownActivationTime)
    //    {
    //        rb.gravityScale *= -1f;
    //        heldUpTimer = 0f;
    //    }

    //    if (buttonDownInput)
    //    {
    //        heldUpTimer = 0f;
    //    }
    //}
    //

    private void FixedUpdate()
    {
        //swapGravity();

        //button in HELD UP state
        if (buttonDownInput == false && buttonInput == false)
        {
            rb.MoveRotation(rb.rotation + (rotationSpeed * Time.fixedDeltaTime * rotationDirecion));
        }

        //button in HELD DOWN state
        if (buttonInput)
        {

        }

        //button down this frame
        if (buttonDownInput)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * dashSpeed, ForceMode2D.Impulse);
            buttonIndicatorSprite.enabled = true;

            Instantiate(dustParticlesPrefab, transform.position, Quaternion.identity);

            buttonDownInput = false;
        }

        //button up this frame
        if (buttonUpInput)
        {
            if (thisCharacter != null)
            {
                if (thisCharacter.changeRotationDirectionOnButtonUp)
                {
                    //for zigzag character
                    rotationDirecion *= -1;
                }
            }
            buttonIndicatorSprite.enabled = false;
            buttonUpInput = false;
        }
    }

    //hardcoded, fix this
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pointy1") && myID == 2)
        {
            rb.velocity = Vector2.zero;
            myMatchManager.teleportPlayer(this.gameObject);
        }
        if (collision.CompareTag("pointy2") && myID == 1)
        {
            rb.velocity = Vector2.zero;
            myMatchManager.teleportPlayer(this.gameObject);
        }
    }
}
