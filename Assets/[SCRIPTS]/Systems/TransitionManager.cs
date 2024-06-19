using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace slotJupiter
{
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager ins;
        [SerializeField] private Image fadeIMG;
        [Header("[SETTINGS]")]
        [SerializeField] private float fadeInTime = 0.5f;
        [SerializeField] private Ease fadeInEase;
        [SerializeField] private float fadeOutTime = 0.5f;
        [SerializeField] private Ease fadeOutEase;


        private void Awake()
        {
            ins = this;
            FadeIn();
        }

        public void FadeIn(Action nextAction = null)
        {
            fadeIMG.DOFade(0f, fadeInTime).SetEase(fadeInEase).OnComplete(
                () =>
                {
                    fadeIMG.raycastTarget = false;
                    nextAction?.Invoke();
                });
        }

        public void FadeOut(Action nextAction = null)
        {
            fadeIMG.raycastTarget = true;
            fadeIMG.DOFade(1f, fadeOutTime).SetEase(fadeOutEase).OnComplete(
                () =>
                {
                    nextAction?.Invoke();
                }); ;
        }
    }
}
