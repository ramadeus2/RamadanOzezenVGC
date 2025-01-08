using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune {

public class UISpecialReward : MonoBehaviour
{
       [SerializeField] private Image _image;
       //[SerializeField] private GameObject ;
       //[SerializeField] private Image _image;
        public void InitializeSpecialRewardIcon(Sprite icon)
        {
            _image.sprite = icon;
        }
        public void ActivateSpecialRewardPopUp( )
        {
            print("reward taken, congratz"); 
        }

}
}
