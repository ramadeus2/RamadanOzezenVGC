using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;

namespace WheelOfFortune.Utilities {

public class Helpers : MonoBehaviour
{
        public static int[] ShuffleArrayIndexes(int arrayLength)
        {
            int[] indexOrders = new int[arrayLength];
            for(int i = 0; i < indexOrders.Length; i++)
            {
                indexOrders[i] = i;
            }
            for(int i = 0; i < indexOrders.Length; i++)
            {
                int index = Random.Range(0, indexOrders.Length);
                 
                int temp = indexOrders[i];
                indexOrders[i] = indexOrders[index];
                indexOrders[index] = temp;
            }
            return indexOrders;
        }
        public static StageZone GetStageZone(int _currentStageNo)
        {
            StageZone stageZone = StageZone.DangerZone;
            if(_currentStageNo > 0 && _currentStageNo % GameManager.Instance.GameSettings.StageSuperZoneThreshold == 0)
            {
                stageZone = StageZone.SuperZone;
            } else if(_currentStageNo > 0 && _currentStageNo % GameManager.Instance.GameSettings.StageSafeZoneThreshold == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
        public static StageZone GetStageZone(int stageNo, GameSettings gameSettings)
        {
            StageZone stageZone = StageZone.DangerZone;
            if(stageNo != 0 && stageNo % gameSettings.StageSuperZoneThreshold == 0)
            {
                stageZone = StageZone.SuperZone;
            } else if(stageNo != 0 && stageNo % gameSettings.StageSafeZoneThreshold == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
    }
}
