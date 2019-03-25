using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class MapSelector : MonoBehaviour
{



    [Header("UI Variables")]

    public Button buttonPrefab;
    public GameObject levelButtonPanel;

    public GameObject modeSelectCanvas;
    public GameObject mapSelectCanvas;

    EventSystem currentES;
    Button currentButton;


    GameObject selectBar;


    [Space(10)]
    public float selectTime;
    public Button startButton; 

    bool buttonPressed; 
    float timePressed; 

    [Header("Scene Management Variables")]
    public GameManager gm;
    public Map[] allMaps;
    
    List<Map> filteredMaps;

    private void Start()
    {
        currentES = GameObject.FindObjectOfType<EventSystem>();
        currentES.SetSelectedGameObject(startButton.gameObject);
        currentButton = startButton;
        selectBar = currentButton.transform.Find("SelectBar").gameObject;
    }


    private void Update()
    {
        
        if (Input.GetButton("0"))
        {
            timePressed += Time.deltaTime;

            float barScale = timePressed / selectTime;
            selectBar.GetComponent<RectTransform>().localScale = new Vector3(barScale, 1, 1);
        }

        if (timePressed > selectTime)
            ButtonPressed(); 

        if (Input.GetButtonUp("0"))
        {
            OnUIButtonUp();
        }
    }

    void ButtonPressed ()
    {
        selectBar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        timePressed = 0; 
        currentButton.onClick.Invoke();
    }


    void OnUIButtonUp()
    {
        if (timePressed < selectTime)
            NextUIElement(); 

        timePressed = 0;
    }


    public void NextUIElement ()
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
            
        } else
        {
            currentButton = startButton;
            currentES.SetSelectedGameObject(currentButton.gameObject);
        }

        selectBar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        selectBar = currentButton.transform.Find("SelectBar").gameObject;

    }
    public void OnGameModeSelect (string selectedGameMode)
    {
        filteredMaps = new List<Map>();
        foreach (Map map in allMaps)
        {
            foreach (GameManager.GameMode mode in map.supportedGameModes)
            {
                if (mode.ToString() == selectedGameMode)
                { 
                    filteredMaps.Add(map);
                    break;
                }
            }
        }


        // DISPLAY UI FOR MAP SELECTOR; 

        modeSelectCanvas.SetActive(false);
        mapSelectCanvas.SetActive(true);

        foreach (Map map in filteredMaps)
        {
 
            Button btn = Instantiate(buttonPrefab, levelButtonPanel.transform);
            if (map == filteredMaps[0])
            {
                currentES.SetSelectedGameObject(btn.gameObject);
                startButton = btn;
            }
            btn.transform.GetComponentInChildren<Text>().text = map.name; 
            btn.onClick.AddListener(delegate { OnMapSelect(map.sceneName); });
        }     
    }

    public void OnMapSelect (string selectedMap)
    {
        SceneManager.LoadScene(selectedMap);
    }

    

}
