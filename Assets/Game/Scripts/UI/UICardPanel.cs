using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WheelOfFortune.General;
using WheelOfFortune.Stage;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UserInterface {

    public class UICardPanel: MonoBehaviour, IPointerClickHandler {

        [SerializeField] private Image _rewardImg;
        [SerializeField] private Image _scaledClickImg;
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _reviveButton; 



        [SerializeField] private GameObject _buttonsHolder;
        [SerializeField] private Image _bombBackground;
        [Header("Card Data")]
        [SerializeField] private Image _cardBackground;
        [SerializeField] private Color _cardSafeColor;
        [SerializeField] private Color _cardBombColor;
        private void OnEnable()
        {
            _reviveButton.onClick.AddListener(RequestRevive);
            _giveUpButton.onClick.AddListener(GiveUpRewards); 
        }



        private void OnDisable()
        {
            _reviveButton.onClick.RemoveListener(RequestRevive);
            _giveUpButton.onClick.RemoveListener(GiveUpRewards); 
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            RewardApproved();
            ClosePanel();
        }
        public void ClosePanel()
        {
            gameObject.SetActive(false);
            _buttonsHolder.SetActive(false);
            _bombBackground.gameObject.SetActive(false);
            _scaledClickImg.enabled = false;

        }

        public void VisualizeBombPanel()
        {
            gameObject.SetActive(true);
            _buttonsHolder.SetActive(true);
            _cardBackground.color = _cardBombColor;
            _bombBackground.gameObject.SetActive(true);
            _giveUpButton.gameObject.SetActive(true);
            _reviveButton.gameObject.SetActive(true);
            _rewardImg.sprite = GameManager.Instance.GameSettings.BombIcon; 
        }

        internal void VisualizeSafePanel(Sprite icon, StageZone stageZone)
        {
            gameObject.SetActive(true);
            _cardBackground.color = _cardSafeColor;
            _rewardImg.sprite = icon;
            _scaledClickImg.enabled = true; 

        } 
        private void GiveUpRewards() => UIManager.Instance.GiveUpRewards();
        private void RequestRevive() => UIManager.Instance.RequestRevive();
        private void RewardApproved() => UIManager.Instance.SetNextStage();
    }
}
