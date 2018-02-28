using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    public Camera cam;

    public LayerMask collectibleLayer;

    public float offset;         //Private variable to store the offset distance between the player and camera
    public Vector3 mouseRotation = new Vector3(45, 90, 0);

    public float rotationSpeed;
    public bool keyboardOnly;
    public float xAngle;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //float rotX = Input.GetAxis("CameraX");
        //float rotY = Input.GetAxis("CameraY");
        //Debug.Log(rotX.ToString() + " " + rotY.ToString());
        //transform.Rotate(rotY, rotX, 0);
        //transform.position = player.transform.position + offset;

        if (!keyboardOnly)
        {
            mouseRotation.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseRotation.y += Input.GetAxis("Mouse X") * rotationSpeed;
        }
        else if (keyboardOnly)
        {
            mouseRotation.x = xAngle;
            if (!player.GetComponent<PlayerController>().onLedge)
            {
                mouseRotation.y += Input.GetAxis("Horizontal") * rotationSpeed;
            }
            else if (player.GetComponent<PlayerController>().onLedge)
            {
                player.GetComponent<Rigidbody>().AddForce(Vector3.right * Input.GetAxis("Horizontal") * player.GetComponent<PlayerController>().movementSpeed);
                //mouseRotation = new Vector3(transform.position + player.hit.point.normalized);
            }
        }
        
        if (mouseRotation.x > 44)
        {
            mouseRotation.x = 44;
        }
        if (mouseRotation.x < -44)
        {
            mouseRotation.x = -44;
        }

        this.transform.position = player.transform.position;
        this.transform.rotation = Quaternion.Euler(mouseRotation.x * 1.7f, mouseRotation.y * 1.7f, 0);
        this.transform.position -= transform.forward * offset;

        RaycastHit hitInfo;

        Debug.DrawLine(player.transform.position, transform.position, Color.green);

        Physics.Linecast(player.transform.position, transform.position, out hitInfo, ~collectibleLayer);
        if (hitInfo.transform != null)
        {
            Debug.Log("hit");
            transform.position = player.transform.position + (transform.forward * -(hitInfo.distance - cam.nearClipPlane));
            Debug.DrawLine(player.transform.position, transform.position, Color.red);
        }

        //this.transform.position -= transform.forward * offset;




    }
}