using System.Collections.Generic; 
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem: MonoBehaviour {

        #region FIELDS
        protected GameSettings _gameSettings;
        protected StageData _currentStage;
        protected UIManager _uiManager;
        protected int _currentStageNo = 0;
        protected int _stageCount;

        private UIRewardPanel _uiRewardPanel;
        #endregion
        #region INITIALIZATION

        [Inject]
        private void Constructor(UIRewardPanel uiRewardPanel, UIManager uIManager, GameSettings gameSettings)
        {
            _uiRewardPanel = uiRewardPanel;
            _uiManager = uIManager;
            _gameSettings = gameSettings;
        }
        protected virtual void OnEnable()
        { 
            // stage system can be changed multiple times (from auto to manual or reverse or just close and open again) so we need to reset the settings on every onenable time
            _currentStageNo = 0; // stage is being set to 0 but stage numbers starts from 1.
            SetStageCount();
            InitializeNextStage();// so we set the next stage right after  doing this also helps the stage bar to lerp  
            _uiManager.InitializeStageBar(_stageCount); 
            _uiManager.ResetReviveTime();

        }
        #endregion
        #region BEHAVIOUR
        public virtual bool InitializeNextStage()
        {
            if(_currentStageNo >= _stageCount)
            {
                return false;
            }
            _currentStageNo++;
            StageZone stageZone = Helpers.GetStageZone(_currentStageNo, _gameSettings);

            //before setting the next stage as "currentStage" we check upcoming stage is super zone so we can give the reward to the player.
            if(stageZone == StageZone.SuperZone)
            {
                CollectSpecialReward();
            }
            // now we can set as currentStage and initialize the rest
            SetCurrentStage();


            // right top upcoming reward visuals
            UpdateZoneRewardIcon(StageZone.SuperZone);
            UpdateZoneRewardIcon(StageZone.SafeZone);



            List<RewardData> rewardDatas = GetRewardList(stageZone);
            RunStage(rewardDatas, _currentStageNo, stageZone); //after all the initializations, actually run the stage. like updating the spin wheel rewards

            return true;
        }
        /// <summary>
        /// it tries to get the current super zone reward. we call this method before setting "current stage" so we wont get the "next upcoming" reward
        /// </summary>
        private void CollectSpecialReward()
        {
            _gameSettings.TryGetZoneRewardData(_currentStageNo, StageZone.SuperZone, out int zoneTargetStageNo, out RewardData specialReward);
            Sprite zoneRewardIcon = _gameSettings.GetRewardSprite(specialReward.SpriteName);
            RewardUnit rewardUnit = new RewardUnit(specialReward, zoneRewardIcon, specialReward.MinAmount);
            _uiRewardPanel.InitializeReward(rewardUnit);
            _uiManager.ActivateSpecialRewardPopUp();
        }
        private void UpdateZoneRewardIcon(StageZone targetZone)
        {

            Sprite zoneRewardIcon;
            if(_gameSettings.TryGetZoneRewardData(_currentStageNo, targetZone, out int zoneTargetStageNo, out RewardData specialReward))
            {
                zoneRewardIcon = _gameSettings.GetRewardSprite(specialReward.SpriteName);
            }
            else
            {
                zoneRewardIcon = _gameSettings.SafeZoneRewardIcon;
            }
            _uiManager.InitializeZoneRewardIcon(targetZone, zoneTargetStageNo, zoneRewardIcon);


        }
        protected abstract List<RewardData> GetRewardList(StageZone stageZone);
        protected abstract void RunStage(List<RewardData> rewardDatas, int stageNo, StageZone stageZone);
        protected abstract void SetStageCount();

        protected abstract void SetCurrentStage();
        #endregion

    }

}
