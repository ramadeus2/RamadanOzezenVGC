using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.CurrencySystem {
    [CreateAssetMenu(fileName = "NewCurrency", menuName = "WheelOfFortune/CurrencySystem/CurrencyData")]
    public class CurrencyUnit : ScriptableObject { 
        [SerializeField] private string _currencyName; 
        [SerializeField] public string CurrencyName=>_currencyName;
        [SerializeField] private Sprite _icon;
        [SerializeField] public Sprite Icon=>_icon;
        [SerializeField] private int _startAmount;
        [SerializeField] public int StartAmount=> _startAmount;
    }

}
