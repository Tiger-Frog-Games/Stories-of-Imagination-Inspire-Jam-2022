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

        [SerializeField] private EventChannelSO OnGameEnding;

        [SerializeField] private GameObject SettingsPanel;
        [SerializeField] private GameObject ScreenGrayer;
        [SerializeField] private GameObject PauseMenu;
        [SerializeField] private GameObject endScene;

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

            OnGameEnding.OnEvent += ShowEndScreen;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDestroy()
        {
            if (openMenuButton != null)
            {
                openMenuButton.performed -= OnOpenMenuButtonPress;
            }
            OnGameEnding.OnEvent -= ShowEndScreen;
        }

        #endregion

        #region Methods

        private void OnOpenMenuButtonPress(InputAction.CallbackContext obj)
        {
            OnPauseButtonPressed();
        }

        public void OnPauseButtonPressed()
        {
            
            if (endScene.activeInHierarchy == true || string.Equals( SceneManager.GetActiveScene().name.ToString(), "MainMenu") )
            {
                return;
            }
            
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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PauseMenu.SetActive(true);
            ScreenGrayer.SetActive(true);
        }
        public void OnPauseMenuClose()
        {
            SettingsPanel.SetActive(false);
            ScreenGrayer.SetActive(false);
            PauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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

        private void ShowEndScreen()
        {
            GameStateManager.Instance.SetState(GameState.Paused);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            endScene.SetActive(true);
        }

        public void GameExit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        #endregion
    }
}