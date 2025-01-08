using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Utilities;

namespace WheelOfFortune {

public class UIZoneInfoContent : MonoBehaviour
{
        [SerializeField] TMP_Text _zoneNameText;
        [SerializeField] TMP_Text _zoneTargetStageNoText;
        [SerializeField] Image _zoneBackground;
        [SerializeField] Image _zoneIcon;
        public void InitializeUIZoneInfo(string zoneName,int zoneTargetStageNo,Sprite zoneIcon,Color bgColor)
        {
            _zoneNameText.text = zoneName;
            _zoneTargetStageNoText.text = zoneTargetStageNo.ToString();
            _zoneIcon.sprite = zoneIcon;
            _zoneBackground.color = bgColor;
        }
    }
}
