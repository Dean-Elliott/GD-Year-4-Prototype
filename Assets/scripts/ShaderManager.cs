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

    #region Player Info

    [Header("Player Info")]

    public Player redPlayerInfo;
    public Player bluePlayerInfo;
    public Player greenPlayerInfo;
    public Player yellowPlayerInfo;

    #endregion

    #region Particle Systems 

    [Header("Particle Systems")]

    public ParticleSystem redParticle;
    public ParticleSystem blueParticle;
    public ParticleSystem greenParticle;
    public ParticleSystem yellowParticle;
    #endregion

    #region Scripts

    [Header("Scripts")]

    public FreeForAllGameMode FFAScript;



    #endregion


    void Awake()
    {
        
    }

    void Start()
    {
        redParticle.Play();
        blueParticle.Play();
        greenParticle.Play();
        yellowParticle.Play();
      
    }

    void Update()
    {
        ColorChanger();

    }
    void ColorChanger()
    {
        #region Variables

        var redEmission = redParticle.emission;
        var blueEmission = blueParticle.emission;
        var greenEmission = greenParticle.emission;
        var yellowEmission = yellowParticle.emission;

        #endregion

        #region Red Player

        ////Red Player
        if (FFAScript.playerScores[0] > FFAScript.playerScores[1])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;
            redEmission.rateOverTime = 10;
        }
        else
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;
            redEmission.rateOverTime = 0;
        }

        if (FFAScript.playerScores[0] > FFAScript.playerScores[2])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;
            redEmission.rateOverTime = 10;
        }
        else
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;
            redEmission.rateOverTime = 0;
        }

        if (FFAScript.playerScores[0] > FFAScript.playerScores[3])
        {
            redPlayer.GetComponent<Renderer>().material = redHolo;
            redEmission.rateOverTime = 10;
        }

        else
        {
            redPlayer.GetComponent<Renderer>().material = standardMat;
            redEmission.rateOverTime = 0;
        }

        #endregion

        #region Blue Player

        ////Blue Player

        if (FFAScript.playerScores[1] > FFAScript.playerScores[0])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;
            blueEmission.rateOverTime = 10;
        }
        else
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;
            blueEmission.rateOverTime = 0;

        }

        if (FFAScript.playerScores[1] > FFAScript.playerScores[2])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;
            blueEmission.rateOverTime = 10;
        }
        else
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;
            blueEmission.rateOverTime = 0;

        }

        if (FFAScript.playerScores[1] > FFAScript.playerScores[3])
        {

            bluePlayer.GetComponent<Renderer>().material = blueHolo;
            blueEmission.rateOverTime = 10;
        }
        else
        {
            bluePlayer.GetComponent<Renderer>().material = standardMat;
            blueEmission.rateOverTime = 0;

        }

        #endregion

        #region Green Player

        ////Green Player

        if (FFAScript.playerScores[2] > FFAScript.playerScores[0])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;
            greenEmission.rateOverTime = 10;
        }
        else
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;
            greenEmission.rateOverTime = 0;

        }

        if (FFAScript.playerScores[2] > FFAScript.playerScores[1])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;
            greenEmission.rateOverTime = 10;
        }
        else
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;
            greenEmission.rateOverTime = 0;

        }

        if (FFAScript.playerScores[2] > FFAScript.playerScores[3])
        {

            greenPlayer.GetComponent<Renderer>().material = greenHolo;
            greenEmission.rateOverTime = 10;
        }
        else
        {
            greenPlayer.GetComponent<Renderer>().material = standardMat;
            greenEmission.rateOverTime = 0;

        }

        #endregion

        #region Yellow Player
        ////Yellow Player

        if (FFAScript.playerScores[3] > FFAScript.playerScores[0])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;
            greenEmission.rateOverTime = 10;
        }
        else
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;
            yellowEmission.rateOverTime = 0;

        }

        if (FFAScript.playerScores[3] > FFAScript.playerScores[1])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;
            yellowEmission.rateOverTime = 10;
        }
        else
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;
            yellowEmission.rateOverTime = 0;

        }

        if (FFAScript.playerScores[3] > FFAScript.playerScores[2])
        {

            yellowPlayer.GetComponent<Renderer>().material = yellowHolo;
            yellowEmission.rateOverTime = 10;
        }
        else
        {
            yellowPlayer.GetComponent<Renderer>().material = standardMat;
            yellowEmission.rateOverTime = 0;

        }

        #endregion
    }
    void InstantiateParticles()
    {
        Instantiate(redParticle, redPlayer.transform.position, Quaternion.identity);
        Instantiate(blueParticle, bluePlayer.transform.position, Quaternion.identity);
        Instantiate(greenParticle, greenPlayer.transform.position, Quaternion.identity);
        Instantiate(yellowParticle, yellowPlayer.transform.position, Quaternion.identity);


    }
}