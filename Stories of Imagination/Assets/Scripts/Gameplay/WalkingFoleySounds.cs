using ECM2.Components;
using Micosmo.SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class WalkingFoleySounds : MonoBehaviour
    {
        #region Variables
        [SerializeField] RaySensor groundDetetor;

        [SerializeField] private AudioSource footAudioSource;
        [SerializeField] private CharacterMovement cm;

        [SerializeField] private AudioClip[] dirtFootClips;
        private List<int> dirtFootClipsIndexes;

        [SerializeField] private AudioClip[] woodFootClips;
        private List<int> woodFootClipsIndexes;

        [SerializeField] private AudioClip[] carpetFootClips;
        private List<int> carpetFootClipsIndexes;

        [SerializeField] private AudioClip[] linoniumFootClips;
        private List<int> linoniumFootClipsIndexes;

        [SerializeField] private float lastFootStepCD;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

            dirtFootClipsIndexes = new List<int>();
            woodFootClipsIndexes = new List<int>();
            carpetFootClipsIndexes = new List<int>();
            linoniumFootClipsIndexes = new List<int>();

            for (int i = 0; i < dirtFootClips.Length; i++)
            {
                dirtFootClipsIndexes.Add(i);
            }

            for (int i = 0; i < woodFootClips.Length; i++)
            {
                woodFootClipsIndexes.Add(i);
            }

            for (int i = 0; i < carpetFootClips.Length; i++)
            {
                carpetFootClipsIndexes.Add(i);
            }

            for (int i = 0; i < linoniumFootClips.Length; i++)
            {
                linoniumFootClipsIndexes.Add(i);
            }

            suffleIndexes(dirtFootClipsIndexes);
            suffleIndexes(woodFootClipsIndexes);
            suffleIndexes(carpetFootClipsIndexes);
            suffleIndexes(linoniumFootClipsIndexes);

            lastFootStep = int.MinValue;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }


        private float lastFootStep;

        private void Update()
        {
            if (lastFootStepCD + lastFootStep < Time.time && cm.velocity.magnitude > .1 && footAudioSource.isPlaying == false)
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
        private int lastPlayedFootCarpet;
        private int lastPlayedFootLinonium;
        private bool isOnDirt;
        private List<GameObject> storedDetetcions;
        private void playFootSound()
        {
            groundDetetor.Pulse();

            storedDetetcions = groundDetetor.GetDetectionsByDistance(); //groundDetetor.GetDetections();
            if (storedDetetcions == null || storedDetetcions.Count < 1 )
            {
                return;
            }
            //print(storedDetetcions[0].tag);

            if ( string.Equals(storedDetetcions[0].tag, "Dirt") )
            {
                footAudioSource.clip = dirtFootClips[dirtFootClipsIndexes[lastPlayedFootDirt]];
                lastPlayedFootDirt++;
                if (lastPlayedFootDirt >= dirtFootClipsIndexes.Count)
                {
                    lastPlayedFootDirt = 0;
                    suffleIndexes(dirtFootClipsIndexes);
                }
            }
            else if(string.Equals(storedDetetcions[0].tag, "Wood"))
            {
                footAudioSource.clip = woodFootClips[woodFootClipsIndexes[ lastPlayedFootWood ]];
                lastPlayedFootWood++;
                if (lastPlayedFootWood >= woodFootClipsIndexes.Count)
                {
                    lastPlayedFootWood = 0;
                    suffleIndexes(woodFootClipsIndexes);
                }
            }
            else if (string.Equals(storedDetetcions[0].tag, "Carpet"))
            {
                footAudioSource.clip = carpetFootClips[carpetFootClipsIndexes[lastPlayedFootCarpet]];
                lastPlayedFootCarpet++;
                if (lastPlayedFootCarpet >= carpetFootClipsIndexes.Count)
                {
                    lastPlayedFootCarpet = 0;
                    suffleIndexes(carpetFootClipsIndexes);
                }
            }
            else if (string.Equals(storedDetetcions[0].tag, "Linium"))
            {
                footAudioSource.clip = linoniumFootClips[linoniumFootClipsIndexes[lastPlayedFootLinonium]];
                lastPlayedFootLinonium++;
                if (lastPlayedFootLinonium >= linoniumFootClipsIndexes.Count)
                {
                    lastPlayedFootLinonium = 0;
                    suffleIndexes(linoniumFootClipsIndexes);
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