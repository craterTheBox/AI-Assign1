/**************************************************
*   AI for Games - Assignment 1
*
*   Carter Menary, 100700587
*   2022-02-06
**************************************************/

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

    [SerializeField] KeyCode openDoorKey = KeyCode.Mouse0;
    
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
    private CharacterController charController;

    float InvertVertical = 1.0f;
    float InvertHorizontal = 1.0f;
    Vector3 rotation = new Vector3(0, 0, 0);
    bool cameraPause = false;

    // Start is called before the first frame update
    void Awake() {
        charController = GetComponent<CharacterController>();
        
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
        
        if (Input.GetKeyDown(KeyCode.Tab)) {
            cameraPause = !cameraPause;
            Cursor.visible = !Cursor.visible;
            if (cameraPause)
                Cursor.lockState = CursorLockMode.None;
            else 
                Cursor.lockState = CursorLockMode.Locked;
        }

        MouseInput();
        Movement();
        
        if (Input.GetKeyDown(openDoorKey))
            OpenDoor();
    }

    void MouseInput() {
        if (cameraPause)
            return;

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
        if (cameraPause)
            return;

        float horiInput = 0.0f;
        float vertInput = 0.0f;
        
        if (Input.GetKey(moveForwardKey)) vertInput = 1.0f;
        else if (Input.GetKey(moveBackwardKey)) vertInput = -1.0f;
        if (Input.GetKey(moveRightKey)) horiInput = 1.0f;
        else if (Input.GetKey(moveLeftKey)) horiInput = -1.0f;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horiInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        if (vertInput != 0 || horiInput != 0)
            charController.Move(Vector3.down * charController.height / 2 * Time.deltaTime);
    }

    void OpenDoor() {
        //oops
    }

}
