using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [HideInInspector] public ScenarioNavigation navigation;
    [HideInInspector] public ScenarioExits[] exits;
    [HideInInspector] public List<string> Interactions = new List<string>();
    [SerializeField] public Button button_south, button_north, button_west, button_east, button_start;
    public GameObject button0, button1, button2, button3, buttonstart, title;
    public Text displayText, game_title, button0_text, button1_text, button2_text, button3_text, buttonstart_text;
    public string text;
    public bool goingOnce = true, gameStart = false, posEast = false, posWest = false, buttonOnce = true;
    public bool lasgun = false, armor = false, gameEnd = false;

    public AudioSource tzeentch;

    private void Awake()
    {
       navigation = GetComponent<ScenarioNavigation>();

       button0 = GameObject.Find("ButtonSouth");
       button1 = GameObject.Find("ButtonWest");
       button2 = GameObject.Find("ButtonNorth");
       button3 = GameObject.Find("ButtonEast");

       buttonstart = GameObject.Find("ButtonStart");
       title = GameObject.Find("Title");

       tzeentch = GetComponent<AudioSource>();
       HideButtons();
       //Debug.Log("test");
    }

    public void DisplayScenarioText(bool alt_text)
    {
        displayText.text = " ";
        text = " ";

        if(alt_text)
        {
            text = navigation.current_scenario.scenario_alt_text + "\n";
            TextWritingScript.TextWritingScript_Static(displayText, text, 0.04f);
        }
        else
        {
            if(navigation.current_scenario.scenario_name == "Scenario3" && lasgun)
            {
                text = "After you picked up the Lasgun, there is no longer a blinking light here to guide you in the darkness." + "\n" + "\n" + "You decide that the best course of action is to return to a location where you can see." + "\n";
                TextWritingScript.TextWritingScript_Static(displayText, text, 0.04f);
            }
            else
            {
                text = navigation.current_scenario.scenario_text + "\n";
                TextWritingScript.TextWritingScript_Static(displayText, text, 0.04f);
            }
            
        }
        

        //text = navigation.current_scenario.scenario_text + "\n";
        //TextWritingScript.TextWritingScript_Static(displayText, text, 0.04f);

        goingOnce = true;
    }

    public void DisplayMainMenu(bool end)
    {
        if(end)
        {
            tzeentch.Play();

            buttonstart.SetActive(false);
            title.SetActive(true);
            text = navigation.current_scenario.scenario_alt_text + "\n";
            TextWritingScript.TextWritingScript_Static(displayText, text, 0.002f);

            TextWritingScript.TextWritingScript_Static(game_title, "THE END", 1f);
        }
        else
        {
            HideButtons();
            buttonstart.SetActive(false);

            text = navigation.current_scenario.scenario_text + "\n";
            TextWritingScript.TextWritingScript_Static(displayText, text, 0.002f);
            //gameStart = true;
            TextWritingScript.TextWritingScript_Static(game_title, "THE GUARDSMAN'S LAST STAND", 0.29f);

            button_start.onClick.AddListener(StartGame);
        }
        
    }

    // Function used to hide buttons (render them inactive) and cleanse their text (to rewrite it when needed)
    void HideButtons()
    {
        button0.SetActive(false);
        button0_text.text = " ";
        button1.SetActive(false);
        button1_text.text = " ";
        button2.SetActive(false);
        button2_text.text = " ";
        button3.SetActive(false);
        button3_text.text = " ";
    }

    // ALL BUTTON FUNCTION SHARE THIS:
    /*
     * 1) Cleanses the displaytext
     * 2) Runs HideButtons() - line 56
     * 3) Uses navigation's respective function (GoNorth, GoSouth, etc.) - see ScenarioNavigation.cs for more information
     * 4) Displays the current Scenario (AKA the new Scenario's text).
     */
    void clickButtonNorth()
    {
        displayText.text = " ";
        HideButtons();
        navigation.GoNorth();
        DisplayScenarioText(lasgun);
    }

    void clickButtonSouth()
    {
        displayText.text = " ";
        HideButtons();
        navigation.GoSouth();
        DisplayScenarioText(true);
    }

    // If posWest == true, then return to main room. Else, go to Lasgun room
    void clickButtonEast()
    {
        if(posWest)
        {

            Debug.Log("positionWest is true, turning it into False and going to the middle");
            navigation.GoSouth();
            DisplayScenarioText(true);
        }
        else
        {
            Debug.Log("positionWest is false, going to the right");
            navigation.GoRight();
            DisplayScenarioText(armor);
        }

        HideButtons();

    }

    // If posEast (the guardsmen is in the lasgun room) is NOT true, that means the guardsmen is trying to reach the Boiler Room. Make him get there and set posWest to true.
    // If posEast IS true, then the guardsmen is in the boilerroom. Make him return to the main room and then set PosEast to false.

    void clickButtonWest()
    {
        HideButtons();

        if(posEast)
        {
            Debug.Log("positionEast is true, turning it into False and going to the middle");
            navigation.GoSouth();
            DisplayScenarioText(true);
        }
        else
        {
            Debug.Log("positionEast is false, going to the left");
            navigation.GoLeft();
            DisplayScenarioText(armor);
        }
        
        
    }

    void SetButtons()
    {
        button_south.onClick.AddListener(clickButtonSouth);
        button_west.onClick.AddListener(clickButtonWest);
        button_north.onClick.AddListener(clickButtonNorth);
        button_east.onClick.AddListener(clickButtonEast);
    }

    void ShowButtons()
    {
        SetButtons();

        //TextWritingScript.TextWritingScript_Static(button0_text, "Your back is against the wall.\nYou cannot go this direction (SOUTH)", 0.04f);

        string switchcase = navigation.current_scenario.scenario_name;

        switch (switchcase)
        {
            
            case "Scenario1":
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);

                TextWritingScript.TextWritingScript_Static(button1_text, "Go Towards an Intense Heat (WEST)", 0.04f);
                TextWritingScript.TextWritingScript_Static(button2_text, "Go Towards a Strange Sound (NORTH)", 0.04f);
                TextWritingScript.TextWritingScript_Static(button3_text, "Go Towards a Blinking Light (EAST)", 0.04f);

                posWest = false;
                posEast = false;

                //button0.SetActive(true);
                break;
            case "Scenario2":
                button3.SetActive(true);
                TextWritingScript.TextWritingScript_Static(button3_text, "You return from where you came (EAST)", 0.04f);

                if(!armor)
                {
                    armor = true;
                }

                posWest = true;
                posEast = false;

                break;
            case "Scenario3":
                button1.SetActive(true);
                TextWritingScript.TextWritingScript_Static(button1_text, "You return from where you came (WEST)", 0.04f);

                if (armor && !lasgun)
                {
                    lasgun = true;
                }

                posWest = false;
                posEast = true;

                break;
            case "Scenario4":
                if(!lasgun)
                {
                    button0.SetActive(true);
                    TextWritingScript.TextWritingScript_Static(button0_text, "You run back from where you came (SOUTH)", 0.04f);
                    break;
                }
                else
                {
                    buttonstart.SetActive(true);
                    TextWritingScript.TextWritingScript_Static(buttonstart_text, "You pray to the God Emperor one last time...", 0.04f);
                    //buttonstart_text.text = "You pray to the God Emperor one last time...";
                    break;
                }
            case "Scenario5":

                buttonstart.SetActive(true);
                TextWritingScript.TextWritingScript_Static(buttonstart_text, "Goodbye, Guardsmen", 0.04f);

                gameEnd = true;
                break;
        }
       
       

        
    }

    void UnpackScenario()
    {
        navigation.UnpackExits();
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayMainMenu(gameEnd);
    }

    void StartGame()
    {
        if(!lasgun)
        {
            navigation.StartGame();
            gameStart = true;

            displayText.text = " ";
            buttonstart.SetActive(false);
            title.SetActive(false);

            DisplayScenarioText(false);
        }
        else
        {
            if(!gameEnd)
            {
                HideButtons();
                Debug.Log("The prayer is lost in the void...");
                navigation.Obliterate();
                buttonstart.SetActive(false);
                DisplayScenarioText(false);
            }
            else
            {
                HideButtons();
                Debug.Log("I WANT TO LIVE! I WANT TO-");
                navigation.MainMenu();
                buttonstart.SetActive(false);
                DisplayMainMenu(gameEnd);
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the text shown and the text to write are identical AND the buttons are hidden, show buttons.
        if(displayText.text == text && goingOnce)
        {
            Debug.Log("The string has finally ended. Thank the Emperor.");
            goingOnce = false;
            if(!gameStart)
            {
                buttonstart.SetActive(true);
                // MAKE THIS BUTTON BLINK (ARCADE FEEL IS IMPORTANT)
            }
            else
            {
                ShowButtons();
            }

        }
    }
}
