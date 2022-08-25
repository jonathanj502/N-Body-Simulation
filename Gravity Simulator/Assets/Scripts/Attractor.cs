using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Vector3 velocity;
    Rigidbody rb;
    float gconstant;
    float AU;
    float SolarMass;
    float TimeT;
    List<Vector3> othrdsopos;
    List<float> othrdsomass;
    List<Vector3> states;
    Vector3 origpos;
    Vector3 origvelo;
    //1 mass = 1 solar mass 100 distance = 1 au
    Vector3 accel(Vector3 rval)
    {
        Vector3 distance;
        Vector3 acceleration;
        Vector3 sumaccel = Vector3.zero;
        for (int i = 0; i <= othrdsopos.Count-1; i++)
        {
            distance = rval-(othrdsopos[i]);
            acceleration = ((-1 * gconstant * (othrdsomass[i])) / (Mathf.Pow(distance.magnitude,3)) * distance);
            sumaccel += acceleration;
        }
        return sumaccel;
    }
    List <Vector3> rungeKutta(float x0, Vector3 initialvelocity, Vector3 position, float h)
    {
 
        Vector3 k1v, k2v, k3v, k4v, k1r, k2r, k3r, k4r;
 
        Vector3 updatedvelocity, updatedposition;
           
        k1v = (accel(position));

        k1r = initialvelocity;
             
        k2v = (accel(position + k1r * h/2));

        k2r = initialvelocity + k1v * h/2;
                          
        k3v = (accel(position + k2r * h/2));

        k3r = initialvelocity + k2v * h/2;
                             
        k4v = accel(position + k3r * h);

        k4r = initialvelocity + k3v * h;

        updatedvelocity = initialvelocity + ((h/6) * (k1v + (2 * k2v) + (2 * k3v) + k4v));

        updatedposition = (position + ((h/6) * (k1r + (2 * k2r) + (2 * k3r) + k4r)))/AU*100;

        List<Vector3> states = new List<Vector3>{updatedvelocity, updatedposition}; 
 
        return states;
    }
    void Awake()
    {
        gconstant = 6.6743f * Mathf.Pow(10,-11);
        SolarMass = 1.98847f * Mathf.Pow(10,30); //mass of sun in kg
        AU = 149597870700f; //mean distance between earth and sun in m
        rb = gameObject.GetComponent<Rigidbody>();
        othrdsomass = new List<float>{};
        othrdsopos = new List<Vector3>{};
        origpos = rb.position;
        origvelo = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeT += Time.deltaTime;
        Attractor[] DSOs = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in DSOs)
        {
            if(attractor != this)
            {
                Rigidbody rbattractee = attractor.GetComponent<Rigidbody>();
                Vector3 positionconverted = (rbattractee.position)/100*AU;
                othrdsopos.Add(positionconverted);
                float dsomass = rbattractee.mass*SolarMass;
                othrdsomass.Add(dsomass);
            }
        }   
        Vector3 rval = rb.position/100*AU;
        states = rungeKutta(TimeT, velocity, rval, 86400); //timestep determines how fast the simulation runs 43200s = 1/2 a day revolution persecond 86400
        velocity = states[0];
        rb.position = states[1];
        othrdsomass = new List<float>{};
        othrdsopos = new List<Vector3>{};
    }
}