using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem: MonoBehaviour {
        [SerializeField] protected int _stageCount;
        protected GameSettings _gameSettings;
        protected StageData _currentStage;
        protected int _currentStageNo = 0;

        protected virtual void OnEnable()
        {

            InitializeNextStage();
            UIManager.Instance.InitializeStageBar(_stageCount);

        }

        protected virtual void OnDisable()
        {
            _currentStage = null;
            _currentStageNo = 0;
        }




        public virtual void InitializeNextStage()
        {
            if(!_gameSettings)
            {
                _gameSettings = GameManager.Instance.GameSettings;
            }
            if(_currentStageNo >= _stageCount)
            {
                return;
            }
            _currentStageNo++;
            StageZone stageZone = Helpers.GetStageZone(_currentStageNo);
            SetCurrentStage();

            UpdateSpecialRewardIcon(stageZone);



            List<RewardData> rewardDatas = GetRewardList(stageZone);
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone);


        }
        private void UpdateSpecialRewardIcon(StageZone stageZone)
        {
            if(_gameSettings.TryGetSpecialRewardData(_currentStageNo, out RewardData specialReward))
            { 
                Sprite specialRewardIcon = _gameSettings.GetRewardSprite(specialReward.SpriteName); 
                UIManager.Instance.InitializeSpecialRewardIcon(specialRewardIcon);
            }
            if(stageZone == StageZone.SuperZone)
            {
                UIManager.Instance.ActivateSpecialRewardPopUp();
            }
        }
        protected abstract void SetCurrentStage();
        protected abstract List<RewardData> GetRewardList(StageZone stageZone);
    }

}
