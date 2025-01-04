using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using WheelOfFortune.Utilities;
using System.Collections.Generic;
using WheelOfFortune.Reward;
using UnityEngine.U2D;
using WheelOfFortune.Stage;

namespace WheelOfFortune {

    public class PickerWheel: MonoSingleton<PickerWheel> {
        [SerializeField] private Button _spinButton;
        [SerializeField] private WheelPiece _piecePrefab;
        [SerializeField] private Transform _wheel;
        private WheelPiece[] _wheelSegments = new WheelPiece[Consts.STAGE_REWARD_AMOUNT];
        private AddressablesManager _addressablesManager;
        private bool _isSpinning = false;
        private SpriteAtlas _spriteAtlas;
        private void OnValidate()
        {
            if(!_spinButton)
            {
                return;
            }
            _spinButton.onClick.RemoveAllListeners();
            _spinButton.onClick.AddListener(StartSpin);
        }
        private void OnEnable()
        {
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

                    _wheelSegments[i].InitializePieceData(rewardDatas[i], _spriteAtlas.GetSprite(rewardDatas[i].SpriteName), rewardAmount);
                }
                _addressablesManager.ReleaseRewardedAtlas();
            });
        }

        public void StartSpin()
        {
            if(_isSpinning) return;

            _isSpinning = true;

            float targetAngle = Random.Range(0, 360) + (360 * 5);

            Sequence spinSequence = DOTween.Sequence();

            spinSequence.Append(_wheel.DORotate(new Vector3(0, 0, -360), .5f, RotateMode.FastBeyond360)
                .SetEase(Ease.InQuad));


            spinSequence.Append(_wheel.DORotate(new Vector3(0, 0, -targetAngle), 2f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    SnapToSegment(targetAngle % 360);
                    _isSpinning = false;
                });
        }

        private void SnapToSegment(float finalAngle)
        {
            float segmentAngle = 360f / _wheelSegments.Length;

            int closestSegmentIndex = Mathf.RoundToInt(finalAngle / segmentAngle) % _wheelSegments.Length;

            float snapAngle = closestSegmentIndex * segmentAngle;
            _wheel.DORotate(new Vector3(0, 0, -snapAngle), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                CollectRewards(_wheelSegments[closestSegmentIndex]);
                SetNextStage();
            });

            //Debug.Log(_wheelSegments[closestSegmentIndex].name, _wheelSegments[closestSegmentIndex].gameObject);


        }

        private void CollectRewards(WheelPiece wheelPiece)
        {
            //throw new System.NotImplementedException();
        }

        private void SetNextStage()
        {
            (AutomaticStageSystem.Instance as AutomaticStageSystem).InitializeNextStage();
        }
    }
}
