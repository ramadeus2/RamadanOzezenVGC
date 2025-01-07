using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.CurrencySystem {
    [CreateAssetMenu(fileName = "NewCurrency", menuName = "WheelOfFortune/CurrencySystem/CurrencyData")]
    public class CurrencyUnit: ScriptableObject {
        [SerializeField] private string _currencyId;
        public string CurrencyId => _currencyId;
        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;
        [SerializeField] private int _startAmount;
        public int StartAmount => _startAmount;

        public void InitializeId()
        {
            if(string.IsNullOrEmpty(_currencyId))
            {
                _currencyId  = System.Guid.NewGuid().ToString();
            }
        }
    }

}
