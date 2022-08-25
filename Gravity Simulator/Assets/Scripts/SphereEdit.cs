using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SphereEdit : MonoBehaviour
{
    UI canvasui;
    Rigidbody rb;
    float mass;
    Vector3 position, velocity;
    public Collider objhit;
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        canvasui = GameObject.Find("Canvas").GetComponent<UI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.collider == true)//displays values of obj cliked on 
            {
                objhit = hit.collider;
                objhit.GetComponent<Outline>().enabled = true;
                canvasui.objcollider = objhit;
                canvasui.uipropertiesactive();
                canvasui.gameobjplaceholder(hit.collider.GetComponent<Rigidbody>().mass, objhit.GetComponent<Attractor>().velocity, objhit.transform.position);
            }
            if (hit.collider == false && IsMouseOverUI() == false)
            {
                gameObject.GetComponent<Outline>().enabled = false;
                objhit = null;
                canvasui.uipropertiesdeactivate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && GetComponent<Outline>().enabled == true && IsMouseOverUI() == false) 
        {
            Destroy(gameObject, 0);
        }
    }
}