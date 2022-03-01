using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoriesofImagination
{
    public class UILoader : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private bool loadUi, LoadTerrain;

        void Start()
        {
            if (loadUi == true)
            {
                checkIfUISceneIsLoaded();
            }
            if (LoadTerrain == true)
            {
                checkIfTerrainSceneIsLoaded();
            }
        }

        private void checkIfUISceneIsLoaded()
        {
            if (SceneManager.GetSceneByName("UI Scene").isLoaded == false)
            {
                SceneManager.LoadScene("UI Scene", LoadSceneMode.Additive);
            }
        }

        private void checkIfTerrainSceneIsLoaded()
        {
            if (SceneManager.GetSceneByName("Terrain").isLoaded == false)
            {
                SceneManager.LoadScene("Terrain", LoadSceneMode.Additive);
            }
        }

    }
}