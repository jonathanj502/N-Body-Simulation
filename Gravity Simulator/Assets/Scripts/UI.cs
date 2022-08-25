using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class UI : MonoBehaviour
{
    public Collider objcollider;
    float floatmassinp, floatxvelo, floatyvelo, floatzvelo, floatxpos, floatypos, floatzpos;
    string massinp, xvelo, yvelo, zvelo, xpos, ypos, zpos;
    [SerializeField] GameObject dropdownmenu, dragndrop, dragndropSphere, uiproperties;
    [SerializeField] TMP_InputField uimass, uixvelo, uiyvelo, uizvelo, uixpos, uiypos, uizpos;
    [SerializeField] CinemachineVirtualCamera cam;
    GameObject[] trailDSOs;
    public void deactivatevalchange()
    {
        trailDSOs = GameObject.FindGameObjectsWithTag("TrailPredict");
        foreach (GameObject attractor in trailDSOs)
        {
            attractor.SetActive(false);
        }
    }
    public void activatevalchange()
    {
        foreach (GameObject attractor in trailDSOs)
        {
            attractor.SetActive(true);
        }
    }
    public void changemass()
    {
        massinp = uimass.text;
        float result;
        if (float.TryParse(massinp, out result))
        {
            floatmassinp = result;
        }
        objcollider.GetComponent<Rigidbody>().mass = floatmassinp;
    }
    public void changexvelo()
    {
        xvelo = uixvelo.text;
        float result;
        if (float.TryParse(xvelo, out result))
        {
            floatxvelo = result;
        }
        //floatxvelo = float.Parse(xvelo, CultureInfo.InvariantCulture);
        objcollider.GetComponent<Attractor>().velocity.x = floatxvelo;
    }
    public void changeyvelo()
    {
        yvelo = uiyvelo.text;
        float result;
        if (float.TryParse(yvelo, out result))
        {
            floatyvelo = result;
        }
        objcollider.GetComponent<Attractor>().velocity.y = floatyvelo;
    }
    public void changezvelo()
    {
        zvelo = uizvelo.text;
        float result;
        if (float.TryParse(zvelo, out result))
        {
            floatzvelo = result;
        }
        //floatzvelo = float.Parse(zvelo, CultureInfo.InvariantCulture);
        objcollider.GetComponent<Attractor>().velocity.z = floatzvelo;
    }
    public void changexpos()
    {
        xpos = uixpos.text;
        float result;
        if (float.TryParse(xpos, out result))
        {
            floatxpos = result;
        }
        //floatxpos = float.Parse(xpos, CultureInfo.InvariantCulture);
        Vector3 newpos = new Vector3 (floatxpos, objcollider.GetComponent<SphereEdit>().transform.position.y, objcollider.GetComponent<SphereEdit>().transform.position.z);
        objcollider.GetComponent<Transform>().position = newpos;
    }
    public void changeypos()
    {
        ypos = uiypos.text;
        float result;
        if (float.TryParse(ypos, out result))
        {
            floatypos = result;
        }
        //floatypos = float.Parse(ypos, CultureInfo.InvariantCulture);
        Vector3 newpos = new Vector3 (objcollider.GetComponent<SphereEdit>().transform.position.x, floatypos, objcollider.GetComponent<SphereEdit>().transform.position.z);
        objcollider.GetComponent<Transform>().position = newpos;
    }
    public void changezpos()
    {
        zpos = uizpos.text;
        float result;
        if (float.TryParse(zpos, out result))
        {
            floatzpos = result;
        }
        //floatzpos = float.Parse(zpos, CultureInfo.InvariantCulture);
        Vector3 newpos = new Vector3 (objcollider.GetComponent<SphereEdit>().transform.position.x, objcollider.GetComponent<SphereEdit>().transform.position.z, floatzpos);
        objcollider.GetComponent<Transform>().position = newpos;
    }
    public void valclear()
    {
        uimass.text = "";
        uixvelo.text = "";
        uiyvelo.text = "";
        uizvelo.text = "";
        uixpos.text = "";
        uiypos.text = "";
        uizpos.text = "";
    }

    public void gameobjplaceholder(float objmass, Vector3 objvelo, Vector3 objpos)
    {
        TextMeshProUGUI uimassplaceholder = (TextMeshProUGUI)uimass.placeholder;
        TextMeshProUGUI uixveloplaceholder = (TextMeshProUGUI)uixvelo.placeholder;
        TextMeshProUGUI uiyveloplaceholder = (TextMeshProUGUI)uiyvelo.placeholder;
        TextMeshProUGUI uizveloplaceholder = (TextMeshProUGUI)uizvelo.placeholder;
        TextMeshProUGUI uixposplaceholder = (TextMeshProUGUI)uixpos.placeholder;
        TextMeshProUGUI uiyposplaceholder = (TextMeshProUGUI)uiypos.placeholder;
        TextMeshProUGUI uizposplaceholder = (TextMeshProUGUI)uizpos.placeholder;
        uimassplaceholder.text = (objmass.ToString());
        uixveloplaceholder.text = (objvelo.x.ToString());
        uiyveloplaceholder.text = (objvelo.y.ToString());
        uizveloplaceholder.text = (objvelo.z.ToString());
        uixposplaceholder.text = (objpos.x.ToString());
        uiyposplaceholder.text = (objpos.y.ToString());
        uizposplaceholder.text = (objpos.z.ToString());
    }
    public void uipropertiesactive()
    {
        uiproperties.SetActive(true);
    }
    public void uipropertiesdeactivate()
    {
        uiproperties.SetActive(false);
    }
    public void dragndropsphere()
    {
        dragndropSphere.SetActive(true);
    }
    public void disableedit()
    {
        dragndrop.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<NbodySandbox>().enabled = false;
        Attractor[] DSOs = FindObjectsOfType<Attractor>();
        deactivatevalchange();
        foreach (Attractor attractor in DSOs)
        {
            attractor.GetComponent<Attractor>().enabled = true;
        }
    }
    public void enableedit()
    {
        dragndrop.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<NbodySandbox>().enabled = true;
        Attractor[] DSOs = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in DSOs)
        {
            attractor.GetComponent<Attractor>().enabled = false;
        }
    }
    public void dropdown()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dropdownmenu.gameObject.SetActive(true);
        }
    }
    public void loadsandbox()
    {
        SceneManager.LoadScene("Sandbox", LoadSceneMode.Single);
        dropdownmenu.gameObject.SetActive(false);
    }
    public void loadsolarsystem()
    {
        SceneManager.LoadScene("SolarSystem", LoadSceneMode.Single);
        dropdownmenu.gameObject.SetActive(false);
    }
    public void clearworldspace() //gets rid of all active dsos
    {
        GameObject[] currentDSOs = GameObject.FindGameObjectsWithTag("CurrentDSOs");
        foreach (GameObject currentdso in currentDSOs)
        {
            Destroy(currentdso, 0);
        }
        cam.transform.position = new Vector3(0, 0, -100);
        cam.transform.eulerAngles = new Vector3 (0, 0, 0);
    }
    // public void loadfigureeight()
    // {
    //     Debug.Log("solarsystem");
    //     SceneManager.LoadScene("SolarSystem", LoadSceneMode.Single);
    //     dropdownmenu.gameObject.SetActive(false);
    // }
    // Start is called before the first frame update
    void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        dropdown();
    }
}