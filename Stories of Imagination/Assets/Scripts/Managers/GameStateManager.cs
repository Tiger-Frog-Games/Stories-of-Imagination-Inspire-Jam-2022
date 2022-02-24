using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{

    public enum GameState
    {
        Gameplay,
        Paused
    }

    public class GameStateManager 
    {
        private static GameStateManager _instance;
        public static GameStateManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        //initially setting the game play to paused in the UIAnimator
        private GameStateManager()
        {
            
        }

        #region Variables

        public GameState CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(GameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;


        #endregion

        #region Methods

        public void SetState( GameState newGameState )
        {
            if (CurrentGameState == newGameState)
            {
                return;
            }

            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }

        #endregion
    }
}