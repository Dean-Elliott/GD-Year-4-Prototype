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
    public float knockbackRecoveryTime = .1f;

    [Header("visuals")]
    public SpriteRenderer buttonIndicatorSprite;
    public GameObject dustParticlesPrefab;
    public SpriteRenderer[] baseColourSprites;
    public SpriteRenderer[] secondaryColourSprites;
    public GameObject weapon;
    public SpriteRenderer weaponSprite;

    public Rigidbody2D rb;
    public MatchManager myMatchManager;
    float rotationDirecion = 1f;
    bool buttonDownInput;
    bool buttonUpInput;
    bool buttonInput;

    public GameObject rotatedChild;

    public bool isLethal = true;

    //
    public bool applyKnockback;
    public GameObject knockbackCollisionObject;
    //

    float knockbacktimer;
    float minTimeBetweenKnockbacks = 0.1f;
    float knockbackRecoveryTimer = 999999f;

    public bool alwaysLethal;
    public float lethalityWindow = 0.1f;
    float lethalityWindowTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buttonIndicatorSprite.enabled = false;
        rb.centerOfMass = Vector3.zero;
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

        print(isLethal);
        //update weapon sprite
        if(isLethal == true)
        {
            weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 1f);
        }
        if (isLethal == false)
        {
            weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 0.5f);
            //weaponSprite.color = Color.black;
        }
    }

    private void FixedUpdate()
    {
        //swapGravity();

        //knockback timer temp
        knockbacktimer += Time.fixedDeltaTime;
        knockbackRecoveryTimer += Time.fixedDeltaTime;
        if(knockbacktimer > minTimeBetweenKnockbacks)
        {
            if (applyKnockback == true)
            {
                knockbackRecoveryTimer = 0f;
                print("kockback executed " + myID);
                //change force to be dependent on impact velo and mass, not dash. improve vector accuracy.
                rb.velocity = Vector2.zero;
                rb.AddForce((transform.position - knockbackCollisionObject.transform.position).normalized * dashSpeed, ForceMode2D.Impulse);
                applyKnockback = false;
            }
            knockbacktimer = 0f;
            //isLethal = true;
        }

        

        //button in HELD UP state
        if (buttonDownInput == false && buttonInput == false)
        {
            //rb.MoveRotation(rb.rotation + (rotationSpeed * Time.fixedDeltaTime * rotationDirecion));
            rotatedChild.transform.Rotate(new Vector3(0, 0, (rotationSpeed * Time.fixedDeltaTime * rotationDirecion)));
            isLethal = false;
        }

        //button in HELD DOWN state
        if (buttonInput)
        {
            isLethal = true;
        }

        //button down this frame
        if (buttonDownInput)
        {
            if (knockbackRecoveryTimer > knockbackRecoveryTime)
            {
                rb.velocity = Vector2.zero;
            }
            //rb.AddForce(transform.up * dashSpeed, ForceMode2D.Impulse);
            rb.AddForce(rotatedChild.transform.up * dashSpeed, ForceMode2D.Impulse);
            buttonIndicatorSprite.enabled = true;

            Instantiate(dustParticlesPrefab, transform.position, Quaternion.identity);

            buttonDownInput = false;

            isLethal = true;
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

            isLethal = false;
        }
    }
}
