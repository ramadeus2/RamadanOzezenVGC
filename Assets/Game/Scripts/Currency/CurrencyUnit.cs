using UnityEngine;
using WheelOfFortune.Reward;

namespace WheelOfFortune.CurrencySystem {
    [CreateAssetMenu(fileName = "NewCurrency", menuName = "WheelOfFortune/CurrencySystem/CurrencyData")]
    public class CurrencyUnit: ScriptableObject {
        [SerializeField] private Sprite _icon;

        // since the reward data (currency reward datas in this scenario) sprites are being called from sprite atlas by name, there is no actual sprite obj. for not calling spriteAtlas.GetSprite(name) method, we storage the icon here to deliver easily.

        public Sprite Icon => _icon;


        [SerializeField] private RewardData _currencyRewardData;
        public RewardData CurrencyRewardData => _currencyRewardData;

        [SerializeField] private int _startAmount;
        public int StartAmount => _startAmount;


    }

}
