using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace WheelOfFortune.UserInterface {

public class UIZoneInfoContent : MonoBehaviour
{
        [SerializeField] private Image _zoneBackground;
        [SerializeField] private Image _zoneIcon;
        [SerializeField] public Image ZoneIcon => _zoneIcon;
        [SerializeField] private TMP_Text _zoneNameText;
        [SerializeField] private TMP_Text _zoneTargetStageNoText;
        public void InitializeUIZoneInfo(string zoneName,int zoneTargetStageNo,Sprite zoneIcon,Color bgColor)
        {
            _zoneNameText.text = zoneName;
            _zoneTargetStageNoText.text = zoneTargetStageNo.ToString();
            _zoneIcon.sprite = zoneIcon;
            _zoneBackground.color = bgColor;
        }
    }
}
