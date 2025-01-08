using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.General;

namespace WheelOfFortune.Utilities {

    public class Helpers: MonoBehaviour {
        public static int[] ShuffleArrayIndexes(int arrayLength)
        {
            int[] indexOrders = new int[arrayLength];
            for(int i = 0; i < indexOrders.Length; i++)
            {
                indexOrders[i] = i;
            }
            for(int i = 0; i < indexOrders.Length; i++)
            {
                int index = Random.Range(0, indexOrders.Length);

                int temp = indexOrders[i];
                indexOrders[i] = indexOrders[index];
                indexOrders[index] = temp;
            }
            return indexOrders;
        }
        public static StageZone GetStageZone(int _currentStageNo)
        {
            StageZone stageZone = StageZone.DangerZone;
            if(_currentStageNo > 0 && _currentStageNo % GameManager.Instance.GameSettings.StageSuperZoneThreshold == 0)
            {
                stageZone = StageZone.SuperZone;
            }
            else if(_currentStageNo > 0 && _currentStageNo % GameManager.Instance.GameSettings.StageSafeZoneThreshold == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
        public static StageZone GetStageZone(int stageNo, GameSettings gameSettings)
        {
            StageZone stageZone = StageZone.DangerZone;
            if(stageNo != 0 && stageNo % gameSettings.StageSuperZoneThreshold == 0)
            {
                stageZone = StageZone.SuperZone;
            }
            else if(stageNo != 0 && stageNo % gameSettings.StageSafeZoneThreshold == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
        public static void AnimateUIObjectMove(RectTransform rect, Vector2 startPos, Vector2 targetPos, float duration,  bool isBoumerang,  Ease ease = Ease.Linear, float delay=0, System.Action onComplete = null)
        {
            rect.gameObject.SetActive(true);
            rect.anchoredPosition = startPos;
            rect.DOAnchorPos(targetPos, duration).OnComplete(() =>
            {
                if(isBoumerang)
                {
                    rect.DOAnchorPos(startPos, duration).OnComplete(() =>
                    {

                        onComplete?.Invoke();

                    }).SetEase(ease).SetDelay(delay);
                }
                else
                {
                    onComplete?.Invoke();
                }
            }).SetEase(ease).SetDelay(isBoumerang ? 0 : delay);
        }
        public static void AnimateUIObjectFade(Image image, float alphaStart,float alphaEnd, float duration, bool isBoumerang,   float delay = 0, System.Action onComplete = null)
        {
            image.gameObject.SetActive(true);
            Color clr = image.color;
            clr.a = alphaStart;
            image.color = clr; 
            image.DOFade(alphaEnd, duration).OnComplete(() =>
            {
                if(isBoumerang)
                {
                    image.DOFade(alphaStart, duration).OnComplete(() =>
                    {

                        onComplete?.Invoke();

                    }).SetDelay(delay);
                }
                else
                {
                    onComplete?.Invoke();
                }
            }).SetDelay(isBoumerang ? 0 : delay);
        }
    }
}
