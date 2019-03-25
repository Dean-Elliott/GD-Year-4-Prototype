using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public SpriteRenderer[] playerBaseColourSprites;
    

    BaseCharacter myCharacter;
    public Player myPlayerInfo;

    [Header("Button Indicator")]
    public SpriteRenderer buttonIndicatorSprite;
    [Range(0,1)]
    public float buttonDownThisFrameIndicatorAlpha;
    [Range(0, 1)]
    public float buttonHeldDownIndicatorAlpha;
    [Range(0, 1)]
    public float buttonupUpThisFrameIndicatorAlpha;
    [Range(0, 1)]
    public float buttonHeldUpIndicatorAlpha;

    private void Awake()
    {
        myCharacter = GetComponent<BaseCharacter>();
    }

    private void Start()
    {
        foreach(SpriteRenderer baseColourSprite in playerBaseColourSprites)
        {
            baseColourSprite.color = GameManager.gameManagerInstance.playerColors[myPlayerInfo.playerID];
        }  
    }

    private void Update()
    {
        SetButtonIndicatorAlpha();
    }

    public void SetButtonIndicatorAlpha()
    {
        if (myCharacter.isButtonHeldUp)
        {
            buttonIndicatorSprite.color = GetSameColourNewAlpha(buttonIndicatorSprite.color, buttonHeldUpIndicatorAlpha);
        }
        if (myCharacter.isButtonHeldDown)
        {
            buttonIndicatorSprite.color = GetSameColourNewAlpha(buttonIndicatorSprite.color, buttonHeldDownIndicatorAlpha);
        }
        else if (myCharacter.isButtonUpThisFrame)
        {
            buttonIndicatorSprite.color = GetSameColourNewAlpha(buttonIndicatorSprite.color, buttonupUpThisFrameIndicatorAlpha);
        }
        else if (myCharacter.isButtonDownThisFrame)
        {
            buttonIndicatorSprite.color = GetSameColourNewAlpha(buttonIndicatorSprite.color, buttonDownThisFrameIndicatorAlpha);
        }
    }

    public Color GetSameColourNewAlpha(Color existingColor, float newAlpha)
    {
       return new Color(existingColor.r, existingColor.g, existingColor.b, newAlpha);
    }
}
