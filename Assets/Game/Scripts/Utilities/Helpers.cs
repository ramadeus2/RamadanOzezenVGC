using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
