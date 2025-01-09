using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using WheelOfFortune.Utilities;
using System.Collections.Generic;
using WheelOfFortune.Reward; 
using WheelOfFortune.General;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class PickerWheel: MonoSingleton<PickerWheel> {
        #region FIELDS
        [Header("References")] 
        [SerializeField] private WheelPiece _piecePrefab;
        [SerializeField] private Image _wheel;
        [SerializeField] private Image _indicator;


        [Header("Visualization")]

        [Header("Wheel")]
        [SerializeField] private Sprite _wheelIconBronze;
        [SerializeField] private Sprite _wheelIconSilver;
        [SerializeField] private Sprite _wheelIconGold;

        [Header("Indicator")]
        [SerializeField] private Sprite _indicatorIconBronze;
        [SerializeField] private Sprite _indicatorIconSilver;
        [SerializeField] private Sprite _indicatorIconGold;



        [Inject] private GameSettings _gameSettings;
        private WheelPiece[] _wheelSegments;
        private UIManager _uIManager;
        private Button _spinButton;
        private Button _collectButton;
        private StageZone _lastStageZone;
        private bool _isSpinning = false;
        #endregion
        #region INITIALIZATION

        [Inject]
        private void Constructor(SpinButton spinButton, CollectButton leaveButton, UIManager uIManager)
        {
            _spinButton = spinButton.Button;
            _collectButton = leaveButton.Button;
            _uIManager = uIManager;
        }

        private void OnEnable()
        { 
            _spinButton.onClick.RemoveAllListeners();
            _spinButton.onClick.AddListener(StartSpin);
            _collectButton.onClick.RemoveAllListeners();
            _collectButton.onClick.AddListener(CollectAndLeave);

            InitializePieces();
        }
        #endregion


        #region BEHAVIOURS
        /// <summary>
        /// possible reward count is currently set to 8. but just in case, it takes the initialized count and calculates the angles. yet it need new sprite for the wheel if initialized count changes
        /// </summary>
        private void InitializePieces()
        {
            if(_wheelSegments == null)
            {
                int length = _gameSettings.StageRewardUnitAmount;
                _wheelSegments = new WheelPiece[length];
            }
            float angleOfEachPiece = 360f / _wheelSegments.Length;

            for(int i = 0; i < _wheelSegments.Length; i++)
            {
                _wheelSegments[i] = Instantiate(_piecePrefab, _wheel.transform);
                _wheelSegments[i].transform.eulerAngles = Vector3.forward * angleOfEachPiece * i;
            }

        }
        /// <summary>
        /// if its a danger zone, it adds 1 bomb then fills the rest. if it's not, it fills all with good rewarsd
        /// </summary> 
        public void UpdatePieces(List<RewardData> rewardDatas, StageZone stageZone, int rewardAmountMultiplier)
        {
            UpdateStageZoneVisual(stageZone);


            for(int i = 0; i < _wheelSegments.Length; i++)
            {
                int rewardAmount = rewardDatas[i].GetRandomAmount() * rewardAmountMultiplier;
                Sprite rewardIcon;

                if(rewardDatas[i].RewardType == RewardType.Normal)
                {
                    rewardIcon = _gameSettings.NormalRewardAtlas.GetSprite(rewardDatas[i].SpriteName);
                }
                else if(rewardDatas[i].RewardType == RewardType.Bomb)
                {
                    rewardIcon = _gameSettings.BombIcon;
                    ;
                }
                else
                {
                    rewardIcon = _gameSettings.CurrencyAtlas.GetSprite(rewardDatas[i].SpriteName);

                }

                RewardUnit rewardUnit = new RewardUnit(rewardDatas[i], rewardIcon, rewardAmount);
                _wheelSegments[i].InitializePieceData(rewardUnit);
            }





        }
        /// <summary>
        /// for the changing wheel,indicator images when stage zone changes
        /// </summary> 
        private void UpdateStageZoneVisual(StageZone stageZone)
        {
            _collectButton.gameObject.SetActive(stageZone != StageZone.DangerZone);
            _collectButton.interactable = true;
            if(_lastStageZone == stageZone)
            {
                return;
            }
            _lastStageZone = stageZone;
            switch(stageZone)
            {
                case StageZone.DangerZone:
                    _wheel.sprite = _wheelIconBronze;
                    _indicator.sprite = _indicatorIconBronze;
                    break;
                case StageZone.SafeZone:
                    _wheel.sprite = _wheelIconSilver;
                    _indicator.sprite = _indicatorIconSilver;
                    break;
                case StageZone.SuperZone:
                    _wheel.sprite = _wheelIconGold;
                    _indicator.sprite = _indicatorIconGold;
                    break;
                default:
                    break;
            }
        }

        public void StartSpin()
        {
            if(_isSpinning) return;
            _collectButton.interactable = false;
            _isSpinning = true;
                                         // at least spin 5 times
            float targetAngle = Random.Range(0, 360) + (360 * 5);

            _wheel.transform.DORotate(new Vector3(0, 0, -targetAngle), 3f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    SnapAndFinish(targetAngle % 360);
                });
        }

        private void SnapAndFinish(float finalAngle)
        {
            float segmentAngle = 360f / _wheelSegments.Length;

            int closestSegmentIndex = Mathf.RoundToInt(finalAngle / segmentAngle) % _wheelSegments.Length;

            float snapAngle = closestSegmentIndex * segmentAngle;
            _wheel.transform.DORotate(new Vector3(0, 0, -snapAngle), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                // sends the taken reward to the manager so it can initialize the reward-bomb situation.
                WheelPiece takenReward = _wheelSegments[closestSegmentIndex];
                    CheckTheRewardOfThisStage(takenReward); 
 
            });
        }

        

        public void ActivateSpin()
        {
            _isSpinning = false;

        }
        private void CheckTheRewardOfThisStage(WheelPiece wheelPiece) =>

           _uIManager.CheckTheRewardOfThisStage(wheelPiece.RewardUnit);
        private void CollectAndLeave() => _uIManager.CollectAndLeave();

        #endregion

    }
}
