using LabyrinthUnity.LocationGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pass : MonoBehaviour
{
    //[SerializeField]
    public Location Location;
    
    public Vector2 Coordinates;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.GetComponent<Player>());
        if(other.GetComponent<Player>() != null)
        {
            Location.currentPass = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
