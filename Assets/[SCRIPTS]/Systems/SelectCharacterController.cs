using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace slotJupiter
{
    public class SelectCharacterController : MonoBehaviour
    {
        [Header("[COMPONENTS]")]
        [SerializeField] private List<CharacterBoxWidget> characterBoxes = new();
        [SerializeField] private Transform selectIndicator;
        [Header("[SETTINGS]")]
        [SerializeField] private float boxScale = 1.15f;
        [SerializeField] private float boxScaleTime = 1f;
        [SerializeField] private Ease boxScaleEase;

        bool moveAllowed;
        bool fadeAllowed;

        private void Awake()
        {
            moveAllowed = true;
            fadeAllowed = true;
            StartCoroutine(Initialized());
        }

        private void OnDestroy()
        {
            CleanupActions();
        }

        #region [Initialized]

        private IEnumerator Initialized()
        {
            yield return InitAction();
            yield return InitData();

            GameManager.instance.READY = true;
        }

        private IEnumerator InitAction()
        {
            foreach (var box in characterBoxes)
            {
                box.enterAction += MoveToBox(box.transform);
                box.clickAction += SelectBox(box.transform);
            }
            yield return null;
        }

        private IEnumerator InitData()
        {
            int index = 0;
            foreach (var box in characterBoxes)
            {
                if (index < GameDatabase.base_PlayerData.Values.Count)
                {
                    var characterData = GameDatabase.base_PlayerData.Values.ElementAt(index);
                    box.Initialize(characterData);
                    index++;
                }
            }
            yield return null;
        }

        #endregion

        #region [Functions]

        private Action MoveToBox(Transform box)
        {
            return () =>
            {
                if (moveAllowed)
                    selectIndicator.transform.position = box.position;
            };
        }

        private Action SelectBox(Transform box)
        {
            return () =>
            {
                moveAllowed = false;
                Vector3 targetScale = new(boxScale, boxScale, boxScale);

                Sequence sequence = DOTween.Sequence();
                sequence.Append(box.DOScale(targetScale, boxScaleTime)).SetEase(boxScaleEase).OnUpdate(
                    () =>
                    {
                        if (box.localScale.x >= targetScale.x
                        && box.localScale.y >= targetScale.y
                        && box.localScale.z >= targetScale.z
                        && fadeAllowed)
                        {
                            fadeAllowed = false;
                            TransitionManager.ins.FadeOut(() => StartCoroutine(GameManager.instance.LoadScene(GAME_SCENE.GAME)));
                        }
                    }
                );
            };
        }
        #endregion

        #region [Cleanup]

        private void CleanupActions()
        {
            foreach (var box in characterBoxes)
            {
                box.enterAction -= MoveToBox(box.transform);
                box.clickAction -= SelectBox(box.transform);
            }
        }

        #endregion
    }
}
