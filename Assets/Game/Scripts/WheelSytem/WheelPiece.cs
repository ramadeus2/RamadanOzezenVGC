using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine; 
using UnityEngine.U2D;
using UnityEngine.UI;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune {

    public class WheelPiece: MonoBehaviour {
        [SerializeField] private Image _img;
        [SerializeField] private TMP_Text _rewardAmountText;

        private RewardUnit _rewardUnit;
        public RewardUnit RewardUnit => _rewardUnit;
          
        public void InitializePieceData(RewardUnit rewardUnit  )
        {
            _rewardUnit = rewardUnit;
            _img.sprite = rewardUnit.RewardIcon; 
            if(_rewardUnit.RewardData.RewardType == RewardType.Bomb)
            {
                _rewardAmountText.text = string.Empty;

            } else
            {
                _rewardAmountText.text = $"{rewardUnit.AppliedRewardAmount}x";
            }
        }
       
    }
}
