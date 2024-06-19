using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace slotJupiter
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        [Header("[GAMEPLAY]")]
        [ReadOnly] public EGAME_PROCESS GameProcess;
        [ReadOnly] public EGAME_EVENT GameEvent;
        [SerializeField] private float timeToWaitProcess = 0.5f;
        [SerializeField] private float timeToFlipCardBack = 1f;
        [SerializeField] private int maxAction = 6;
        [SerializeField] private int currentAction = 0;
        [ReadOnly, SerializeField] private List<Card> CardsFlip = new();
        
        [Header("[ENEMY]")]
        [SerializeField] private EnemyBrain enemyBrain;

        [field: Header("[INFO]")]
        [SerializeField] private int maxRoom = 10;
        [SerializeField] private int eliteAppearRoom = 5;

        [Serializable]
        public class EnemyAmount
        {
            public int minAmount;
            public int maxAmount;
        }

        [SerializedDictionary("Room", "Amount")]
        [SerializeField] private SerializedDictionary<int, EnemyAmount> enemySpawnSettings = new();
        public int currentRoom;

        [field: Header("[UI Components]")]
        [field: SerializeField] public BoardManager boardManager { get; private set; }
        [SerializeField] private CharacterPanelWidget characterPanel;
        [SerializeField] private TMP_Text roomText;
        [SerializeField] private TMP_Text actionCountText;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            currentRoom = 1;
            currentAction = maxAction;
            yield return new WaitUntil(() => GameDatabase.databaseReady);
            yield return UpdateRoomInfo();
            yield return InitPlayer();
            yield return InitEnemy();
            yield return boardManager.Initialize(GameManager.instance.RuntimeEnemies.Count);
            GameManager.instance.READY = true;
            yield return boardManager.CardAppear();
        }

        private IEnumerator InitPlayer()
        {
            GameManager.instance.RuntimePlayer = CharacterFactory.Create(GameManager.instance.playerCharacterData.name, GameDatabase.base_PlayerData, 0);
            yield return new WaitUntil(() => GameManager.instance.RuntimePlayer != null);
            characterPanel.Initialize(GameManager.instance.RuntimePlayer);
            yield return null;
        }

        private IEnumerator InitEnemy()
        {
            int _randomAmount = Random.Range(enemySpawnSettings[currentRoom].minAmount, enemySpawnSettings[currentRoom].maxAmount);
            yield return new WaitUntil(() => _randomAmount != 0);

            for (int i = 0; i < _randomAmount; i++)
            {
                if (currentRoom < eliteAppearRoom)
                {
                    var filteredList = GameDatabase.base_EnemyData.Where(pair => pair.Value.characterClass != ECHARACTER_CLASS.ELITE).ToList();
                    if (filteredList.Count == 0)
                    {
                        throw new InvalidOperationException("No characters available for selection.");
                    }

                    // Randomly pick an item from the filtered list
                    int randomIndex = Random.Range(0, filteredList.Count);
                    RuntimeCharacter _newEnemy = CharacterFactory.Create(filteredList[randomIndex].Key, GameDatabase.base_EnemyData, i);
                    GameManager.instance.RuntimeEnemies.Add(_newEnemy);
                }
            }
            yield return null;
        }

        private IEnumerator UpdateRoomInfo()
        {
            roomText.text = "ROOM " + currentRoom.ToString();
            actionCountText.text = "Action count : " + currentAction.ToString();
            yield return null;
        }

        public IEnumerator Matching(Card _c)
        {
            if (GameProcess != EGAME_PROCESS.WAIT || CardsFlip.Count < 2)
            {
                CardsFlip.Add(_c);
            }

            if (CardsFlip.Count == 1)
            {
                yield return OnGameEvent(EGAME_EVENT.FLIPCARD_ONE);
                yield return OnGameProcess(EGAME_PROCESS.NORMAL);
            }
            else if (CardsFlip.Count == 2)
            {
                yield return OnGameEvent(EGAME_EVENT.FLIPCARD_TWO);

                bool isMatch = CardsFlip[0].currentCardRuntime.cardName == CardsFlip[1].currentCardRuntime.cardName;

                if (isMatch)
                {
                    yield return OnGameEvent(EGAME_EVENT.MATCHING);
                    Debug.Log("MATCHING !");
                    yield return OnGameProcess(EGAME_PROCESS.WAIT_TO_COMPLETE);
                }
                else
                {
                    Debug.Log("ERROR !");
                    yield return OnGameProcess(EGAME_PROCESS.WAIT);
                    yield return new WaitForSeconds(timeToFlipCardBack);
                    FlipCardBack();
                }

                // Reset cards list and game process
                CardsFlip.Clear();
                yield return OnGameProcess(EGAME_PROCESS.NORMAL);
            }
        }

        private void FlipCardBack()
        {
            foreach (var card in CardsFlip)
                StartCoroutine(card.FlipCard());
        }

        public IEnumerator OnGameEvent(EGAME_EVENT eGame_Event)
        {
            yield return GameEvent = eGame_Event;

            switch (GameEvent)
            {
                case EGAME_EVENT.FLIPCARD_TWO:
                    currentAction--;
                    if (currentAction == 0) currentAction = maxAction;
                    yield return UpdateRoomInfo();
                    break;
            }
        }

        public IEnumerator OnGameProcess(EGAME_PROCESS egameState, Action nextAction = null)
        {
            yield return GameProcess = egameState;

            switch (egameState)
            {
                case EGAME_PROCESS.WAIT:
                    yield return new WaitForSeconds(timeToWaitProcess);
                    yield return OnGameProcess(EGAME_PROCESS.NORMAL);
                    break;
                case EGAME_PROCESS.WAIT_TO_COMPLETE:
                    nextAction?.Invoke();
                    yield return new WaitUntil(() => GameProcess == EGAME_PROCESS.PROCESS_COMPLETE);
                    yield return OnGameProcess(EGAME_PROCESS.NORMAL);
                    break;
            }
        }
    }
}
