using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    #region Materials

    [Header("Materials")]

    public Material redHolo;
    public Material blueHolo;
    public Material greenHolo;
    public Material yellowHolo;
    public Material standardMat;

    #endregion

    #region GameObjects

    [Header("Game Objects")]

    private GameObject redPlayer;
    private GameObject bluePlayer;
    private GameObject greenPlayer;
    private GameObject yellowPlayer;

    #endregion

    #region Scripts

    [Header("Scripts")]

    public KingOfTheHillGameMode KOTHScript;
    public FreeForAllGameMode FFAScript;
    private basicCharacter characterScript;
    public GameMode gameModeScript;
    public Player[] playerScript;


    #endregion

    void Awake()
    {
        characterScript = GetComponent<basicCharacter>();
        //playerScript = GetComponent<Player>();
    }

    void Start()
    {

        redPlayer = gameModeScript.players[0].activeCharacterInScene;
        bluePlayer = gameModeScript.players[1].activeCharacterInScene;
        greenPlayer = gameModeScript.players[2].activeCharacterInScene;
        yellowPlayer = gameModeScript.players[3].activeCharacterInScene;


    }

    void Update()
    {
        redPlayer = gameModeScript.players[0].activeCharacterInScene;
        bluePlayer = gameModeScript.players[1].activeCharacterInScene;
        greenPlayer = gameModeScript.players[2].activeCharacterInScene;
        yellowPlayer = gameModeScript.players[3].activeCharacterInScene;

        //if (GameModeNames.FreeForAll)
        //{
        //    ColorChangerFFA();
        //}
        //else
        //{
        //    ColorChangerKOTH();
        //}




    }
    void ColorChangerFFA()
    {

        #region Red Player

        ////Red Player
        if (FFAScript.playerScores[0] > FFAScript.playerScores[1])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;

        }
        else if ((FFAScript.playerScores[0] < FFAScript.playerScores[1]))
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (FFAScript.playerScores[0] > FFAScript.playerScores[2])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;

        }
        else if (FFAScript.playerScores[0] < FFAScript.playerScores[2])
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (FFAScript.playerScores[0] > FFAScript.playerScores[3])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;

        }

        else if (FFAScript.playerScores[0] < FFAScript.playerScores[3])
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;

        }

        #endregion

        #region Blue Player

        ////Blue Player

        if (FFAScript.playerScores[1] > FFAScript.playerScores[0])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;

        }
        else if (FFAScript.playerScores[1] < FFAScript.playerScores[0])
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (FFAScript.playerScores[1] > FFAScript.playerScores[2])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;

        }
        else if (FFAScript.playerScores[1] < FFAScript.playerScores[2])
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (FFAScript.playerScores[1] > FFAScript.playerScores[3])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;

        }
        else if (FFAScript.playerScores[1] < FFAScript.playerScores[3])
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;


        }

        #endregion

        #region Green Player

        ////Green Player

        if (FFAScript.playerScores[2] > FFAScript.playerScores[0])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;

        }
        else if (FFAScript.playerScores[2] < FFAScript.playerScores[0])
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (FFAScript.playerScores[2] > FFAScript.playerScores[1])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;

        }
        else if (FFAScript.playerScores[2] < FFAScript.playerScores[1])
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (FFAScript.playerScores[2] > FFAScript.playerScores[3])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;

        }
        else if (FFAScript.playerScores[2] < FFAScript.playerScores[3])
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;


        }

        #endregion

        #region Yellow Player
        ////Yellow Player

        if (FFAScript.playerScores[3] > FFAScript.playerScores[0])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;

        }
        else if (FFAScript.playerScores[3] < FFAScript.playerScores[0])
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (FFAScript.playerScores[3] > FFAScript.playerScores[1])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;

        }
        else if (FFAScript.playerScores[3] < FFAScript.playerScores[1])
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (FFAScript.playerScores[3] > FFAScript.playerScores[2])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;

        }
        else if (FFAScript.playerScores[3] < FFAScript.playerScores[2])
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;

        }

        #endregion
    }
    void ColorChangerKOTH()
    {

        #region Red Player

        ////Red Player
        if (KOTHScript.playerScores[0] > KOTHScript.playerScores[1])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;

        }
        else if ((KOTHScript.playerScores[0] < KOTHScript.playerScores[1]))
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (KOTHScript.playerScores[0] > KOTHScript.playerScores[2])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;

        }
        else if (KOTHScript.playerScores[0] < KOTHScript.playerScores[2])
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (KOTHScript.playerScores[0] > KOTHScript.playerScores[3])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;

        }

        else if (KOTHScript.playerScores[0] < KOTHScript.playerScores[3])
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;

        }

        #endregion

        #region Blue Player

        ////Blue Player

        if (KOTHScript.playerScores[1] > KOTHScript.playerScores[0])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;

        }
        else if (KOTHScript.playerScores[1] < KOTHScript.playerScores[0])
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (KOTHScript.playerScores[1] > KOTHScript.playerScores[2])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;

        }
        else if (KOTHScript.playerScores[1] < KOTHScript.playerScores[2])
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (KOTHScript.playerScores[1] > KOTHScript.playerScores[3])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;

        }
        else if (KOTHScript.playerScores[1] < KOTHScript.playerScores[3])
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;


        }

        #endregion

        #region Green Player

        ////Green Player

        if (KOTHScript.playerScores[2] > KOTHScript.playerScores[0])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;

        }
        else if (KOTHScript.playerScores[2] < KOTHScript.playerScores[0])
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (KOTHScript.playerScores[2] > KOTHScript.playerScores[1])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;

        }
        else if (KOTHScript.playerScores[2] < KOTHScript.playerScores[1])
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;

        }

        if (KOTHScript.playerScores[2] > KOTHScript.playerScores[3])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;

        }
        else if (KOTHScript.playerScores[2] < KOTHScript.playerScores[3])
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;


        }

        #endregion

        #region Yellow Player
        ////Yellow Player

        if (KOTHScript.playerScores[3] > KOTHScript.playerScores[0])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;

        }
        else if (KOTHScript.playerScores[3] < KOTHScript.playerScores[0])
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (KOTHScript.playerScores[3] > KOTHScript.playerScores[1])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;

        }
        else if (FFAScript.playerScores[3] < FFAScript.playerScores[1])
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;


        }

        if (KOTHScript.playerScores[3] > KOTHScript.playerScores[2])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;

        }
        else if (KOTHScript.playerScores[3] < KOTHScript.playerScores[2])
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;

        }

        #endregion
    }
}