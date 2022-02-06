using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProperties : MonoBehaviour
{
    [Header("Door Properties")]
    [Tooltip("These are the properties of the door.")]
    [SerializeField] bool isHot;
    [SerializeField] bool isNoisy;
    [SerializeField] bool isSafe;

    [Header("Other Stuff")]
    [Tooltip("This is everything else that needs to be in here for this to work lol.")]
    [SerializeField] GameObject heatParticles;
    [SerializeField] GameObject audioEmitter; 

    // Start is called before the first frame update
    void Start()
    {
        if (isHot)
            heatParticles.SetActive(true);
        if (isNoisy)
            audioEmitter.SetActive(true);
        if (isSafe)
            ;   //it's safe :)
    }

    //Getters and Setters
    bool getIsHot() {   return isHot;   }
    void setIsHot(bool set) {   isHot = set;    }

    bool getIsNoisy() {   return isNoisy;   }
    void setIsNoisy(bool set) {   isNoisy = set;    }

    bool getIsSafe() {   return isSafe;   }
    void setIsSafe(bool set) {   isSafe = set;    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
