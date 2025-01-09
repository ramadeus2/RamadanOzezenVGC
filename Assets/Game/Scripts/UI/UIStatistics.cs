using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.SaveManagement;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune {

    public class UIStatistics: MonoBehaviour {
        #region  FIELDS

        [Header("References")]
        [SerializeField] private UIRewardContent _rewardContentPrefab;
        [SerializeField] private GameObject _menuPanel;

        [Header("Normal Reward Info")]
        [SerializeField] private Transform _normalRewardContentHolder;

        [Header("Special Reward Info")]
        [SerializeField] private Transform _specialRewardContentHolder;

        private GameSettings _gameSettings;
        private Button _closeButton;
        #endregion
        #region INITIALIZATION

        [Inject]
        private void Constructor(CloseButton closeButton, GameSettings gameSettings)
        {
            _closeButton = closeButton.Button;
            _gameSettings = gameSettings;
        }
        private void OnEnable()
        {
            if(!_closeButton)
            {
                gameObject.SetActive(false);
                return;
            }
            _closeButton.onClick.AddListener(CloseThisPanel);
            _menuPanel.SetActive(false);
            InitializeCollectedItems(_normalRewardContentHolder, RewardType.Normal);
            InitializeCollectedItems(_specialRewardContentHolder, RewardType.Special);
        }


        private void OnDisable()
        {
            if(!_closeButton)
            {
                gameObject.SetActive(false);
                return;
            }
            _closeButton.onClick.RemoveListener(CloseThisPanel);
            _menuPanel.SetActive(true);

        }
        #endregion
        #region BEHAVIOUR
        /// <summary>
        /// seperates the special and normal rewards and adds to the right table
        /// </summary>
        private void InitializeCollectedItems(Transform holder, RewardType rewardType)
        {
            for(int i = 0; i < holder.childCount; i++)
            {
                Destroy(holder.GetChild(i).gameObject);
            }

            // get the spesified type of datas. like "normal rewards".
            string key = Consts.SAVE_INFO_NAME_NORMAL_REWARD;
            if(rewardType == RewardType.Special)
            {
                key = Consts.SAVE_INFO_NAME_SPECIAL;
            }


            List<DataSaveInfo> rewardSaveDatas = SaveSystem.LoadDatas(key, rewardType);
         
            for(int i = 0; i < rewardSaveDatas.Count; i++)
            {
                UIRewardContent rewardContent = Instantiate(_rewardContentPrefab, holder);
                RewardData rewardData = _gameSettings.RewardPool.GetRewardData(rewardSaveDatas[i].DataId);
                Sprite icon = _gameSettings.GetRewardSprite(rewardData.SpriteName);
                RewardUnit rewardUnit = new RewardUnit(rewardData, icon, rewardSaveDatas[i].CurrentAmount);
                rewardContent.InitializeRewardContent(rewardUnit);
            }
        }

        private void CloseThisPanel()
        {
            gameObject.SetActive(false);
        }
        #endregion

    }
}
