using System;
using _CookingMaster._Scripts.ControllerRelated;
using TMPro;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class ScoresAndTimer : MonoBehaviour
    {
        [SerializeField] private float timeLeft; 
        [SerializeField] private float score;
        
        private TextMeshProUGUI _scoreText, _timerText;
        private PlayerController _playerController;
        private bool _timeUp;

        public float Score
        {
            get => score;
            set => score = value;
        }

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _scoreText = _playerController.PlayersType == PlayerController.PlayerType.PlayerA
                ? UiController.instance.scoreTextA
                : UiController.instance.scoreTextB;
            _timerText = _playerController.PlayersType == PlayerController.PlayerType.PlayerA
                ? UiController.instance.timerTextA
                : UiController.instance.timerTextB;
        }

        private void Update()
        {
            if (_timeUp) return;
            timeLeft -= Time.deltaTime;
            _timerText.text = Mathf.FloorToInt(timeLeft).ToString();
            if (timeLeft <= 0)
            {
                _timeUp = true;
                MainController.instance.SetActionType(GameState.GameOver);
            }
        }

        public void UpdateScore(float value)
        {
            score += value;
            _scoreText.text = Mathf.FloorToInt(score).ToString();
        }

        public void UpdateTimer(int value)
        {
            timeLeft += value;
        }
    }
}
