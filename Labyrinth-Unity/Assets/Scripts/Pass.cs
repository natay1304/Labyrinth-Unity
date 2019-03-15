using LabyrinthUnity.LocationGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pass : MonoBehaviour
{
    public Location Location;
    
    public Vector2Int Coordinates;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
            Location.CurrentPass = this;
    }
}
