using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UserInterface {

    public class UIStageBarContent: MonoBehaviour {

        [SerializeField] private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;

        [SerializeField] private Image _backgroundImg;
        [SerializeField] private TMP_Text _stageCountText;

        [SerializeField] private Sprite _dangerZoneBgSprite;
        [SerializeField] private Sprite _safeZoneBgSprite;
        [SerializeField] private Sprite _superZoneBgSprite;

        [SerializeField] private Color _defaultDangerZoneTextColor;
        [SerializeField] private Color _currentDangerZoneTextColor;

        [SerializeField] private Color _defaultSafeZoneTextColor;
        [SerializeField] private Color _currentSafeZoneTextColor;

        [SerializeField] private Color _defaultSuperZoneTextColor;
        [SerializeField] private Color _currentSuperZoneTextColor; 
         
        private StageZone _stageZone;
        public void InitializaStageBar(int stageNo, StageZone stageZone,Transform textParent)
        {
            _stageCountText.transform.SetParent(textParent);
            _stageCountText.text = stageNo.ToString();
            _stageZone = stageZone;
            bool isFirstStage = stageNo == 0;
            switch(stageZone)
            {
                case StageZone.DangerZone:
                    _backgroundImg.sprite = _dangerZoneBgSprite;
                    break;
                case StageZone.SafeZone:
                    _backgroundImg.sprite = _safeZoneBgSprite; 
                    break;
                case StageZone.SuperZone:
                    _backgroundImg.sprite = _superZoneBgSprite; 
                    break;
                default:
                    break;
            }
                    ChangeTextColorForCurrentZone(isFirstStage);
        }
        public void ChangeTextColorForCurrentZone(bool isCurrentZone)
        {
            Color color = _defaultDangerZoneTextColor;
            switch(_stageZone)
            {
                case StageZone.DangerZone:
                    color = isCurrentZone ?  _currentDangerZoneTextColor:_defaultDangerZoneTextColor ; 
                    break;
                case StageZone.SafeZone:
                    color = isCurrentZone ? _currentSafeZoneTextColor :_defaultSafeZoneTextColor ; 
                    break;
                case StageZone.SuperZone:
                    color = isCurrentZone ?  _currentSuperZoneTextColor:_defaultSuperZoneTextColor ; 
                    break;
                default:
                    break;
            }
            _stageCountText.color = color;

        }
    }
}
