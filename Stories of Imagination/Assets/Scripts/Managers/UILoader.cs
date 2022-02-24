using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoriesofImagination
{
    public class UILoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            checkIfUISceneIsLoaded();
        }

        private void checkIfUISceneIsLoaded()
        {
            if (SceneManager.GetSceneByName("UI Scene").isLoaded == false)
            {
                SceneManager.LoadScene("UI Scene", LoadSceneMode.Additive);
            }
        }

    }
}