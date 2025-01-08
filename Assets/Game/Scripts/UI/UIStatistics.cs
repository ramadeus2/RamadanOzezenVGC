using System;
using System.Collections;
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
        [SerializeField] private UIRewardContent _rewardContentPrefab;
        [SerializeField] private GameObject _menuPanel;
        [Header("Normal Reward Info")]
        [SerializeField] private Transform _normalRewardContentHolder;

        [Header("Special Reward Info")]
        [SerializeField] private Transform _specialRewardContentHolder;
        private Button _closeButton;
        
        [Inject]
        private void Constructor(CloseButton closeButton)
        {
            _closeButton = closeButton.Button; 
        }
        private void OnEnable()
        {
            if(!_closeButton)
            {
                gameObject.SetActive(false);
                return;
            }
            _closeButton.onClick.AddListener(CloseThisObject);
            _menuPanel.SetActive(false);
            InitializeCollectedItems(_normalRewardContentHolder,RewardType.Normal);
            InitializeCollectedItems(_specialRewardContentHolder,RewardType.Special);
        }


        private void OnDisable()
        {
            if(!_closeButton)
            {
                gameObject.SetActive(false);
                return;
            }
            _closeButton.onClick.RemoveListener(CloseThisObject);
            _menuPanel.SetActive(true);

        }
        private void InitializeCollectedItems(Transform holder,RewardType rewardType)
        { 
            for(int i = 0; i < holder.childCount; i++)
            {
                Destroy(holder.GetChild(i).gameObject);
            }
            string key = Consts.SAVE_INFO_NAME_NORMAL_REWARD;
            if(rewardType == RewardType.Special)
            {
                key = Consts.SAVE_INFO_NAME_SPECIAL;
            }
            List<DataSaveInfo> rewardSaveDatas = SaveSystem.LoadDatas (key, rewardType);
            GameSettings gameSettings = GameManager.Instance.GameSettings; 
            for(int i = 0; i < rewardSaveDatas.Count; i++)
            {
                UIRewardContent rewardContent = Instantiate(_rewardContentPrefab, holder);
                RewardData rewardData = gameSettings.RewardPool.GetRewardData(rewardSaveDatas[i].DataId);
                Sprite icon = gameSettings.GetRewardSprite(rewardData.SpriteName);
                RewardUnit rewardUnit = new RewardUnit(rewardData, icon , rewardSaveDatas[i].CurrentAmount);
                rewardContent.InitializeRewardContent(rewardUnit);
            }
        }

        private void CloseThisObject()
        {
            gameObject.SetActive(false);
        }
    }
}
