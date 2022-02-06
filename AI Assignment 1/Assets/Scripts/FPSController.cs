using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Keybinds")]
    [Tooltip("Here for easy access to change them, but settings should be available in game eventually.")]
    //Movement
    [SerializeField] KeyCode moveForwardKey = KeyCode.W;
    [SerializeField] KeyCode moveBackwardKey = KeyCode.S;
    [SerializeField] KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] KeyCode moveRightKey = KeyCode.D;

    [SerializeField] KeyCode touchDoorKey = KeyCode.Mouse0;
    [SerializeField] KeyCode openDoorKey = KeyCode.E;
    
    [Header("Camera Settings")]
    [Tooltip("Settings to tweak the camera inputs and FOV.")]
    //Camera Options 
    [SerializeField] bool InvertLookHori = false;
    [SerializeField] bool InvertLookVert = false;
    [SerializeField] bool RawMouseInput = true;
    [SerializeField] float lookSensitivity = 5.0f;
    [SerializeField] float cameraFOV = 95.0f;

    [Header("Under the Hood Settings")]
    [Tooltip("This is the stuff that will just need to be tweaked to get right.")]
    //Player Settings
    [SerializeField] float movementSpeed = 8.0f;

    //Game Objects
    [SerializeField] Camera playerView;

    float InvertVertical = 1.0f;
    float InvertHorizontal = 1.0f;
    Vector3 rotation = new Vector3(0, 0, 0);
    bool isPaused = false;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
        if (InvertLookVert) {
            InvertVertical = -1.0f;
        }
        if (InvertLookHori) {
            InvertHorizontal = -1.0f;
        }

        playerView.fieldOfView = cameraFOV;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        MouseInput();
        Movement();
    }

    void MouseInput() {
        if (rotation.x <= -15)
            rotation.x = -15;
        if (rotation.x >= 15)
            rotation.x = 15;

        if (RawMouseInput) {
            rotation.y += InvertHorizontal * Input.GetAxis("Mouse X");
            rotation.x += InvertVertical * -Input.GetAxis("Mouse Y");

            //Rotates just the camera vertically
            Vector3 tempRotateVert = new Vector3(rotation.x, rotation.y, rotation.z);
            playerView.transform.eulerAngles = (Vector3)tempRotateVert * lookSensitivity; 

            //Rotates the camera and player body horizontally
            Vector3 tempRotateHori = new Vector3(0.0f, rotation.y, rotation.z);
            transform.eulerAngles = (Vector3)tempRotateHori * lookSensitivity; 

            //playerView.transform.eulerAngles = (Vector3)rotation * lookSensitivity; //this causes the entire body to move when looking vertically
        }
    }

    void Movement() { //This whole script is borked. Change all this
        if (isPaused)
            return;

        if (Input.GetKey(moveForwardKey))
            transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * movementSpeed * Time.deltaTime);
        if (Input.GetKey(moveBackwardKey))
            transform.Translate(new Vector3(0.0f, 0.0f, -1.0f) * movementSpeed * Time.deltaTime);
        if (Input.GetKey(moveLeftKey))
            transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * movementSpeed * Time.deltaTime);
        if (Input.GetKey(moveRightKey))
            transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * movementSpeed * Time.deltaTime);
    }
}
