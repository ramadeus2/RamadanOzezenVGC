using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.CurrencySystem {

    [CreateAssetMenu(fileName = "NewCurrencySettings", menuName = "WheelOfFortune/CurrencySystem/CurrencySettings")]
public class CurrencySettings : ScriptableObject {
        [SerializeField] private List<CurrencyUnit> _availableCurrencies;
        public List<CurrencyUnit> AvailableCurrencies => _availableCurrencies;
    }
}
