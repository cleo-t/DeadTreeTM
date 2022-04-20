using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float minPitch = -10;
    public float maxPitch = 80;
    float pitch = 0;
    float yaw = 0;
    Transform playerBody;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraRevolve.active)
        {
            return;
        }
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        // yaw
        playerBody.Rotate(Vector3.up * moveX);

        // pitch
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, this.minPitch, this.maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        //Debug.Log(yaw + ", " + pitch);
    }
}
