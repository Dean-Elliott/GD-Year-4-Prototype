using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum rotationDirection { clockwise, counterClockwise, noRotation, unassigned };

public abstract class BaseCharacter : MonoBehaviour
{
    public int playerID;

    [Header("Input")]
    public string inputButton;
    bool isButtonClickedDown;
    bool isButtonClickedUp;
    bool isButtonHeldDown;
    bool isButtonHeldUp;

    [Header("Stats")]
    public float char_mass;
    public float char_gravityScale;
    public rotationDirection char_initialRotationDirection = rotationDirection.clockwise;
    public float char_rotationSpeed;
    public float char_dashSpeed;

    [Header("movement")]
    rotationDirection currentRotationDirection;
    float rotationSpeed;
    float dashSpeed;

    public void onAwake()
    {

    }

    //rotate(rotationDirection newRotationDirection, float rotationSpeed)

    //dash()

    //getDirection()

    //updates Inputs
    public void checkForInputs()
    {
        //if statements prevents missing inputs by ensuring that a press will not be overwritten false if the input is not used by the next update loop
        //button down this frame
        if (Input.GetButtonDown(inputButton) == true)
        {
            isButtonClickedDown = true;
        }
        //button up this frame
        if (Input.GetButtonUp(inputButton) == true)
        {
            isButtonClickedUp = true;
        }
        //button held down
        isButtonHeldDown = Input.GetButton(inputButton);
        //button held up
        isButtonHeldUp = !Input.GetButton(inputButton);
    }
}
