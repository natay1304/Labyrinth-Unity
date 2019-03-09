using System.Collections.Generic;
using UnityEngine;

namespace LabyrinthUnity.LocationGenerator
{
    [CreateAssetMenu(menuName = "Location")]
    public class LocationLib : ScriptableObject
    {
        public List<GameObject> Walls;
        public GameObject Pass;
        public GameObject Enter;
        public GameObject Exit;
    }
}
