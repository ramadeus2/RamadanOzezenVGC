using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;

namespace WheelOfFortune.CurrencySystem {
    [CreateAssetMenu(fileName = "NewCurrency", menuName = "WheelOfFortune/CurrencySystem/CurrencyData")]
    public class CurrencyUnit: ScriptableObject {
       
        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;


        [SerializeField] private RewardData _currencyRewardData;
        public RewardData CurrencyRewardData => _currencyRewardData ;

        [SerializeField] private int _startAmount;
        public int StartAmount => _startAmount;

        
    }

}
