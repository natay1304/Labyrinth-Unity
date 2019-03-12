using LabyrinthUnity.LocationGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pass : MonoBehaviour
{
    //[SerializeField]
    private Location _location;
    public Location Location { get => _location; set => _location = value; }

    //[SerializeField]
    private Vector2 _coordinates;
    public Vector2 Coordinates { get => _coordinates; set => _coordinates = value; }

    private void OnTriggerEnter(Collider other)
    {
        _location.currentPass = this;
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
