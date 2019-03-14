using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LabyrinthUnity.MapGeneratorNS
{
    public class MapIds
    {
        public int PassId { get; private set; }
        public int ExitId { get; private set; } 
        public int EnterId { get; private set; }
        public IEnumerable<int> WallsIds { get; private set; }
        private readonly int defaultWallsId = 3;

        public MapIds(IEnumerable<int> wallsIds = null, int passId = 0, int enterId = 1, int exitId = 2)
        {
            if(wallsIds == null)
                wallsIds = new int[] { defaultWallsId };

            CheckIds(wallsIds.Concat(new int[] { PassId, EnterId, ExitId }));

            PassId = passId;
            ExitId = exitId;
            EnterId = enterId;
            WallsIds = wallsIds;
        }
        private void CheckIds(IEnumerable<int> allIds)
        {
            if (allIds.Distinct().Count() == allIds.Count())
                throw new InvalidOperationException("One of Ids in MapIds is not unique");
        }
    }
}
