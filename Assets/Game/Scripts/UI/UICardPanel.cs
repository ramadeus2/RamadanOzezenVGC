using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WheelOfFortune.General;
using WheelOfFortune.Stage;
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class UICardPanel: MonoBehaviour, IPointerClickHandler {

        [SerializeField] private Image _rewardImg;
        [SerializeField] private Image _scaledClickImg;


         
        [SerializeField] private Image _bombBackground;
        [Header("Card Data")]
        [SerializeField] private Image _cardBackground;
        [SerializeField] private Color _cardSafeColor;
        [SerializeField] private Color _cardBombColor;
        [SerializeField] private Vector2 _cardPopupStartPos = new Vector2(0, 0);
        [SerializeField] private Vector2 _cardPopupTargetPos = new Vector2(0, 0);


       private Button _giveUpButton;
       private Button _reviveButton;
        private bool _skipClicked;
        [Inject]
        private void Constructor(ReviveButton revive, GiveUpButton giveUpButton )
        {
            _reviveButton = revive.Button;
            _giveUpButton = giveUpButton.Button; 
        }
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
            if(_skipClicked)
            {
                return;
            }
            _skipClicked = true;
            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupTargetPos, _cardPopupStartPos, duration: .3f, isBoumerang: false, Ease.InBack, delay: 0, () =>
            {
                RewardApproved();
                ClosePanel();
            _skipClicked = false;
            });

        }
        public void ClosePanel()
        {
            gameObject.SetActive(false);
            _reviveButton.gameObject.SetActive(false);
            _giveUpButton.gameObject.SetActive(false);
            _bombBackground.gameObject.SetActive(false);
            _scaledClickImg.enabled = false;

        }

        public void VisualizeBombPanel()
        {
            gameObject.SetActive(true); 
            _reviveButton.gameObject.SetActive(true);
            _giveUpButton.gameObject.SetActive(true);
            _cardBackground.color = _cardBombColor;
            _bombBackground.gameObject.SetActive(true);
            _giveUpButton.gameObject.SetActive(true);
            _reviveButton.gameObject.SetActive(true);
            _rewardImg.sprite = GameManager.Instance.GameSettings.BombIcon;
            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupStartPos, _cardPopupTargetPos, duration: .3f, isBoumerang: false, Ease.OutBack);
            Helpers.AnimateUIObjectFade(_bombBackground, 0.0f, 1.0f, 0.3f, false);

        }

        internal void VisualizeSafePanel(Sprite icon, StageZone stageZone)
        {
            gameObject.SetActive(true);
            _cardBackground.color = _cardSafeColor;
            _rewardImg.sprite = icon;
            _scaledClickImg.enabled = true;
            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupStartPos, _cardPopupTargetPos, duration: .3f, isBoumerang: false, Ease.OutBack);

        }
        private void GiveUpRewards()
        {
            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupTargetPos, _cardPopupStartPos, duration: .3f, isBoumerang: false, Ease.InBack, delay: 0, () =>
            {
                UIManager.Instance.GiveUpRewards();
                Helpers.AnimateUIObjectFade(_bombBackground, 1.0f, 0.0f, 0.3f, false);

            });
        }
        private void RequestRevive()

        {
            if(UIManager.Instance.RequestRevive())
            {
                Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupTargetPos, _cardPopupStartPos, duration: .3f, isBoumerang: false, Ease.InBack, delay: 0, () =>
                {
                    RewardApproved();
                    ClosePanel();
                    Helpers.AnimateUIObjectFade(_bombBackground, 1.0f, 0.0f, 0.3f, false);

                });
            }
            else
            {
                _reviveButton.interactable = false;
                _reviveButton.image.rectTransform.DOShakeAnchorPos(.3f, 10, 25).OnComplete(() =>
                {
                    _reviveButton.interactable = true;
                });
            }

        }
        private void RewardApproved() => UIManager.Instance.SetNextStage();
    }
}
