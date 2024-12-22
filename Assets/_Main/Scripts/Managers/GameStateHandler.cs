using System;
using System.Collections;
using System.Collections.Generic;
using dincdev._Main.Scripts.Operations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace dincdev
{
    public class GameStateHandler : SingletonMonoBehaviour<GameStateHandler>
    {
        [field: SerializeField] public GameObject GameWinScreen { get; private set; }
        [field: SerializeField] public GameObject GameLostScreen { get; private set; }
       
        public Button NextLevelButton;
        public Button RetryLevelButton;

        public override void Awake()
        {
            base.Awake();
            NextLevelButton.onClick.AddListener(LoadNextLevel);
            RetryLevelButton.onClick.AddListener(RestartLevel);
        }

        private void OnDisable()
        {
            NextLevelButton.onClick.RemoveListener(LoadNextLevel);
            RetryLevelButton.onClick.RemoveListener(RestartLevel);
        }

        public void GameOver()
        {
            GameLostScreen.SetActive(true);
            Debug.Log("Game Over");
        }

        public void GameWin()
        {
            GameWinScreen.SetActive(true);
            Debug.Log("Game Win");
        }
        
        public void LoadNextLevel()
        {
            Debug.Log("Load Next Level");
        }
        
        public void RestartLevel()
        {
            Debug.Log("Restart Level");
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
