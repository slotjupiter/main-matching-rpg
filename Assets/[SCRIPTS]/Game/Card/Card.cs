using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace slotJupiter
{
    public class Card : MonoBehaviour
    {
        public RuntimeCard currentCardRuntime;

        [Header("[SETTINGS]")]
        [SerializeField] private float flipTime = 0.5f;
        [SerializeField] private Ease easeFlip;
        [Header("[SPRITES]")]
        [SerializeField] private Sprite frontSprite;
        [SerializeField] private Sprite backSprite;
        [SerializeField] private Image cardBorder;
        [Header("[CARD INFO]")]
        [SerializeField] private TMP_Text cardName;
        [SerializeField] private Image cardImage;
        [SerializeField] private Slider hpSlider;

        private bool flipAllowed;
        private bool isFacedUp;

        public Card Initialize(RuntimeCard _runtimeCard)
        {
            currentCardRuntime = _runtimeCard;
            switch (currentCardRuntime.cardType)
            {
                case ECARD_TYPE.ENEMY:
                    InitCardEnemy(currentCardRuntime, ref currentCardRuntime.runtimeEnemy);
                    return currentCardRuntime.card;
                case ECARD_TYPE.ITEM:
                    InitCardItem(currentCardRuntime, currentCardRuntime.itemData);
                    return currentCardRuntime.card;
                case ECARD_TYPE.ABILITY:
                    InitCardAbility(currentCardRuntime, currentCardRuntime.abilityData);
                    return currentCardRuntime.card;
                default:
                    return null;
            }
        }
        //Init Enemy
        private void InitCardEnemy(RuntimeCard _runtimeCard, ref RuntimeCharacter _enemyCharacter)
        {
            currentCardRuntime = _runtimeCard;
            currentCardRuntime.cardName = _enemyCharacter.characterData.info.Name + _enemyCharacter.PT.Get<int>(STATE.CHARACTER_INDEX).Value;
            cardName.text = _enemyCharacter.characterData.info.Name;
            cardImage.sprite = _enemyCharacter.characterData.info.Sprite;
            cardImage.SetNativeSize();
            hpSlider.maxValue = _enemyCharacter.PT.Get<int>(ECHARACTER_STATUS.MAX_HP).Value;
            hpSlider.value = _enemyCharacter.PT.Get<int>(ECHARACTER_STATUS.HP).Value;

            transform.localScale = Vector3.zero;
        }

        //Init Item
        private void InitCardItem(RuntimeCard _runtimeCard, ItemData _itemData)
        {
            currentCardRuntime = _runtimeCard;
            currentCardRuntime.cardName = _itemData.info.Name;
            cardName.text = _itemData.info.Name;
            cardImage.sprite = _itemData.info.Sprite;
            cardImage.SetNativeSize();

            transform.localScale = Vector3.zero;
        }

        //Init Ability
        private void InitCardAbility(RuntimeCard _runtimeCard, AbilityData _abilityData)
        {
            currentCardRuntime = _runtimeCard;
            currentCardRuntime.cardName = _abilityData.info.Name;
            cardName.text = _abilityData.info.Name;
            cardImage.sprite = _abilityData.info.Sprite;
            cardImage.SetNativeSize();

            transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            cardBorder.sprite = backSprite;
            flipAllowed = true;
            isFacedUp = false;
        }

        #region [Interacts]
        private void OnMouseDown()
        {
            if (GameController.instance.GameProcess == EGAME_PROCESS.WAIT_TO_COMPLETE || GameController.instance.GameProcess == EGAME_PROCESS.WAIT)
                return;
            if (currentCardRuntime.PT.Get<ECARD_STATE>(STATE.CARD_STATE).Value == ECARD_STATE.FLIP || currentCardRuntime.PT.Get<ECARD_STATE>(STATE.CARD_STATE).Value == ECARD_STATE.FLIPPING)
                return;

            if (flipAllowed)
                StartCoroutine(FlipCard());
        }

        private void OnMouseEnter()
        {
            if (GameController.instance.GameProcess == EGAME_PROCESS.WAIT_TO_COMPLETE)
                return;
            transform.DOScale(1.25f, 0.25f);
        }

        private void OnMouseExit()
        {
            if (GameController.instance.GameProcess == EGAME_PROCESS.WAIT_TO_COMPLETE)
                return;
            transform.DOScale(1f, 0.25f);
        }
        #endregion

        #region [Functions]

        public IEnumerator Appear()
        {
            transform.DOScale(1f, 0.15f);
            yield return null;
        }

        public IEnumerator Disappear()
        {
            transform.DOScale(0f, 0.1f);
            yield return null;
        }

        private void ShowData(bool showCommonInfo, bool showHP)
        {
            cardName.gameObject.SetActive(showCommonInfo);
            cardImage.gameObject.SetActive(showCommonInfo);
            hpSlider.gameObject.SetActive(showHP);
        }

        public IEnumerator FlipCard()
        {
            flipAllowed = false;
            currentCardRuntime.PT.Get<ECARD_STATE>(STATE.CARD_STATE).Value = ECARD_STATE.FLIPPING;
            if (!isFacedUp)
            {

                Sequence sequence = DOTween.Sequence();
                sequence.Append(transform.DORotate(new Vector3(0f, 180f, 0f), flipTime)).SetEase(easeFlip).OnUpdate(
                    () =>
                    {
                        if (transform.rotation.eulerAngles.y >= 90f)
                        {
                            cardBorder.sprite = frontSprite;
                            if (currentCardRuntime.cardType == ECARD_TYPE.ENEMY)
                                ShowData(true, true);
                            else
                                ShowData(true, false);
                        }
                    }
                ).OnComplete(() =>
                {
                    StartCoroutine(GameController.instance.Matching(this));
                    currentCardRuntime.PT.Get<ECARD_STATE>(STATE.CARD_STATE).Value = ECARD_STATE.FLIP;
                });
            }
            else if (isFacedUp)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(transform.DORotate(new Vector3(0f, 0f, 0f), flipTime)).SetEase(easeFlip).OnUpdate(
                    () =>
                    {
                        if (transform.rotation.eulerAngles.y <= 90f)
                        {
                            cardBorder.sprite = backSprite;
                            ShowData(false, false);
                        }
                    }
                ).OnComplete(() =>
                {
                    currentCardRuntime.PT.Get<ECARD_STATE>(STATE.CARD_STATE).Value = ECARD_STATE.UNFLIP;
                });
            }
            yield return new WaitForSeconds(0.1f);
            flipAllowed = true;
            isFacedUp = !isFacedUp;
        }
    }
    #endregion
}
