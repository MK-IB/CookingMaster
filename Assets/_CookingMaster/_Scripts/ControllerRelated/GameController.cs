using System.Collections.Generic;
using _CookingMaster._Scripts.ElementRelated;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _CookingMaster._Scripts.ControllerRelated
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public GameOver gameOverScript;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            MainController.GameStateChanged += GameManager_GameStateChanged;
        }
        private void OnDisable()
        {
            MainController.GameStateChanged -= GameManager_GameStateChanged;
        }
        void GameManager_GameStateChanged(GameState newState, GameState oldState)
        {
            if(newState==GameState.GameOver)
            {
                gameOverScript.gameObject.SetActive(true);
                CheckForWinner();
            }
        }

        [SerializeField] private PlayerController _playerA, _playerB;
        void CheckForWinner()
        {
            if(_playerA.Score > _playerB.Score)
                gameOverScript.ShowRank(_playerA);
            else gameOverScript.ShowRank(_playerB);
        }

        private void Update()
        {
             
        }
        

        public void On_ReplayButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }   
}
