using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NbodySandbox : MonoBehaviour
{
    [SerializeField] GameObject nbodyduplicate;
    [SerializeField] Camera cam;
    [SerializeField] GameObject dragndropSphere;
    private Vector3 worldpos;
    Plane planexy;
    bool button;
    // Start is called before the first frame update
    public Vector3 xyzcoord()
    {
        Vector3 xyzcoordinates = Vector3.zero;
        planexy = new Plane(Vector3.forward, new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z+100));
        //Debug.Log(planexy);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float distancetoplanexy;
        if (planexy.Raycast(ray, out distancetoplanexy))
        {
            xyzcoordinates = ray.GetPoint(distancetoplanexy); //use this to get y coord of mouse
        }        
        return xyzcoordinates;
    }
    void Update()
    {
        Vector3 xyzmouseposition = xyzcoord();
        dragndropSphere.transform.position = xyzmouseposition;
        if (Input.GetMouseButtonDown(0) && dragndropSphere.activeInHierarchy == true)
        {
            worldpos = xyzmouseposition;
            Instantiate(nbodyduplicate, worldpos, Quaternion.identity); 
            dragndropSphere.GetComponent<DistanceLine>().distancetext.text = null;
            GameObject.Find("Canvas").GetComponent<UI>().activatevalchange();
            dragndropSphere.SetActive(false);  
        }
    }    
}