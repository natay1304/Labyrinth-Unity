using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGenerator
{
    public class MapIds
    {

        private int _passId = 0;
        public int PassId
        {
            get => _passId;
            set
            {
                List<int> compareList = new List<int>();
                compareList.Add(EnterId);
                compareList.Add(ExitId);
                compareList.AddRange(BlocksIds);
                if(IsUniqueIdsArray(new int[] {value}, compareList))
                {
                    _passId = value;
                }
                else
                {
                    Debug.Log("Pass Id is not setted. Value is not unique");
                }
            }
        }

        private int _enterId = 1;
        public int EnterId
        {
            get => _enterId;
            set
            {
                List<int> compareList = new List<int>();
                compareList.Add(PassId);
                compareList.Add(ExitId);
                compareList.AddRange(BlocksIds);
                if (IsUniqueIdsArray(new int[] { value }, compareList))
                {
                    _enterId = value;
                }
                else
                {
                    Debug.Log("Enter Id is not setted. Value is not unique");
                }
            }
        }

        private int _exitId = 2;
        public int ExitId
        {
            get => _exitId;
            set
            {
                List<int> compareList = new List<int>();
                compareList.Add(PassId);
                compareList.Add(EnterId);
                compareList.AddRange(BlocksIds);
                if (IsUniqueIdsArray(new int[] { value }, compareList))
                {
                    _exitId = value;
                }
                else
                {
                    Debug.Log("Exit Id is not setted. Value is not unique");
                }
            }
        }


        private readonly int _defaultBlockId = 3;
        private int[] _blocksIds;
        public int[] BlocksIds
        {
            get
            {
                if(_blocksIds.Length == 0)
                {
                    return _blocksIds = new int[] { _defaultBlockId};
                }
                return _blocksIds;
            }
            set
            {
                List<int> compareList = new List<int>();
                compareList.Add(PassId);
                compareList.Add(EnterId);
                compareList.Add(ExitId);
                if (value.Length > 0 && !IsUniqueIdsArray(value, compareList))
                {
                    _blocksIds = value;
                }
                else
                {
                    Debug.Log("Blocks Ids list is not setted. It is empty or exists defined IDs");
                }
            }
        }
        private bool IsUniqueIdsArray(int[] testIdsArray, List<int>compareIdsList)
        {
            for (int idNumber = 0; idNumber < testIdsArray.Length; idNumber++)
            {
                if (compareIdsList.Contains(testIdsArray[idNumber]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
