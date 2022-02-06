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
    [SerializeField] string filename;

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
        
    }

    void Update()
    {
        //DEVELOPER STUFF TO MAKE SURE MY SPAGHETTI CODE WORKED :)
        ToggleHotDoor();
        ToggleNoisyDoor();
        ToggleSafeDoor();
        //END OF DEVELOPER STUFF



    }

    //These functions literally just serve the purpose of making sure that 
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
