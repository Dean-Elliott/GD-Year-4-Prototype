using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : State
{
    
    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    // Update is called once per frame
    public override void Update()
    {
        mm.CheckMultipleInputs(); 
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

}
