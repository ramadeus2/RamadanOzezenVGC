using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune {

    public class DoTweenTestAnimation: MonoBehaviour
    {
        [SerializeField] private RectTransform imageRect;
        [SerializeField] private Vector2 finalAnchorPos;
        [SerializeField] private float tweenDuration;
        [SerializeField] private Ease tweenEase;

        private void Start()
        {
            imageRect.DOLocalRotate(Vector3.zero, tweenDuration).SetDelay(2);
            imageRect.DOAnchorPos(finalAnchorPos, tweenDuration).SetEase(tweenEase).SetDelay(2);
        }
    }
}
