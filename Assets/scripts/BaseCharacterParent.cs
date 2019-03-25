using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used so that certain methods can be sealed in the BaseCharacter class that inherits from this, preventing unintended overriding in unique characters that inherit from BaseCHaracter.
public class BaseCharacterParent : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Awake()
    {        
    }

    // Update is called once per frame
    protected virtual void Update()
    {      
    }

    protected virtual void ButtonDownThisFrame()
    {
    }

    protected virtual void ButtonUpThisFrame()
    {
    }

}
