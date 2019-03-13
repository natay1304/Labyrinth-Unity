using System.Collections.Generic;
using UnityEngine;

namespace LabyrinthUnity.LocationGenerator
{
    [CreateAssetMenu(menuName = "LocationLib")]
    public class LocationLib : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> _blocksOrderByIds;
        public List<GameObject> Blocks { get => _blocksOrderByIds;}
    }
}
