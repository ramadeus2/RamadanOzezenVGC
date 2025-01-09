using DG.Tweening; 
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WheelOfFortune.General; 
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class UICardPanel: MonoBehaviour, IPointerClickHandler {
        #region FIELDS 
        [Header("References")]
        [SerializeField] private Image _rewardImg;
        [SerializeField] private Image _scaledClickImg; // getting touch from anywhere of the screen to skip the card panel visuals. its RaycastTarget is open.

        [Header("Background Data")]
        [SerializeField] private Image _backgroundImg;
        [SerializeField] private Color _backgroundDefaultColor;
        [SerializeField] private Color _backgroundBombColor;
        [SerializeField] private Color _backgroundSafeColor;

        [Header("Card Data")]
        [SerializeField] private Image _cardBackground;
        [SerializeField] private TMP_Text _cardAmountText;
        [SerializeField] private Color _cardSafeColor;
        [SerializeField] private Color _cardBombColor;
        [SerializeField] private Vector2 _cardPopupStartPos = new Vector2(0, 0);
        [SerializeField] private Vector2 _cardPopupTargetPos = new Vector2(0, 0);
        [SerializeField] private string[] _bombMessages;

        private GameSettings _gameSettings;
        private UIManager _uiManager;
        private Button _giveUpButton;
        private ReviveButton _reviveButton;
        private bool _skipClicked;
        #endregion  

        #region INITIALIZATION 
        [Inject]
        private void Constructor(ReviveButton reviveButton, GiveUpButton giveUpButton, UIManager uIManager, GameSettings gameSettings)
        {
            _reviveButton = reviveButton;
            _giveUpButton = giveUpButton.Button;
            _uiManager = uIManager;
            _gameSettings = gameSettings;
        }
        private void OnEnable()
        {
            _reviveButton.Button.onClick.RemoveAllListeners();
            _reviveButton.Button.onClick.AddListener(RequestRevive);
            _giveUpButton.onClick.RemoveAllListeners();
            _giveUpButton.onClick.AddListener(GiveUpRewards);
        }

        #endregion
        #region BEHAVIOURS

        internal void VisualizeSafePanel(Sprite icon, int amount)
        {
            gameObject.SetActive(true);
            _scaledClickImg.enabled = true;

            _cardAmountText.text = "x" + amount;
            _rewardImg.sprite = icon;
            SetColors(_backgroundSafeColor, _cardSafeColor);

            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupStartPos, _cardPopupTargetPos, duration: .3f, isBoumerang: false, Ease.OutBack);

        }
        public void VisualizeBombPanel()
        {
            gameObject.SetActive(true);
            _reviveButton.gameObject.SetActive(true);
            _giveUpButton.gameObject.SetActive(true);


            _cardAmountText.text = string.Empty;
            for(int i = 0; i < _bombMessages.Length - 1; i++) // dont give line space to the last one
            {
                _cardAmountText.text += _bombMessages[i] + "\n";
            }
            _cardAmountText.text += _bombMessages[^1]; 


            _rewardImg.sprite = _gameSettings.BombIcon;
            SetColors(_backgroundBombColor, _cardBombColor);


            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupStartPos, _cardPopupTargetPos, duration: .3f, isBoumerang: false, Ease.OutBack);
            Helpers.AnimateUIObjectFade(_backgroundImg, 0.0f, 1.0f, 0.3f, false);

        }

        private void SetColors(Color backgroundColor, Color cardColor)
        {
            _backgroundImg.gameObject.SetActive(true);
            Color clr = _backgroundDefaultColor;
            _backgroundImg.color = clr;
            _backgroundImg.DOColor(backgroundColor, 0.5f);
            _cardBackground.color = cardColor;
        }
        public void InitializeReviveAmountText(int price)
        {
            string text = "Revive x" + price;
            _reviveButton.ButtonText.text = text;
        }
        public void OnPointerClick(PointerEventData eventData) // getting touch from right here
        {
            if(_skipClicked)
            {
                return;
            }
            _skipClicked = true;


            RewardApproved(); // it sends info to initalize the next rewards. so its being called from here. not after completing the animation so it can on the background when the card is in front of the wheel.
            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupTargetPos, _cardPopupStartPos, duration: .3f, isBoumerang: false, Ease.InBack, delay: 0, () =>
            {
                ClosePanel();
                _skipClicked = false;
                PickerWheel.Instance.ActivateSpin();

            });

        }
        public void ClosePanel()
        {
            gameObject.SetActive(false);
            _reviveButton.gameObject.SetActive(false);
            _giveUpButton.gameObject.SetActive(false);
            _backgroundImg.DOFade(0.0f, .5f).OnComplete(() =>
            {
                _backgroundImg.gameObject.SetActive(false);

            });
            _scaledClickImg.enabled = false;

        }


        private void GiveUpRewards()
        {
            Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupTargetPos, _cardPopupStartPos, duration: .3f, isBoumerang: false, Ease.InBack, delay: 0, () =>
            {
                _uiManager.GiveUpRewards();
                PickerWheel.Instance.ActivateSpin();
                Helpers.AnimateUIObjectFade(_backgroundImg, 1.0f, 0.0f, 0.3f, false);

            });
        }
        private void RequestRevive()
        {
            if(_uiManager.RequestRevive())// if we can buy revive
            {
                if(_skipClicked)
                {
                    return;
                }
                _skipClicked = true;


                RewardApproved();
                Helpers.AnimateUIObjectMove(_cardBackground.rectTransform, _cardPopupTargetPos, _cardPopupStartPos, duration: .3f, isBoumerang: false, Ease.InBack, delay: 0, () =>
                {
                    ClosePanel();
                    Helpers.AnimateUIObjectFade(_backgroundImg, 1.0f, 0.0f, 0.3f, false);
                    _skipClicked = false;
                    PickerWheel.Instance.ActivateSpin();
                });
            }
            else
            {
                // if we cant buy revive, it shakes the button little bit.
                _reviveButton.Button.interactable = false;
                _reviveButton.Button.image.rectTransform.DOShakeAnchorPos(.3f, 10, 25).OnComplete(() =>
                {
                    _reviveButton.Button.interactable = true;
                });
            }

        }
        private void RewardApproved() => _uiManager.SetNextStage();
        #endregion

    }
}
