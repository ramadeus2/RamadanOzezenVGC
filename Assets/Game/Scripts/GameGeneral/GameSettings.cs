using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Reward;
using WheelOfFortune.Stage;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.General {

    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "WheelOfFortune/General/GameSettings")]
    public class GameSettings: ScriptableObject {
        [SerializeField] private int _stageSuperZoneThreshold = 30;
        public int StageSuperZoneThreshold => _stageSuperZoneThreshold;


        [SerializeField] private int _stageSafeZoneThreshold = 5;
        public int StageSafeZoneThreshold => _stageSafeZoneThreshold;


        [SerializeField] private int _stageRewardUnitAmount = 8;
        public int StageRewardUnitAmount => _stageRewardUnitAmount;

        [SerializeField] private int _stageRewardMultiplierSafeZone = 2;
        public int StageRewardMultiplierSafeZone => _stageRewardMultiplierSafeZone;

        [SerializeField] private int _stageRewardMultiplierSuperZone = 4;
        public int StageRewardMultiplierSuperZone => _stageRewardMultiplierSuperZone;
        [SerializeField] private List<RewardData> _availableSpecialRewards;
        public List<RewardData> AvailableSpecialRewards => _availableSpecialRewards;


        [SerializeField] private int _revivePriceMin = 10;
        public int RevivePriceMin => _revivePriceMin;
        [SerializeField] private int _revivePriceMax = 1000;
        public int RevivePriceMax => _revivePriceMax;
        [SerializeField] private int _revivePriceIncreasementMultiplier = 3;
        public int RevivePriceIncreasementMultiplier => _revivePriceIncreasementMultiplier;


        [SerializeField] private SpriteAtlas _normalRewardAtlas;
        public SpriteAtlas NormalRewardAtlas => _normalRewardAtlas;


        [SerializeField] private SpriteAtlas _specialRewardAtlas;
        public SpriteAtlas SpecialRewardAtlas => _specialRewardAtlas;



        [SerializeField] private SpriteAtlas _currencyAtlas;
        public SpriteAtlas CurrencyAtlas => _currencyAtlas;

        [SerializeField] private Sprite _bombIcon;
        public Sprite BombIcon => _bombIcon;
        [SerializeField] private Sprite _safeZoneRewardIcon;
        public Sprite SafeZoneRewardIcon => _safeZoneRewardIcon;
        [SerializeField] private CurrencySettings _currencySettings;
        public CurrencySettings CurrencySettings => _currencySettings;


        [SerializeField] private RewardPool _rewardPool;
        public RewardPool RewardPool => _rewardPool;


        [SerializeField] private StagePool _stagePool;
        public StagePool StagePool => _stagePool;


        [SerializeField] private int _automaticStageSystemStageCount = 60;
        public int AutomaticStageSystemStageCount => _automaticStageSystemStageCount;
        [SerializeField] private CurrencyUnit _reviveCurrency;
        public CurrencyUnit ReviveCurrency => _reviveCurrency;

        public bool TryGetZoneRewardData(int _currentStageNo, StageZone stageZone, out int zoneRewardStageNo, out RewardData specialReward)
        {
            int threshold = 0;
            switch(stageZone)
            {
                case StageZone.DangerZone:
                    break;
                case StageZone.SafeZone:
                    threshold = StageSafeZoneThreshold;
                    break;
                case StageZone.SuperZone:
                    threshold = StageSuperZoneThreshold;
                    break;
                default:
                    break;
            }

            int zoneRewardIndex = Mathf.RoundToInt(_currentStageNo / threshold);
            zoneRewardStageNo = (zoneRewardIndex + 1) * threshold;
            if(stageZone == StageZone.SuperZone)
            {
                if(AvailableSpecialRewards.Count > zoneRewardIndex)
                {
                    specialReward = AvailableSpecialRewards[zoneRewardIndex];
                    return true;
                }

            }
            specialReward = null;
            return false;
        }
        public int GetRevivePrice(int reviveTime)
        {
            int price = _revivePriceMin; 
            if(reviveTime > 0)
            {
                for(int i = 0; i < reviveTime; i++)
                {
                    price *= _revivePriceIncreasementMultiplier;
                }

            
            }
            if(price> _revivePriceMax)
            {
                price = _revivePriceMax;
            } 
            return price;

        }
        public Sprite GetRewardSprite(string name)
        {
            Sprite rewardIcon;

            if(GetRewardSprite(name, RewardType.Normal, out rewardIcon))
            {
                return rewardIcon;
            }
            else if(GetRewardSprite(name, RewardType.Special, out rewardIcon))
            {
                return rewardIcon;
            }
            else if(GetRewardSprite(name, RewardType.Currency, out rewardIcon))
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

        public CurrencyUnit GetCurrencyUnit(string currencyData)
        {
            for(int i = 0; i < CurrencySettings.AvailableCurrencies.Count; i++)
            {
                if(CurrencySettings.AvailableCurrencies[i].CurrencyRewardData.RewardId == currencyData)
                {
                    return CurrencySettings.AvailableCurrencies[i];
                }
            }

            return null;
        }
    }
}
