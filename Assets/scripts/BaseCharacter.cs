using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection { clockwise, counterClockwise, noRotation, unassigned };

public abstract class BaseCharacter : BaseCharacterParent
{
    public string inputButton;

    [Header("Input")]
    public bool isButtonDownThisFrame;
    public bool isButtonUpThisFrame;
    public bool isButtonHeldDown;
    public bool isButtonHeldUp;

    [Header("Stats")]
    public float char_mass;
    public float char_gravityScale;
    public RotationDirection char_initialRotationDirection = RotationDirection.clockwise;
    public float char_rotationSpeed;
    public float char_dashForce;
    public float char_linearDrag;

    [Header("Movement")]
    [HideInInspector]
    public RotationDirection currentRotationDirection;
    public float rotationSpeed;
    public float dashForce;
    public bool canZeroVelocity = true;

    [Header("Components and children")]
    Rigidbody2D myRigidBody2D;
    public GameObject rotatingChildObject; //rotate a child of the object with the rb2D so that the rb2D can have its rotation frozen, preventing unwanted torque

    public Player myPlayerInfo;

    protected override sealed void Awake()
    {
        myRigidBody2D = GetComponentInParent<Rigidbody2D>();
        myRigidBody2D.centerOfMass = Vector3.zero; //ensures center of mass is centered to prevent wobbling and unwanted behaviour
        currentRotationDirection = char_initialRotationDirection;

        OnEndOfAwake(); //for overriding
    }

    protected override sealed void Update() //prevents overriding
    {
        GetInputs();
    }

    public void OnEndOfAwake() //for overriding
    {
    }

    private void FixedUpdate()
    {
        if (isButtonDownThisFrame) { ButtonDownThisFrame(); }
        if (isButtonUpThisFrame) { ButtonUpThisFrame(); }
        if (isButtonHeldDown) { OnButtonHeldDown(); }
        if (isButtonHeldUp) { OnButtonHeldUp(); }
    }

    //attempt to zero out velocity, then apply addforce in direction player is facing
    public void Dash()
    {
        SetVelocityToZero(canZeroVelocity);
        myRigidBody2D.AddForce(rotatingChildObject.transform.up * char_dashForce, ForceMode2D.Impulse);
    }

    //applied before adding forces to achieve more consistent, snappy movement
    public void SetVelocityToZero(bool arg_canZeroVelocity)
    {
        if (arg_canZeroVelocity == true)
        {
            myRigidBody2D.velocity = Vector2.zero;
        }
    }

    //rotate the rotating child object in a given direction at a frequency given in degrees per second
    public void RotateCharacter(RotationDirection newRotationDirection, float rotationSpeed)
    {
        rotatingChildObject.transform.Rotate(new Vector3(0, 0, (rotationSpeed * RotationDirectionEnumToFloat(newRotationDirection) * Time.fixedDeltaTime)));
    }

    //converts rotation direction to a float
    public float RotationDirectionEnumToFloat(RotationDirection newRotationDirection)
    {
        if (newRotationDirection == RotationDirection.clockwise)
        {
            return 1f;
        }
        else if (newRotationDirection == RotationDirection.counterClockwise)
        {
            return -1f;
        }
        else if (newRotationDirection == RotationDirection.noRotation)
        {
            return 0f;
        }
        else if (newRotationDirection == RotationDirection.unassigned)
        {
            return 0f;
            throw new System.Exception("rotation direction is unassigned!");
        }
        else return 0f;
    }

    //switches the characters rotation direction to its opposite
    public void InvertCurrentRotationDirection()
    {
        if (currentRotationDirection == RotationDirection.counterClockwise)
        {
            currentRotationDirection = RotationDirection.clockwise;
        }
        else if (currentRotationDirection == RotationDirection.clockwise)
        {
            currentRotationDirection = RotationDirection.counterClockwise;
        }
    }

    public void GetInputs() //updates Inputs
    {
        //if statements prevents missing inputs by ensuring that a press will not be overwritten false if the input is not used by the next update loop
        //button down this frame
        if (Input.GetButtonDown(inputButton) == true)
        {
            isButtonDownThisFrame = true;
        }
        //button up this frame
        if (Input.GetButtonUp(inputButton) == true)
        {
            isButtonUpThisFrame = true;
        }
        //button held down
        isButtonHeldDown = Input.GetButton(inputButton);
        //button held up
        isButtonHeldUp = !Input.GetButton(inputButton);
    }

    protected override sealed void ButtonDownThisFrame() //prevents override
    {
        OnButtonDownThisFrame();
        isButtonDownThisFrame = false;
    }

    protected override sealed void ButtonUpThisFrame() //prevents override
    {
        OnButtonUpThisFrame();
        isButtonUpThisFrame = false;
    }

    //designers code behaviours that should happen on the frame the button is depressed by overriding this function
    //this method MUST be overriden in the inherited class
    public abstract void OnButtonDownThisFrame(); //for override

    //designers code behaviours that should happen on the frame the button is released by overriding this function
    //this method MUST be overriden in the inherited class
    public abstract void OnButtonUpThisFrame(); //for override

    //designers code behaviours that should happen while the button is released by overriding this function
    //this method MUST be overriden in the inherited class
    public abstract void OnButtonHeldUp(); //for override

    //designers code behaviours that should happen while the button is released by overriding this function
    //this method MUST be overriden in the inherited class
    public abstract void OnButtonHeldDown(); //for override
}
