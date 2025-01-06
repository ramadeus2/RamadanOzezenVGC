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
        [SerializeField] private Button _spinButton;
        [SerializeField] private WheelPiece _piecePrefab;
        [SerializeField] private Transform _wheel;
        private WheelPiece[] _wheelSegments = new WheelPiece[Consts.STAGE_REWARD_AMOUNT];
        private AddressablesManager _addressablesManager;
        private bool _isSpinning = false;
        private SpriteAtlas _spriteAtlas;

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
                _wheelSegments[i] = Instantiate(_piecePrefab, _wheel);
                _wheelSegments[i].transform.eulerAngles = Vector3.forward * angleOfEachPiece * i;
            }
        }

        public void UpdatePieces(List<RewardData> rewardDatas, int rewardAmountMultiplier)
        {
            _addressablesManager.GetRewardAtlas((spriteAtlas) =>
            {
                _spriteAtlas = spriteAtlas;
                for(int i = 0; i < _wheelSegments.Length; i++)
                {
                    int rewardAmount = rewardDatas[i].GetRandomAmount() * rewardAmountMultiplier;
                    Sprite rewardIcon = _spriteAtlas.GetSprite(rewardDatas[i].SpriteName);
                    RewardUnit rewardUnit = new RewardUnit(rewardDatas[i], rewardIcon, rewardAmount);
                    _wheelSegments[i].InitializePieceData(rewardUnit);
                }
                _addressablesManager.ReleaseRewardedAtlas();
            });
        }

        public void StartSpin()
        {
            if(_isSpinning) return;

            _isSpinning = true;

            float targetAngle = Random.Range(0, 360) + (360 * 5);

            _wheel.DORotate(new Vector3(0, 0, -targetAngle), 3f, RotateMode.FastBeyond360)
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
            _wheel.DORotate(new Vector3(0, 0, -snapAngle), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                CollectRewards(_wheelSegments[closestSegmentIndex]);
                _isSpinning = false;

            }); 
        }

        private void CollectRewards(WheelPiece wheelPiece)
        {
            UIManager.Instance.RewardEarned(wheelPiece.RewardUnit);
        }

        
    }
}
