using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //sliders for positions of main 3 controller components
    public Transform playerCam, character, centerPoint;


    //movement variables
    private float moveFB, moveLR;
    public float moveSpeed = 2f;


    //zoom variables
    private float zoom;
    public float zoomSpeed = 2;

    public float zoomMin = -2f;
    public float zoomMax = -10f;

    public float rotationSpeed = 5f;

    //mouse camera controls
    private float mouseX, mouseY;
    public float mouseSensitivity = 10f;
    public float mouseYPosition = 1f;

    // Use this for initialization
    void Start () {
        
        zoom = -3;
        
    }

    // Update is called once per frame
    void Update () {

        //zoom controls
        zoom += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
            zoom = zoomMin;
       

        if (zoom < zoomMax)
            zoom = zoomMax;
 

        playerCam.transform.localPosition = new Vector3 (0,0,zoom);

        //Mouse Orbiting
        if (Input.GetAxis("Mouse X") != 0)

        {

            //right click moves camera
            mouseX -= Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");

        }

        mouseY = Mathf.Clamp(mouseY, -60, 60);
        //controll of right mouse click
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0); //Not Vector3, rotations always use quarternions

        //playermovement
        moveFB = Input.GetAxis ("Vertical") * moveSpeed;
        moveLR = Input.GetAxis ("Horizontal") * moveSpeed;

        Vector3 movement = new Vector3 (moveLR, 0, moveFB);

        character.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);
        centerPoint.position = new Vector3 (character.position.x, character.position.y + mouseYPosition, character.position.z);

        if (Input.GetAxis ("Vertical") > 0 | Input.GetAxis("Vertical") < 0 )
        {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);

            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);

        } 

    }
}
