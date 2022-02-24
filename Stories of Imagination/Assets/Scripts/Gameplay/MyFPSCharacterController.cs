using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ECM2.Characters;

namespace StoriesofImagination
{
    public class MyFPSCharacterController : FirstPersonCharacter
    {
        #region Variables

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnAwake();
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

        }

        private void OnDestroy()
        {
            
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        #endregion

        #region Methods
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        #endregion
    }
}