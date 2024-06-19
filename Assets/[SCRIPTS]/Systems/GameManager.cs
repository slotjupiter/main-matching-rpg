using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace slotJupiter
{
    public enum GAME_SCENE
    {
        MAIN_MENU, SELECT_CHARACTER, GAME
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("[DATA MANAGE]")]
        public CharacterData playerCharacterData;
        public RuntimeCharacter RuntimePlayer;
        public List<RuntimeCharacter> RuntimeEnemies = new();
        public List<ItemData> selectedItem = new();

        [Header("[SCENES MANAGE]")]
        public SerializedDictionary<GAME_SCENE, int> scenes = new();
        public bool READY { get; set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }

            GameDatabase.Initialize();
            READY = false;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #region  [SCENE]
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(FadeToNewScene());
        }

        private IEnumerator FadeToNewScene()
        {
            yield return new WaitUntil(() => READY);
            TransitionManager.ins?.FadeIn(() => READY = false);
        }

        public IEnumerator LoadScene(GAME_SCENE game_Scene)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenes[game_Scene]);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        #endregion

        public void DebugLog(string _text)
        {
            Debug.Log(_text);
        }
    }
}
