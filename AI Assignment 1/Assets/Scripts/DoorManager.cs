/**************************************************
*   AI for Games - Assignment 1
*
*   Carter Menary, 100700587
*   2022-02-06
**************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class DoorManager : MonoBehaviour
{
    [Header("Door List")]
    [Tooltip("The 20 doors in the scene.")]
    [SerializeField] GameObject[] Doors;

    [Header("File")]
    [Tooltip("This is the stuff for the .txt file.")]
    [SerializeField] string filename; //the name of the file, this is to make sure reading the file works properly
    [SerializeField] GameObject path;
    string textInput = "";

    bool[,] txtFile = new bool[8, 3]; // [x, y] x = number of variations, y = hot, noisy, safe
    float[] percOfDoors = new float[8]; // 8 variations

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
        //Initializes the doors to the file provided
        ReadTextFile(filename);
        SetDoorProperties();
        
        //This function does exactly what the main one does, except it doesn't read a file
        //Used for testing purposes
        //SetDoorPropertiesTEST();
    }

    void Update()
    {
        //DEVELOPER STUFF TO MAKE SURE MY SPAGHETTI CODE WORKED :)
        ToggleHotDoor();
        ToggleNoisyDoor();
        ToggleSafeDoor();
        //END OF DEVELOPER STUFF

        textInput = path.GetComponent<TMP_InputField>().text;   //this line took over 2 hours to get working

        if (textInput != filename && File.Exists(textInput)) {    //Just so it doesn't spam the search when you've found it
            ReadTextFile(textInput);
            SetDoorProperties();
            filename = textInput;
        }
    }

    bool ReadTextFile(string _path) {
        if (_path == "") {
            return false;
        }

        string lines = "";

        StreamReader reader = new StreamReader(_path);

        lines = reader.ReadLine(); //Header is omitted from the data reading

        for (int i = 0; !reader.EndOfStream; i++) {
            lines = reader.ReadLine();
            string s = lines.Trim().Replace("\t", string.Empty).Replace(" ", string.Empty);    //Gets rid of any tabs or spaces

            //s is the trimmed line
            char[] lineC = s.ToCharArray();
            //go through it char by char for the first 3, then everything after that is assumed to be a 0.xx float
            
            //FIRST CHAR - HOT
            if (lineC[0] == 'Y') txtFile[i, 0] = true;
            else if (lineC[0] == 'N') txtFile[i, 0] = false;
            
            //SECOND CHAR - NOISY
            if (lineC[1] == 'Y') txtFile[i, 1] = true;
            else if (lineC[1] == 'N') txtFile[i, 1] = false;
            
            //THIRD CHAR - SAFE
            if (lineC[2] == 'Y') txtFile[i, 2] = true;
            else if (lineC[2] == 'N') txtFile[i, 2] = false;
            
            //PROBABILITY
            string num = s.Substring(3); //remove the first 3 chars from it, leaving just the number
            float prob = float.Parse(num);
            percOfDoors[i] = prob;
        }

        return true;
    }

    void SetDoorProperties() {
        bool[,] doorProps = new bool[20, 3]; // [x, y] x = door number -1, y = hot, noisy, safe
        int[] numOfDoors = new int[8]; // 8 variations

        //Goes through and figures out the number of doors each variation has
        for (int i = 0; i < numOfDoors.Length; i++) {
            numOfDoors[i] = (Mathf.RoundToInt((float)Doors.Length * percOfDoors[i])); //Rounds to the nearest int as well
        }

        int doorVariation = 0;
        int whichDoor = 0;

        for (int d = 0; d < (doorProps.Length / 3); d++) {    //This cycles through each door, d = door number
            if (whichDoor >= numOfDoors[doorVariation]) {
                //if the door number in the variation exceeds the amount of doors in that variation, reset the door number for the variation and move to the next one
                whichDoor = 0;
                if (doorVariation >= doorProps.Length) break;
                doorVariation++;
            }
            if (whichDoor < numOfDoors[doorVariation]) { //if the current door is more than the total number of doors for this variation
                for (int i = 0; i < 3; i++)     //i is the property (0 = hot, 1 = noisy, 2 = safe)
                    doorProps[d, i] = txtFile[doorVariation, i];
                whichDoor++; //one more door done
            }
            //Set the values of the doors here
            Doors[d].GetComponent<DoorProperties>().setIsHot(doorProps[d, 0]);
            Doors[d].GetComponent<DoorProperties>().setIsNoisy(doorProps[d, 1]);
            Doors[d].GetComponent<DoorProperties>().setIsSafe(doorProps[d, 2]);
        }

        print("if you're seeing this, it should've worked");
    }

    //These functions below just serve the purpose of making sure that everything worked properly. Left it in to show my work
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

    void SetDoorPropertiesTEST() {
        bool[,] doorProps = new bool[20, 3]; // [x, y] x = door number -1, y = hot, noisy, safe
        int[] numOfDoors = new int[8]; // 8 variations

        //These are dummy values copied from the .txt file, purely for testing the system
        bool[,] txtFileTEST = new bool[8,3]{    {true, true, true},     //0.05 - 1
                                                {true, true, false},    //0.30 - 6
                                                {true, false, true},    //0.03 - 1
                                                {true, false, false},   //0.21 - 4
                                                {false, true, true},    //0.06 - 1
                                                {false, true, false},   //0.11 - 2
                                                {false, false, true},   //0.20 - 4
                                                {false, false, false},  //0.04 - 1
        };
        float[] percOfDoorsTEST = {0.05f, 0.30f, 0.03f, 0.21f, 0.06f, 0.11f, 0.20f, 0.04f};

        //Goes through and figures out the number of doors each variation has
        for (int i = 0; i < numOfDoors.Length; i++) {
            numOfDoors[i] = (Mathf.RoundToInt((float)Doors.Length * percOfDoorsTEST[i])); //Rounds to the nearest int as well
        }

        int doorVariation = 0;
        int whichDoor = 0;

        for (int d = 0; d < (doorProps.Length / 3); d++) {    //This cycles through each door, d = door number
            if (whichDoor >= numOfDoors[doorVariation]) {
                //if the door number in the variation exceeds the amount of doors in that variation, reset the door number for the variation and move to the next one
                whichDoor = 0;
                doorVariation++;
            }
            if (whichDoor < numOfDoors[doorVariation]) { //if the current door is more than the total number of doors for this variation
                for (int i = 0; i < 3; i++)     //i is the property (0 = hot, 1 = noisy, 2 = safe)
                    doorProps[d, i] = txtFileTEST[doorVariation, i];
                whichDoor++; //one more door done
            }
            //Set the values of the doors here
            Doors[d].GetComponent<DoorProperties>().setIsHot(doorProps[d, 0]);
            Doors[d].GetComponent<DoorProperties>().setIsNoisy(doorProps[d, 1]);
            Doors[d].GetComponent<DoorProperties>().setIsSafe(doorProps[d, 2]);
        }

        print("if you're seeing this, it should've worked");
    }

}