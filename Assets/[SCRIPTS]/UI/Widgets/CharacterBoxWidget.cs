using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace slotJupiter
{
    public class CharacterBoxWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("[INFO]")]
        [SerializeField] private TMP_Text characterName;
        [SerializeField] private Image characterImg;
        [SerializeField] private Image abilityImg;
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private TMP_Text abilityDesc;
        [Header("[STATUS]")]
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text defText;
        [SerializeField] private TMP_Text spdText;

        public Action enterAction { get; set; }
        public Action exitAction { get; set; }
        public Action clickAction { get; set; }
        public CharacterData CHAR_DATA { get; private set; }

        public void Initialize(CharacterData characterData)
        {
            CHAR_DATA = characterData;
            characterName.text = $"<uppercase>{CHAR_DATA.info.Name}</uppercase>";
            characterImg.sprite = CHAR_DATA.info.Sprite;
            characterImg.SetNativeSize();

            abilityImg.sprite = CHAR_DATA.characterAbility.info.Sprite;
            abilityImg.SetNativeSize();
            abilityName.text = $"<uppercase>{CHAR_DATA.characterAbility.info.Name}</uppercase>";
            abilityDesc.text = CHAR_DATA.characterAbility.GetAbilityDescription();

            atkText.text = CHAR_DATA.atk.ToString();
            defText.text = CHAR_DATA.def.ToString();
            spdText.text = CHAR_DATA.spd.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.instance.playerCharacterData = CHAR_DATA;
            clickAction?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            enterAction?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            exitAction?.Invoke();
        }
    }
}
