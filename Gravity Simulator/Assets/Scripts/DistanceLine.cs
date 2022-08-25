using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceLine : MonoBehaviour
{
    private LineRenderer lr;
    private Transform points1, points2;
    //[SerializeField] GameObject dragndropsphere;
    [SerializeField] RectTransform canvasrect;
    [SerializeField] RectTransform distancetextrect;
    public TextMeshProUGUI distancetext;
    void FindObjects() //draws line betweeen dragndropsphere and sphere closest to it
    {
        List <Vector3> distancebtwnobj = new List<Vector3>{};
        SphereEdit[] spheres = FindObjectsOfType<SphereEdit>();
        foreach (SphereEdit sphereEdit in spheres)
        {
            //sphereEdit.enabled = true;
            points2 = sphereEdit.GetComponent<Transform>();
            Vector3 distance = transform.position - points2.position;
            distancebtwnobj.Add(distance);
        } 
        float minval = distancebtwnobj[0].magnitude;
        int minvalindex = 0;
        for (int i = 1; i < distancebtwnobj.Count; i++)
        {
            if(distancebtwnobj[i].magnitude<minval)
            {
                minval = distancebtwnobj[i].magnitude;
                minvalindex = i;
            }
        }
        float AUdistance = distancebtwnobj[minvalindex].magnitude/100;
        distancetext.text = AUdistance.ToString() + " AU";
        points1 = gameObject.GetComponent<Transform>();
        points2 = spheres[minvalindex].GetComponent<Transform>();
        lr.SetPosition(0, points1.position);
        lr.SetPosition(1, points2.position);
    }
    // Start is called before the first frame update
    void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == true)//if placing new obj show distance
        {
            FindObjects();
            Vector2 anchoredpos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasrect, Input.mousePosition - new Vector3 (100, 0, 0), Camera.main, out anchoredpos); 
            distancetextrect.anchoredPosition = anchoredpos;
        }
    }
}