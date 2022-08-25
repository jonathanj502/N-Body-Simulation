using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cmVirtualCamera;
    float targetfov = 60;
    float fovmin = 10;
    float fovmax = 120;
    float mouseSensitivityX = 5.0f;
	float mouseSensitivityY = 5.0f;
	float rotY = 0.0f;

    private void zoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetfov -= 5;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetfov += 5;
        }
        if (Input.GetMouseButtonDown(2))
        {
            targetfov = 60;
        }
        targetfov = Mathf.Clamp(targetfov, fovmin, fovmax);
        float zoomspeed = 10;
        Mathf.Lerp(cmVirtualCamera.m_Lens.FieldOfView, targetfov, Time.deltaTime * zoomspeed); //smoothly (gradually) changes fov to target
        cmVirtualCamera.m_Lens.FieldOfView = targetfov;
    }
	private void rotate()
	{
        if (Input.GetMouseButton(1)) 
        {
            float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivityX;
            rotY += Input.GetAxis("Mouse Y") * mouseSensitivityY;
            rotY = Mathf.Clamp(rotY, -89.5f, 89.5f);
            transform.localEulerAngles = new Vector3(-rotY, rotX, 0.0f);
        }
	}
    void wasdpan()
    {
        Vector3 inputdir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputdir.z = +1;
        if (Input.GetKey(KeyCode.S)) inputdir.z = -1;
        if (Input.GetKey(KeyCode.D)) inputdir.x = +1;
        if (Input.GetKey(KeyCode.A)) inputdir.x = -1;
        Vector3 movedir = transform.forward * inputdir.z + transform.right * inputdir.x;
        float movespeed = 200.0f;
        transform.position += movedir * movespeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        wasdpan();
        rotate();
        zoom();
    }
}
