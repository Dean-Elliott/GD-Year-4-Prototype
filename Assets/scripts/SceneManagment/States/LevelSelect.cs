using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : State
{ 
    public override void OnEnterState()
    {
        base.OnEnterState();

        base.mm.mapSelectCanvas.SetActive(true);

        foreach (Map map in mm.filteredMaps)
        {

            Button btn = UnityEngine.GameObject.Instantiate(mm.buttonPrefab, mm.levelButtonPanel.transform);
            if (map == mm.filteredMaps[0])
            {
                mm.currentES.SetSelectedGameObject(btn.gameObject);
                mm.startButton = btn;
            }
            btn.transform.GetComponentInChildren<Text>().text = map.name;
            btn.onClick.AddListener(delegate { mm.OnMapSelect(map.sceneName); });
            btn.onClick.AddListener(delegate { mm.ChangeToCharacterSelect(); });
        }

    }
    public override void Update()
    {

        base.Update();
    }

    public override void OnExitState ()
    {
        base.mm.mapSelectCanvas.SetActive(false);
    }
}
