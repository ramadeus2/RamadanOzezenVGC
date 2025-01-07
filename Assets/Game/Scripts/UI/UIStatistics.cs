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

namespace WheelOfFortune {

    public class UIStatistics: MonoBehaviour {
        [SerializeField] private UIRewardContent _rewardContentPrefab;
        [SerializeField] private Transform _contentHolder;
        [SerializeField] private GameObject _menuPanel;
        private Button _closeButton;
        private void OnValidate()
        {
            _closeButton = GetComponentInChildren<Button>();
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
            InitializeCollectedItems();
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
        private void InitializeCollectedItems()
        { 
            for(int i = 0; i < _contentHolder.childCount; i++)
            {
                Destroy(_contentHolder.GetChild(i).gameObject);
            }
            List<DataSaveInfo> rewardSaveDatas = SaveSystem.LoadDatas (Consts.SAVE_INFO_NAME_REWARD);
            GameSettings gameSettings = GameManager.Instance.GameSettings; 
            for(int i = 0; i < rewardSaveDatas.Count; i++)
            {
                UIRewardContent rewardContent = Instantiate(_rewardContentPrefab, _contentHolder); 
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
