/**************************************************
*   AI for Games - Assignment 1
*
*   Carter Menary, 100700587
*   2022-02-06
**************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Door List")]
    [Tooltip("The 20 doors in the scene.")]
    [SerializeField] GameObject[] Doors;

    [Header("File")]
    [Tooltip("This is the stuff for the .txt file.")]
    [SerializeField] bool useFile; //when true, this will attempt to use the file
    [SerializeField] string filename; //the name of the file, this is to make sure reading the file works properly

    //ADD VARIABLES AND THINGS FOR THE ACTUAL PROBABILITIES AND STATES TO BE STORED IN

    [Header("Overwriting")]
    [Tooltip("This will overwrite any of the default states or imported file states. Purely for testing purposes.")]
    [SerializeField] bool toggleHot;
    bool tempToggleHot = false;
    [SerializeField] bool toggleNoisy;
    bool tempToggleNoisy = false;
    [SerializeField] bool toggleSafe;
    bool tempToggleSafe = false;

    void Start()
    {
        if (useFile && (filename != "")) {
            print("Using file located at \"" + filename + "\" for probabilities.");

            //read file and assign the values to the variables

            ProbabilityCalc();

            //other functions to set all the new values
        }
    }

    void Update()
    {
        //DEVELOPER STUFF TO MAKE SURE MY SPAGHETTI CODE WORKED :)
        ToggleHotDoor();
        ToggleNoisyDoor();
        ToggleSafeDoor();
        //END OF DEVELOPER STUFF

        

    }

    //PURELY TESTING THE WATERS RIGHT NOW TO FIGURE THIS ONE OUT
    void ProbabilityCalc() {
        int totalDoors = Doors.Length;

        float percentageOfDoors = 0.05f;

        int numberOfDoorsWithThis = (int)((float)totalDoors * percentageOfDoors);


    }

    //These functions below just serve the purpose of making sure that 
    //  the foreach loop and long single line to swap the property worked. 
    void ToggleHotDoor() {
        if (tempToggleHot == toggleHot)
            return;

        foreach (GameObject door in Doors)
            {
                door.GetComponent<DoorProperties>().setIsHot(!door.GetComponent<DoorProperties>().getIsHot());
            }
        tempToggleHot = !tempToggleHot;
    }
    void ToggleNoisyDoor() {
        if (tempToggleNoisy == toggleNoisy)
            return;

        foreach (GameObject door in Doors)
            {
                door.GetComponent<DoorProperties>().setIsNoisy(!door.GetComponent<DoorProperties>().getIsNoisy());
            }
        tempToggleNoisy = !tempToggleNoisy;
    }
    void ToggleSafeDoor() {
        if (tempToggleSafe == toggleSafe)
            return;

        foreach (GameObject door in Doors)
            {
                door.GetComponent<DoorProperties>().setIsSafe(!door.GetComponent<DoorProperties>().getIsSafe());
            }
        tempToggleSafe = !tempToggleSafe;
    }
}
