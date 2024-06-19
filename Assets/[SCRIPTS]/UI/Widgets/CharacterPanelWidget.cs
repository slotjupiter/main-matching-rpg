using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace slotJupiter
{
    public class CharacterPanelWidget : MonoBehaviour
    {
        [Header("[COMPONENTS]")]
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Image characterIMGFill;
        [SerializeField] private Image abilityIMGFill;
        [Header("[STATUS]")]
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text defText;
        [SerializeField] private TMP_Text spdText;

        public void Initialize(RuntimeCharacter runtimeCharacter)
        {
            hpSlider.maxValue = runtimeCharacter.PT.Get<int>(ECHARACTER_STATUS.MAX_HP).Value;
            hpSlider.value = runtimeCharacter.PT.Get<int>(ECHARACTER_STATUS.HP).Value;

            characterIMGFill.sprite = runtimeCharacter.characterData.info.Sprite;
            characterIMGFill.SetNativeSize();

            abilityIMGFill.sprite = runtimeCharacter.characterData.characterAbility.info.Sprite;
            abilityIMGFill.SetNativeSize();

            atkText.text = runtimeCharacter.PT.Get<int>(ECHARACTER_STATUS.ATTACK).Value.ToString();
            defText.text = runtimeCharacter.PT.Get<int>(ECHARACTER_STATUS.DEFENSE).Value.ToString();
            spdText.text = runtimeCharacter.PT.Get<int>(ECHARACTER_STATUS.SPEED).Value.ToString();
        }

    }
}
