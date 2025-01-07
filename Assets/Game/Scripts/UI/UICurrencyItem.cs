using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.SaveManagement;

namespace WheelOfFortune.UserInterface {

    public class UICurrencyItem: MonoBehaviour {
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TMP_Text _amountText;

        public void UpdateItem(CurrencySaveData currencyData)
        {
            _currencyIcon.sprite = currencyData.Currency.Icon; 
            _amountText.text = currencyData.CurrentAmount.ToString();
        }
    }
}
