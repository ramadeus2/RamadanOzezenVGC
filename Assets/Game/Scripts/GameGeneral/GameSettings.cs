using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Reward;
using WheelOfFortune.Stage;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.General {

    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "WheelOfFortune/General/GameSettings")]
    public class GameSettings: ScriptableObject {
        [SerializeField] private int _stageSuperZoneMultiplier = 30;
        public int StageSuperZoneMultiplier => _stageSuperZoneMultiplier;


        [SerializeField] private int _stageSafeZoneMultiplier = 5;
        public int StageSafeZoneMultiplier => _stageSafeZoneMultiplier;


        [SerializeField] private int _stageRewardAmount = 8;
        public int StageRewardAmount => _stageRewardAmount;


        [SerializeField] private SpriteAtlas _normalRewardAtlas;
        public SpriteAtlas NormalRewardAtlas =>_normalRewardAtlas;


        [SerializeField] private SpriteAtlas _specialRewardAtlas;
        public SpriteAtlas SpecialRewardAtlas => _specialRewardAtlas;



        [SerializeField] private SpriteAtlas _currencyAtlas;
        public SpriteAtlas CurrencyAtlas=>_currencyAtlas;

        [SerializeField] private Sprite _bombIcon;
        public Sprite BombIcon => _bombIcon;

        [SerializeField] private CurrencySettings _currencySettings;
        public CurrencySettings CurrencySettings=>_currencySettings;


        [SerializeField] private RewardPool _rewardPool;
        public RewardPool RewardPool => _rewardPool;


        [SerializeField] private StagePool _stagePool;
        public StagePool StagePool => _stagePool;

        public Sprite GetRewardSprite(string name)
        {
            Sprite rewardIcon;

            if(GetRewardSprite(name, RewardType.Normal, out rewardIcon))
            {
                return rewardIcon;
            } else if(GetRewardSprite(name, RewardType.Special, out rewardIcon))
            {
                return rewardIcon;
            } else if(GetRewardSprite(name, RewardType.Currency, out rewardIcon))
            {
                return rewardIcon;
            }

            return null;
        }
        public bool GetRewardSprite(string name, RewardType rewardType, out Sprite rewardIcon)
        {
            SpriteAtlas atlas = null; 
            switch(rewardType)
            {
                case RewardType.Normal:
                    atlas = _normalRewardAtlas;
                    break;
                case RewardType.Currency:
                    atlas = _currencyAtlas;
                    break;
                case RewardType.Special:
                    atlas = _specialRewardAtlas;
                    break;
                case RewardType.Bomb:
                    rewardIcon = _bombIcon;
                    return true; ;
                default:
                    break;
            }
            rewardIcon = atlas.GetSprite(name);
            return rewardIcon;
        }

    }
}
