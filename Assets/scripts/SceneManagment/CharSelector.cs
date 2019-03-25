using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelector : MonoBehaviour
{
    [Header("Character Manager")]
    public GameObject[] characters;
    GameObject currentCharacter;

    public int selectedCharacter = 0;

    public Player player;

    [Header("UIVariables")]
    public float selectSpriteSize; 

    float selectTime;
    float timePressed; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (timePressed > selectTime)
        {

        }
    }

    void OnButtonDown()
    {

    }

    void OnButtonUp ()
    {
        if (timePressed < selectTime )
        {
            timePressed = 0;
            NextCharacter(); 
        }
        
    }

    void NextCharacter ()
    {
        Destroy(currentCharacter);

        selectedCharacter++;

        if (selectedCharacter >= characters.Length )
        {
            selectedCharacter = 0; 
        }

        currentCharacter = Instantiate(characters[selectedCharacter], this.transform);
        
        
    }


   IEnumerator OnCharacterSelect ()
    {

        return null; 
    }
}
