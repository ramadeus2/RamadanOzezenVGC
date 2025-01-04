using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune {

    public class WheelPiece: MonoBehaviour {
        [SerializeField] private Image _img;
        [SerializeField] private TMP_Text _amountText;

        private RewardData _rewardData;
        public RewardData RewardData => _rewardData;


        public void InitializePieceData(RewardData rewardData,Sprite icon, int amount  )
        {
            _rewardData = rewardData;
            _img.sprite = icon;
            if(rewardData.IsBomb)
            {
                _amountText.text = string.Empty;

            } else
            {
            _amountText.text = $"{amount}x";
            }
        }
       
    }
}
