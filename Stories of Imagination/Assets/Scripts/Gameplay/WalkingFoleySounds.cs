using ECM2.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class WalkingFoleySounds : MonoBehaviour
    {
        #region Variables

        [SerializeField] private AudioSource footAudioSource;
        [SerializeField] private CharacterMovement cm;

        [SerializeField] private AudioClip[] dirtFootClips;
        private List<int> dirtFoodClipsIndexes;
        [SerializeField] private AudioClip[] woodFootClips;
        private List<int> woodFoodClipsIndexes;

        [SerializeField] private float lastFootStepCD;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

            dirtFoodClipsIndexes = new List<int>();
            woodFoodClipsIndexes = new List<int>();

            for (int i = 0; i < dirtFootClips.Length; i++)
            {
                dirtFoodClipsIndexes.Add(i);
            }

            for (int i = 0; i < woodFootClips.Length; i++)
            {
                woodFoodClipsIndexes.Add(i);
            }

            suffleIndexes(dirtFoodClipsIndexes);
            suffleIndexes(woodFoodClipsIndexes);

            lastFootStep = int.MinValue;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }


        private float lastFootStep;

        private void Update()
        {
            if (lastFootStepCD + lastFootStep < Time.time && cm.velocity.magnitude > 0 && footAudioSource.isPlaying == false)
            {
                lastFootStep = Time.time;
                playFootSound();
            }
            
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;

            if (newGameState == GameState.Gameplay)
            {
                footAudioSource.UnPause();
            }
            else
            {
                footAudioSource.Pause();
            }

        }

        private int lastPlayedFootWood;
        private int lastPlayedFootDirt;
        private bool isOnDirt;

        private void playFootSound()
        {
            if ( isOnDirt == true )
            {
                footAudioSource.clip = dirtFootClips[dirtFoodClipsIndexes[lastPlayedFootDirt]];
                lastPlayedFootDirt++;
                if (lastPlayedFootDirt >= dirtFoodClipsIndexes.Count)
                {
                    lastPlayedFootDirt = 0;
                    suffleIndexes(dirtFoodClipsIndexes);
                }
            }
            else
            {
                footAudioSource.clip = woodFootClips[woodFoodClipsIndexes[ lastPlayedFootWood ]];
                lastPlayedFootWood++;
                if (lastPlayedFootWood >= woodFoodClipsIndexes.Count)
                {
                    lastPlayedFootWood = 0;
                    suffleIndexes(woodFoodClipsIndexes);
                }
            }   
            footAudioSource.Play();
        }

        private void suffleIndexes( List<int> list )
        {
            for (int i = 0; i < list.Count; i++)
            {
                int temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        public void updateGroundStatus(bool isOnDirtI)
        {
            isOnDirt = isOnDirtI;
        }

        #endregion
    }
}