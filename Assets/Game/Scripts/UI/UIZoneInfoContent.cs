using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Utilities;

namespace WheelOfFortune {

public class UIZoneInfoContent : MonoBehaviour
{
        [SerializeField] private TMP_Text _zoneNameText;
        [SerializeField] private TMP_Text _zoneTargetStageNoText;
        [SerializeField] private Image _zoneBackground;
        [SerializeField] private Image _zoneIcon;
        [SerializeField] public Image ZoneIcon => _zoneIcon;
        public void InitializeUIZoneInfo(string zoneName,int zoneTargetStageNo,Sprite zoneIcon,Color bgColor)
        {
            _zoneNameText.text = zoneName;
            _zoneTargetStageNoText.text = zoneTargetStageNo.ToString();
            _zoneIcon.sprite = zoneIcon;
            _zoneBackground.color = bgColor;
        }
    }
}
