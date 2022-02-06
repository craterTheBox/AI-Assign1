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
    [SerializeField] GameObject safeCube;
    [SerializeField] GameObject unsafeCube;

    // Start is called before the first frame update
    void Start()
    {
        if (isHot)
            heatParticles.SetActive(true);
        if (isNoisy)
            audioEmitter.SetActive(true);
        if (isSafe)
            safeCube.SetActive(true);   //it's safe :)
        if (!isSafe)
            unsafeCube.SetActive(true); // :(
    }

    //Getters and Setters
    public bool getIsHot() {   return isHot;   }
    public void setIsHot(bool set) {   isHot = set;    }

    public bool getIsNoisy() {   return isNoisy;   }
    public void setIsNoisy(bool set) {   isNoisy = set;    }

    public bool getIsSafe() {   return isSafe;   }
    public void setIsSafe(bool set) {   isSafe = set;    }

    // Update is called once per frame
    void Update()
    {
        if (isHot)
            heatParticles.SetActive(true);
        else if (!isHot)
            heatParticles.SetActive(false);

        if (isNoisy)
            audioEmitter.SetActive(true);
        else if (!isNoisy)
            audioEmitter.SetActive(false);

        if (isSafe) {
            safeCube.SetActive(true);   //it's safe :)
            unsafeCube.SetActive(false);
        }
        else if (!isSafe) {
            unsafeCube.SetActive(true); // :(
            safeCube.SetActive(false);
        }
    }

}
