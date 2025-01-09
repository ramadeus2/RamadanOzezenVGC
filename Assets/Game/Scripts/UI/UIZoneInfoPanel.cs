using DG.Tweening; 
using UnityEngine;
using UnityEngine.UI; 
using WheelOfFortune.Utilities; 

namespace WheelOfFortune.UserInterface {

    public class UIZoneInfoPanel: MonoBehaviour {
        [SerializeField] private Transform _contentHolder;
        [SerializeField] private RectTransform _specialRewardPopup;
        [SerializeField] private Image _specialRewardPopupImage;
        [SerializeField] private UIZoneInfoContent _uiZoneInfoContent;
        [SerializeField] private Vector2 _popupStartPos = new Vector2(0, -350);
        [SerializeField] private Vector2 _popupTargetPos = new Vector2(0, 350);

        private UIZoneInfoContent _superZoneInfoContent;
        private UIZoneInfoContent _safeZoneInfoContent;
       


        public void InitializeZoneRewardIcon(StageZone stageZone, int zoneTargetStageNo, Sprite icon)
        {
            UIZoneInfoContent uIZoneInfoContent = null;
            string zoneName = "";
            Color backgroundColor = Color.white;
            switch(stageZone)
            {
                case StageZone.SafeZone:
                    zoneName = "Safe Zone";
                    backgroundColor = Color.green;
                    if(!_safeZoneInfoContent)
                    {
                        _safeZoneInfoContent = Instantiate(_uiZoneInfoContent, _contentHolder);
                    }
                    uIZoneInfoContent = _safeZoneInfoContent;
                    break;
                case StageZone.SuperZone: 
                    zoneName = "Super Zone";
                    backgroundColor = Color.yellow;
                    if(!_superZoneInfoContent)
                    {
                        _superZoneInfoContent = Instantiate(_uiZoneInfoContent, _contentHolder);
                    }
                    uIZoneInfoContent = _superZoneInfoContent;
                    break;
                default:
                    break;
            }

            uIZoneInfoContent.InitializeUIZoneInfo(zoneName, zoneTargetStageNo, icon, backgroundColor); ;




        }

        public void ActivateSpecialRewardPopUp()
        {
          
            _specialRewardPopupImage.sprite = _superZoneInfoContent.ZoneIcon.sprite; 
            Helpers.AnimateUIObjectMove(_specialRewardPopup, _popupStartPos, _popupTargetPos,duration:.7f, isBoumerang: true, Ease.OutBack, delay: 2,  () =>
            { 
                    _specialRewardPopup.gameObject.SetActive(false); 
            } );
          
        }

    }
}
