using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UserInterface {

    public class UIStageBar: MonoBehaviour {
        [SerializeField] private RectTransform _imageHolder;
        [SerializeField] private RectTransform _textHolder;
        [SerializeField] private UIStageBarContent _contentPrefab;

        private List<UIStageBarContent> _stageBars;
        private int _currentStageIndex = 0;
        private Vector2 _targetPosition;



        public void InitializeStageVisual(int stageCount)
        {
            DestroyOldStageBarDatas();
            _stageBars = new List<UIStageBarContent>();
            for(int i = 0; i < stageCount; i++)
            {
                UIStageBarContent stageBar = Instantiate(_contentPrefab, _imageHolder);
                int stageNo = i + 1; // stage no starts from 1

                StageZone stageZone = Helpers.GetStageZone(stageNo);
                stageBar.InitializaStageBar(stageNo, stageZone, _textHolder);
                _stageBars.Add(stageBar);
            }
            AnimateStageBar();
        }

        private void DestroyOldStageBarDatas()
        {
            if(_stageBars != null)
            {
                for(int i = 0; i < _stageBars.Count; i++)
                {
                    Destroy(_stageBars[i].gameObject);
                }
            }
        }

        private void AnimateStageBar()
        {
            ApplyTextVisual();
            if(_currentStageIndex == 0)
            {
                StartCoroutine(AnimateAfterFrame());
            } else
            {
                Animate();
            }

        }
        private void Animate()
        {
            float contentOffset = _stageBars[_currentStageIndex].RectTransform.localPosition.x;
            _targetPosition = new Vector2(-contentOffset, _imageHolder.anchoredPosition.y);
            _imageHolder.DOAnchorPos(_targetPosition, 0.5f);
            _textHolder.DOAnchorPos(_targetPosition, 0.5f);
        }
        private IEnumerator AnimateAfterFrame()
        {
            yield return null;
            Animate();
        }
        public void SetNextStage()
        {
            _currentStageIndex++;
            AnimateStageBar();
        }

        private void ApplyTextVisual()
        {
            if(_currentStageIndex > 0)
            {
                _stageBars[_currentStageIndex - 1].ChangeTextColorForCurrentZone(false);
            }
            _stageBars[_currentStageIndex].ChangeTextColorForCurrentZone(true);
        }

    }
}
