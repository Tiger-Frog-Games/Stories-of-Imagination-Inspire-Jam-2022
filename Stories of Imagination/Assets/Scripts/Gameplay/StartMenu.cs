using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoriesofImagination
{
    public class StartMenu : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject Credits;
        [SerializeField] private GameObject SettingsPanel;

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public void StartGame()
        {
            SceneManager.LoadScene("Gameplay Scene");
        }

        public void SettingsOpen()
        {
            MainMenu.SetActive(false);
            SettingsPanel.SetActive(true);
        }

        public void SettingsClose()
        {
            SettingsPanel.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void ShowCredits()
        {
            MainMenu.SetActive(false);
            Credits.SetActive(true);
        }

        public void HideCredits()
        {
            MainMenu.SetActive(true);
            Credits.SetActive(false);
        }

        public void exitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}