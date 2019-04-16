using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelect : State
{

    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public  override void Update()
    {
        base.Update();
    }

    public override void OnExitState()
    {
        base.OnExitState();

        mm.modeSelectCanvas.SetActive(false);
    }


}
