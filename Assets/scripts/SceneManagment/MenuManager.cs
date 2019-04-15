using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems; 

public class MenuManager : MonoBehaviour
{
    MenuStateMachine m_msm;
    public GameManager gm;

    public EventSystem currentES;
    Button currentButton;

    GameObject selectBar;

    public const string LEVEL_SELECT = "LevelSelect";
    public const string MODE_SELECT = "ModeSelect";
    public const string CHAR_SELECT = "CharSelect";

    [Header ("Mode Select Variables")]
    public GameObject modeSelectCanvas;


    [Space(10)]
    public float selectTime;
    public Button startButton;

    bool buttonPressed;
    float[] timePressed = new float[4];

    [Header("Character Select Variables")]

    public GameObject charSelectCanvas;

    public ReadyZone readyZone; 
    public Player[] players = new Player[4];
    public Transform[] spawnPoints = new Transform[4];

    int[] selectedChar = new int[4];
    
    public List<GameObject> characterPrefabs; 
    
    [Header("Level Select Variables")]
    public GameObject mapSelectCanvas;

    public Button buttonPrefab;
    public GameObject levelButtonPanel;

    public Map[] allMaps;

    [HideInInspector]
    public List<Map> filteredMaps;

    string selectedMap;

    void Start()
    {
        m_msm = GetComponent<MenuStateMachine>();

        m_msm.AddState(LEVEL_SELECT, new LevelSelect());
        m_msm.AddState(MODE_SELECT, new GameModeSelect());
        m_msm.AddState(CHAR_SELECT, new CharacterSelect());


        m_msm.ChangeToState(MODE_SELECT);

        characterPrefabs = new List<GameObject>(gm.allCharacterPrefabs);
        currentES = GameObject.FindObjectOfType<EventSystem>();
        currentES.SetSelectedGameObject(startButton.gameObject);
        currentButton = startButton;
        selectBar = currentButton.transform.Find("SelectBar").gameObject;

    }

    public void OnLevelWasLoaded(int level)
    {
        players = FindObjectOfType<TempGameModeInitializer>().spoofedPlayerData;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CheckMultipleInputs ()
    {
        foreach (Player player in players)
        {
            int ID = player.playerID; 
            if (Input.GetButton(player.inputButton))
            {
                
                timePressed[ID] += Time.deltaTime;

                if (timePressed[ID] > selectTime)
                {
                    SelectCharacter(player.activeCharacterInScene);
                    timePressed[ID] = 0;
                }
            }

            if (Input.GetButtonUp(player.inputButton))
            {
                if (!player.isActive)
                {
                    player.isActive = true;
                }
                else if (timePressed[ID] < selectTime)
                {
                    if (player.activeCharacterInScene != null)
                    {
                        if (player.activeCharacterInScene.GetComponent<Rigidbody2D>().isKinematic)
                            NextCharacter(ID);
                    } else
                    {
                        NextCharacter(ID);
                    }



                }
                timePressed[ID] = 0;
            }
          
        }
    }


    public void ReadyUp()
    {
        int activePlayers= 0;

        foreach (Player player in players)
        {
            if (player.isActive)
            {
                activePlayers++;
            }
        }

        if (activePlayers > 1)
        {

if (readyZone.playersInZone == activePlayers)
            {
                foreach (Player player in players)
                {
                    Destroy(player.activeCharacterInScene);
                }
                SceneManager.LoadScene(selectedMap);
            }
        }
    }
    public void NextCharacter( int playerID)
    {
        Player player = players[playerID]; 
        if (player.characterSelectionPrefab == null)
        {
            selectedChar[playerID] = 0;
            player.characterSelectionPrefab = gm.allCharacterPrefabs[0];
        } else
        {
            selectedChar[playerID]++;   

            if (selectedChar[playerID] > gm.allCharacterPrefabs.Length - 1)
            {
                selectedChar[playerID] = 0; 
            }

            player.characterSelectionPrefab = gm.allCharacterPrefabs[selectedChar[playerID]];

            Destroy(player.activeCharacterInScene);
        }

        player.activeCharacterInScene = Instantiate(gm.allCharacterPrefabs[selectedChar[playerID]], players[playerID].transform);

        GameObject playerCharacter = player.activeCharacterInScene;
        playerCharacter.transform.position = spawnPoints[playerID].position;

        player.activeCharacterInSceneCharacterScript = player.activeCharacterInScene.GetComponent<BaseCharacter>();
        player.activeCharacterInSceneCharacterScript.inputButton = player.inputButton;
        player.activeCharacterInSceneCharacterScript.myPlayerInfo = player;
        player.activeCharacterInScene.GetComponent<PlayerVisuals>().myPlayerInfo = player;
        playerCharacter.GetComponentInChildren<HitBox>().myCharacter = player.activeCharacterInSceneCharacterScript;
        playerCharacter.GetComponentInChildren<HitBox>().myPlayerID = playerID;
        // playerCharacter.GetComponentInChildren<HitBox>().activeGameMode = activeGameMode;

        SpawnShield sheild = playerCharacter.GetComponentInChildren<SpawnShield>();
        sheild.myCharacter = player.activeCharacterInSceneCharacterScript;
        sheild.PopShield();
       


        DisableCharacterForUI(player.activeCharacterInScene);
    }
    public void SelectCharacter(GameObject character)
    {
        EnableCharacter(character);
    }

    public void DisableCharacterForUI (GameObject character)
    {
        character.GetComponent<Rigidbody2D>().isKinematic = true;
        character.GetComponent<basicCharacter>().enabled = false;

       
        

    }

    public void EnableCharacter(GameObject character)
    {
        character.GetComponent<Rigidbody2D>().isKinematic = false;
        character.GetComponent<basicCharacter>().enabled = true;
    }

    public void CheckInput()
    {
        if (Input.GetButton("0"))
        {
            timePressed[0] += Time.deltaTime;

            float barScale = timePressed[0] / selectTime;
            selectBar.GetComponent<RectTransform>().localScale = new Vector3(barScale, 1, 1);
        }

        if (timePressed[0] > selectTime)
            ButtonPressed();

        if (Input.GetButtonUp("0"))
        {
            OnUIButtonUp();
        }

    }

    void OnUIButtonUp()
    {
        if (timePressed[0] < selectTime)
            NextUIElement();

        timePressed[0] = 0;
    }


    void NextUIElement()
    {

        if (currentButton == null)
        {
            currentButton = startButton;
        }

        Selectable nextButton = currentButton.FindSelectableOnDown();


        if (nextButton != null)
        {
            currentButton = nextButton.GetComponent<Button>();
            currentES.SetSelectedGameObject(currentButton.gameObject);

        }
        else
        {
            currentButton = startButton;
            currentES.SetSelectedGameObject(currentButton.gameObject);
        }

        selectBar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        selectBar = currentButton.transform.Find("SelectBar").gameObject;

    }

    void ButtonPressed()
    {
        selectBar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        timePressed[0] = 0;
        currentButton.onClick.Invoke();
    }

    public void SelectGameMode (string gameMode)
    {
        filteredMaps = new List<Map>();
        foreach (Map map in allMaps)
        {
            foreach (GameManager.GameMode mode in map.supportedGameModes)
            {
                if (mode.ToString() == gameMode)
                {
                    filteredMaps.Add(map);
                    break;
                }
            }
        }
    }

    public void OnMapSelect(string map)
    {
        selectedMap = map;
    }

    public void ChangeToLevelSelect()
    {
        m_msm.ChangeToState(LEVEL_SELECT);
    }

    public void ChangeToGameModeSelect()
    {
        m_msm.ChangeToState(MODE_SELECT);
    }

    public void ChangeToCharacterSelect()
    {
        m_msm.ChangeToState(CHAR_SELECT);
    }
}
