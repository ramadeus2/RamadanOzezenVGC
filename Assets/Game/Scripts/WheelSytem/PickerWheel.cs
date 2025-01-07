using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using WheelOfFortune.Utilities;
using System.Collections.Generic;
using WheelOfFortune.Reward;
using UnityEngine.U2D;
using WheelOfFortune.Stage;

namespace WheelOfFortune.UserInterface {

    public class PickerWheel: MonoSingleton<PickerWheel> {
        private Button _spinButton;
        [SerializeField] private WheelPiece _piecePrefab;
        [SerializeField] private Image _wheel;
        [SerializeField] private Image _indicator;
        private WheelPiece[] _wheelSegments = new WheelPiece[Consts.STAGE_REWARD_AMOUNT];
        private AddressablesManager _addressablesManager;
        private bool _isSpinning = false;
        private SpriteAtlas _normalRewardSpriteAtlas;
        private SpriteAtlas _currencyRewardSpriteAtlas;
        private StageZone _lastStageZone;

        [Header("Visualization")]
        [Header("Wheel")]
        [SerializeField] private Sprite _wheelIconBronze;
        [SerializeField] private Sprite _wheelIconSilver;
        [SerializeField] private Sprite _wheelIconGold;
        [Header("Indicator")]
        [SerializeField] private Sprite _indicatorIconBronze;
        [SerializeField] private Sprite _indicatorIconSilver;
        [SerializeField] private Sprite _indicatorIconGold;

        private void OnValidate()
        {
            _spinButton = GetComponentInChildren<Button>();
        }
        private void OnEnable()
        {
            if(_spinButton)
            {
                _spinButton.onClick.RemoveAllListeners();
                _spinButton.onClick.AddListener(StartSpin);
            }
            _addressablesManager = AddressablesManager.Instance;
            InitializePieces();
        }

        private void InitializePieces()
        {
            float angleOfEachPiece = 360f / _wheelSegments.Length;

            for(int i = 0; i < _wheelSegments.Length; i++)
            {
                _wheelSegments[i] = Instantiate(_piecePrefab, _wheel.transform);
                _wheelSegments[i].transform.eulerAngles = Vector3.forward * angleOfEachPiece * i;
            }
        }

        public void UpdatePieces(List<RewardData> rewardDatas, StageZone stageZone, int rewardAmountMultiplier)
        {
            UpdateStageZoneVisual(stageZone);
            if(!_addressablesManager)
            {
                _addressablesManager = AddressablesManager.Instance;
            }
            _addressablesManager.GetRewardAtlas((normalSpriteAtlas) =>
            {
                _normalRewardSpriteAtlas = normalSpriteAtlas;

                _addressablesManager.GetRewardAtlas((currencySpriteAtlas) =>
                {
                    _currencyRewardSpriteAtlas = currencySpriteAtlas;


                    for(int i = 0; i < _wheelSegments.Length; i++)
                    {
                        int rewardAmount = rewardDatas[i].GetRandomAmount() * rewardAmountMultiplier;
                        Sprite rewardIcon = null;

                        if(rewardDatas[i].RewardType == RewardType.Normal)
                        {
                            rewardIcon = _normalRewardSpriteAtlas.GetSprite(rewardDatas[i].SpriteName);
                        } else if(rewardDatas[i].RewardType == RewardType.Bomb)
                        {
                            rewardIcon = UIManager.Instance.BombIcon;
                        } else
                        {
                            rewardIcon = _currencyRewardSpriteAtlas.GetSprite(rewardDatas[i].SpriteName);

                        }

                        RewardUnit rewardUnit = new RewardUnit(rewardDatas[i], rewardIcon, rewardAmount);
                        _wheelSegments[i].InitializePieceData(rewardUnit);
                    }
                }, "CurrencySprites");
            }, "NormalRewardSprites");




        }

        private void UpdateStageZoneVisual(StageZone stageZone)
        {
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

            _isSpinning = true;

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
                WheelPiece bomb = GetBomb();
                if(bomb)
                {
                    CheckThisStage(bomb); 
                } else
                {

                    CheckThisStage(_wheelSegments[closestSegmentIndex]);
                }
                _isSpinning = false;

            });
        }
        private WheelPiece GetBomb()
        {
            for(int i = 0; i < _wheelSegments.Length; i++)
            {
                if(_wheelSegments[i].RewardUnit.RewardData.RewardType == RewardType.Bomb)
                {
                    return _wheelSegments[i];
                }
            }
            return null;
        }

        private void CheckThisStage(WheelPiece wheelPiece) =>

            UIManager.Instance.CheckTheStage(wheelPiece.RewardUnit);



    }
}
