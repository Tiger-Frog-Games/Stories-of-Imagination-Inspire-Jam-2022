using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace StoriesofImagination
{
    public class PuaseMenu : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject SettingsPanel;
        [SerializeField] private GameObject ScreenGrayer;
        [SerializeField] private GameObject PauseMenu;

        [SerializeField] private InputActionAsset actions;
        private InputAction openMenuButton;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            openMenuButton = actions.FindAction("Pause");
            if (openMenuButton != null)
            {
                openMenuButton.performed += OnOpenMenuButtonPress;
            }
            openMenuButton?.Enable();
        }

        private void OnDestroy()
        {
            if (openMenuButton != null)
            {
                openMenuButton.performed -= OnOpenMenuButtonPress;
            }
        }

        #endregion

        #region Methods

        private void OnOpenMenuButtonPress(InputAction.CallbackContext obj)
        {
            OnPauseButtonPressed();
        }

        public void OnPauseButtonPressed()
        {
            // In Game

            if (GameStateManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                OnPauseMenuOpen();
            }
            else
            {
                if (SettingsPanel.activeSelf == true)
                {
                    OnSettingsClose();
                }
                else
                {
                    OnPauseMenuClose();
                }
            }

            //
        }

        public void OnPauseMenuOpen()
        {
            GameStateManager.Instance.SetState(GameState.Paused);

            PauseMenu.SetActive(true);
            ScreenGrayer.SetActive(true);
        }
        public void OnPauseMenuClose()
        {
            SettingsPanel.SetActive(false);
            ScreenGrayer.SetActive(false);
            PauseMenu.SetActive(false);

            GameStateManager.Instance.SetState(GameState.Gameplay);
        }

        public void OnSettingsOpen()
        {
            PauseMenu.SetActive(false);
            SettingsPanel.SetActive(true);
        }

        public void OnSettingsClose()
        {
            SettingsPanel.SetActive(false);
            PauseMenu.SetActive(true);
        }

        public void GameExit()
        {
            SceneManager.LoadScene("Main Menu");
        }
        #endregion
    }
}