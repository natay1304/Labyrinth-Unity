using System;
using System.Collections.Generic;
using UnityEngine;

namespace LabyrinthUnity.MapGenerator
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
                    throw new ArgumentException("Pass Id is not setted. Value is not unique ", "PassId");
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
                    throw new ArgumentException("EnterId is not setted. Value is not unique ", "EnterId");
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
                    throw new ArgumentException("ExitId is not setted. Value is not unique ", "ExitId");
                }
            }
        }


        private readonly int _defaultBlockId = 3;
        private int[] _blocksIds = new int[0];
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
                    throw new ArgumentException("BlocksIds are not setted. Some ids are not unique ", "BlocksIds");
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
