using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioNavigation : MonoBehaviour
{
    public ScenarioScript current_scenario, menu, start, lasgun, armor, spawn, tzeentch;

    public GameDirector director;

    private void Awake()
    {
        director = GetComponent<GameDirector> ();
    }

    public void UnpackExits()
    {
        // IF USING EXITS, TAKE IT FROM HERE
    }

    public void GoLeft()
    {
        current_scenario = armor;
        Debug.Log("Grabbing Armor!");
    }

    public void GoRight()
    {
        current_scenario = lasgun;
        Debug.Log("Grabbing the Lasgun!");
    }

    // Going north is used only in one room. Thus there's only one option
    public void GoNorth()
    {
        current_scenario = spawn;
        Debug.Log("Going towards Certain Doom!");
    }

    // Going south is used only in one room. Thus there's only one option
    public void GoSouth()
    {
        current_scenario = start;
        Debug.Log("Back to the corridor!");
    }

    public void Obliterate()
    {
        current_scenario = tzeentch;
        Debug.Log("All is touched by the Architect of Fate...");
    }


    public void MainMenu()
    {
        current_scenario = menu;
    }

    // Set current Scenario to "Start"
    public void StartGame()
    {
        current_scenario = start;
    }


}
