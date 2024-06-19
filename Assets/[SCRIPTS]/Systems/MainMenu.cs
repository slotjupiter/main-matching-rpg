using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace slotJupiter
{
    public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Slider startgameSlider;
        bool startgameAllowed;
        bool enterGame;
        private Coroutine holdCoroutine = null;

        private void Awake()
        {
            enterGame = false;
            startgameAllowed = true;
            startgameSlider.value = 0;
            startgameSlider.maxValue = 100;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            if (startgameAllowed) holdCoroutine = StartCoroutine(IncreaseBar());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            holdCoroutine = StartCoroutine(DecreaseBar());
        }

        private IEnumerator IncreaseBar()
        {
            while (true)
            {
                startgameSlider.value += 0.25f;
                if (startgameSlider.value == startgameSlider.maxValue)
                {
                    enterGame = true;
                    startgameSlider.value = startgameSlider.maxValue;
                    TransitionManager.ins.FadeOut(() => StartCoroutine(GameManager.instance.LoadScene(GAME_SCENE.SELECT_CHARACTER)));
                }
                yield return null;
            }
        }

        private IEnumerator DecreaseBar()
        {
            while (true)
            {
                if (!enterGame)
                    startgameSlider.value -= 0.25f;
                yield return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            startgameAllowed = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            startgameAllowed = false;
            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            holdCoroutine = StartCoroutine(DecreaseBar());
        }
    }
}
