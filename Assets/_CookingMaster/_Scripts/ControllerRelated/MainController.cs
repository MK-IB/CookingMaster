using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace _CookingMaster._Scripts.ControllerRelated
{
    /*
     * Controls the state change of the game
     */
    public enum GameState
    {
        None,
        Create,
        LevelStart,
        GameOver
    }

    public class MainController : MonoBehaviour
    {
        public static MainController instance;
        
        [SerializeField] private GameState _gameState;
        public static event Action<GameState, GameState> GameStateChanged;

        public GameState GameState
        {
            get => _gameState;
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;
                    if (GameStateChanged != null)
                        GameStateChanged(_gameState, oldState);
                }
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            SetActionType(GameState.Create);
        }

        public void StartGame()
        {
            SetActionType(GameState.LevelStart);
        }

        public void SetActionType(GameState _curState)
        {
            GameState = _curState;
        }
    }
}