using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Utilities;

namespace WheelOfFortune {

    public class UIZoneInfoPanel: MonoBehaviour {
        [SerializeField] private UIZoneInfoContent _uiZoneInfoContent;
        private UIZoneInfoContent _superZoneInfoContent;
        private UIZoneInfoContent _safeZoneInfoContent;
        public void InitializeSpecialRewardIcon(StageZone stageZone, int zoneTargetStageNo, Sprite icon)
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
                        _safeZoneInfoContent = Instantiate(_uiZoneInfoContent, transform);
                    }
                    uIZoneInfoContent = _safeZoneInfoContent;
                    break;
                case StageZone.SuperZone: 
                    zoneName = "Super Zone";
                    backgroundColor = Color.yellow;
                    if(!_superZoneInfoContent)
                    {
                        _superZoneInfoContent = Instantiate(_uiZoneInfoContent, transform);
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
            print("reward taken, congratz");
        }

    }
}
