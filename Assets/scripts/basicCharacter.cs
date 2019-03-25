using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCharacter : BaseCharacter
{
    public override void OnButtonDownThisFrame()
    {
        Dash();
    }

    public override void OnButtonHeldDown()
    {
        //
    }

    public override void OnButtonHeldUp()
    {
        RotateCharacter(currentRotationDirection, char_rotationSpeed);
    }

    public override void OnButtonUpThisFrame()
    {
        //
    }
}
