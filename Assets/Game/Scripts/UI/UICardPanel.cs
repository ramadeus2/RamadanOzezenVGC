using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WheelOfFortune.Stage;

namespace WheelOfFortune.UserInterface {

    public class UICardPanel: MonoBehaviour, IPointerClickHandler {

        [SerializeField] private Image _rewardImg;
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _reviveButton;
        
        
        
        [Header("Panel Data")]
        [SerializeField] private Image _panelBackground;
        [SerializeField] private Color _panelSafeColor;
        [SerializeField] private Color _panelBombColor;
        [Header("Card Data")]
        [SerializeField] private Image _cardBackground;
        [SerializeField] private Color _cardSafeColor;
        [SerializeField] private Color _cardBombColor;

        private bool _isBomb;
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
        public void InitializePanel(Sprite icon, bool isBomb)
        {
            _rewardImg.sprite = icon;
            _isBomb = isBomb;
            _giveUpButton.gameObject.SetActive(_isBomb);
            _reviveButton.gameObject.SetActive(_isBomb);
            if(isBomb)
            {
                _panelBackground.color = _panelBombColor;
                _cardBackground.color = _cardBombColor;
            } else
            {
                _panelBackground.color = _panelSafeColor;
                _cardBackground.color = _cardSafeColor;
            }
            gameObject.SetActive(true);
        }
       
        public void OnPointerClick(PointerEventData eventData)
        {
            if(_isBomb)
            {
                return;
            }
            ClosePanel();
        }
        public void ClosePanel()
        {
            gameObject.SetActive(false);
            SetNextStage();

        }
        private void GiveUpRewards() => UIManager.Instance.GiveUpRewards();
        private void RequestRevive() => UIManager.Instance.RequestRevive();
        private void SetNextStage() => UIManager.Instance.SetNextStage();
         
    }
}
