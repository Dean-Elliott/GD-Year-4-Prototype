using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateMachine : MonoBehaviour
{

    private Dictionary<string, State> m_states = new Dictionary<string, State>();

    public State m_currentState;

    void Update()
    {
        if (null != m_currentState)
        {
            m_currentState.Update();
        }
    }

    public bool AddState(string stateName, State newState)
    {
        if (!m_states.ContainsKey(stateName))
        {
            m_states.Add(stateName, newState);
            return true;
        }

        return false;
    }


    public void ChangeToState(string stateName)
    {
        State newState;
        if (m_states.TryGetValue(stateName, out newState))
        {
            if (null != m_currentState)
            {
                m_currentState.OnExitState();
            }
            m_currentState = newState;
            m_currentState.OnEnterState();
        }
    }
}

public abstract class State
{
    public  MenuManager mm; 
    public virtual void Update()
    {
        mm.CheckInput();     
    }

    public virtual void OnEnterState() {
        mm = UnityEngine.Object.FindObjectOfType<MenuManager>();
    }

    public virtual void OnExitState() {
        mm.mapSelectCanvas.SetActive(false);
    }

}
