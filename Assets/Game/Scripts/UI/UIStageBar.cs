using DG.Tweening; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using WheelOfFortune.General;
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class UIStageBar: MonoBehaviour {
        #region FIELDS
        // images and texts are not being in the same parent to make the right masked visualization.
        [SerializeField] private RectTransform _imageHolder; // in scroll view content
        [SerializeField] private RectTransform _textHolder; // in outside to not being masked
        [SerializeField] private UIStageBarContent _contentPrefab;


        [Inject] private GameSettings _gameSettings;
        private List<UIStageBarContent> _stageBars;
        private Vector2 _targetPosition;
        private int _currentStageIndex = 0;
        #endregion
        #region INITIALIZATION


        public void InitializeStageBarVisual(int stageCount)
        {
            DestroyOldStageBarDatas();
            _currentStageIndex = 0;
            _stageBars = new List<UIStageBarContent>();
            for(int i = 0; i < stageCount; i++)
            {
                UIStageBarContent stageBar = Instantiate(_contentPrefab, _imageHolder);
                int stageNo = i + 1; // stage no starts from 1

                StageZone stageZone = Helpers.GetStageZone(stageNo, _gameSettings);
                stageBar.InitializaStageBar(stageNo, stageZone, _textHolder); // send the initialized datas to the item
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
                    Destroy(_textHolder.GetChild(i).gameObject);
                    Destroy(_imageHolder.GetChild(i).gameObject);
                }
            }
        }
        #endregion
        #region BEHAVIOUR

        private void AnimateStageBar()
        {
            ApplyTextVisual();
            if(_currentStageIndex == 0)
            {
                StartCoroutine(AnimateAfterFrame()); // there is a horizontal layout component bug. it doesnt apply the local position at start and it causes first stage bar item not to center. so at start, we wait for 1 frame, then animate.
            }
            else
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
                _stageBars[_currentStageIndex - 1].ChangeTextColorForCurrentZone(false); // change back the previous stage's bar item's color
            }
            if(_currentStageIndex < _stageBars.Count)
            {
                _stageBars[_currentStageIndex].ChangeTextColorForCurrentZone(true);// set the next stage's bar item's color
            }
        }
        #endregion

    }
}
