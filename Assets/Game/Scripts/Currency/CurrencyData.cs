using System;
using UnityEngine;
namespace WheelOfFortune.CurrencySystem {
    [Serializable]
public class CurrencyData 
{
        public CurrencyData(CurrencyUnit currency)
        {  
            _currency = currency;
            _currentAmount = _currency.StartAmount; 
        }
        public CurrencyData(CurrencyUnit currency,int amount)
        {
            _currency = currency;
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
        private CurrencyUnit _currency;
        public CurrencyUnit Currency => _currency;
        private int _currentAmount;
        public int CurrentAmount => _currentAmount;
        

}
}
