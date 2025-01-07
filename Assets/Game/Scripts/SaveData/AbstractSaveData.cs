using System;
using UnityEngine;
namespace WheelOfFortune.SaveManagement {
    [Serializable] 
    public abstract class AbstractSaveData 
{
        public AbstractSaveData(string dataId )
        {
            _dataId = dataId; 
        }
        public AbstractSaveData(string dataId,  int amount)
        { 
            _dataId = dataId;
            _currentAmount = amount;
        }
        public int InsertAmount(int amount)
        {
            _currentAmount += amount;
            return CurrentAmount;
        }
        public int ExtractAmount(int amount)
        {
            _currentAmount -= amount;
            return CurrentAmount;
        }
     
        protected int _currentAmount;
        public int CurrentAmount => _currentAmount;
        protected string _dataId;
        public string DataId => _dataId;

    }
}
