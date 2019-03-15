using LabyrinthUnity.LocationGenerator;
using UnityEngine;

public class ExitLocation : MonoBehaviour
{
    public Pass Pass; 

    private void OnTriggerEnter(Collider other)
    { 
        if(other.GetComponent<Player>() != null)
            Pass.Location.Regenerate(Pass.Location.CurrentPass.Coordinates);
    }
}
