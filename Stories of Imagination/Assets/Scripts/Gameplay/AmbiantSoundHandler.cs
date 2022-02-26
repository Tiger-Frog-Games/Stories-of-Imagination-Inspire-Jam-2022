using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class AmbiantSoundHandler : MonoBehaviour
    {
        #region Variables

        private List<AudioSource> allAmbientSounds;
        [SerializeField] private float fadeInTime = 4;

        [SerializeField] private List<AudioSource> insideAmbientSounds;
        [SerializeField] private List<AudioSource> outsideAmbientSounds;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

            allAmbientSounds = new List<AudioSource>();
            ambientSound_Coroutine = new Dictionary<AudioSource, IEnumerator>();
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        public void addAmbientSound(AudioSource audioIn)
        {
            allAmbientSounds.Add(audioIn);
            StartCoroutine( audioFadeIn(audioIn));
        }

        //private IEnumerator storyTeller_Coroutine;
        private IEnumerator audioFadeIn( AudioSource fadeInAudioSource )
        {
            float currentTime = fadeInAudioSource.volume * fadeInTime ;
            while (currentTime < fadeInTime)
            {
                fadeInAudioSource.volume = currentTime/fadeInTime;

                while (GameStateManager.Instance.CurrentGameState == GameState.Paused)
                {
                    yield return new WaitForSeconds(.1f);
                }

                yield return new WaitForSeconds(.1f);
                currentTime += .1f;
            }
            fadeInAudioSource.volume = 1;
        }

        private IEnumerator audioFadeOut(AudioSource fadeInAudioSource)
        {
            float currentTime = fadeInAudioSource.volume * fadeInTime;
            while (currentTime > 0)
            {
                fadeInAudioSource.volume = currentTime / fadeInTime;

                while (GameStateManager.Instance.CurrentGameState == GameState.Paused)
                {
                    yield return new WaitForSeconds(.1f);
                }

                yield return new WaitForSeconds(.1f);
                currentTime -= .1f;
            }
            fadeInAudioSource.volume = 0;
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private Dictionary<AudioSource, IEnumerator> ambientSound_Coroutine;
        public void goOutside()
        {
            foreach(AudioSource source in outsideAmbientSounds)
            {
                if (ambientSound_Coroutine.TryGetValue(source, out IEnumerator source_coroutine))
                {
                    if (source_coroutine != null)
                    {
                        StopCoroutine(source_coroutine);
                    }
                    ambientSound_Coroutine.Remove(source);
                }
                
                ambientSound_Coroutine.Add(source, audioFadeIn(source));
                StartCoroutine( ambientSound_Coroutine[source] );
            }

            foreach (AudioSource source in insideAmbientSounds)
            {
                if (ambientSound_Coroutine.TryGetValue(source, out IEnumerator source_coroutine))
                {
                    if (source_coroutine != null)
                    {
                        StopCoroutine(source_coroutine);
                    }
                    ambientSound_Coroutine.Remove(source);
                }
                ambientSound_Coroutine.Add(source, audioFadeOut(source));
                StartCoroutine(ambientSound_Coroutine[source]);
            }
        }

        public void goInside()
        {
            foreach (AudioSource source in insideAmbientSounds)
            {
                if (ambientSound_Coroutine.TryGetValue(source, out IEnumerator source_coroutine))
                {
                    if (source_coroutine != null)
                    {
                        StopCoroutine(source_coroutine);
                    }
                    ambientSound_Coroutine.Remove(source);
                }
                ambientSound_Coroutine.Add(source, audioFadeIn(source));
                StartCoroutine(ambientSound_Coroutine[source]);
            }

            foreach (AudioSource source in outsideAmbientSounds)
            {
                if (ambientSound_Coroutine.TryGetValue(source, out IEnumerator source_coroutine))
                {
                    if (source_coroutine != null)
                    {
                        StopCoroutine(source_coroutine);
                    }
                    ambientSound_Coroutine.Remove(source);
                }
                ambientSound_Coroutine.Add(source, audioFadeOut(source));
                StartCoroutine(ambientSound_Coroutine[source]);
            }
        }

        #endregion
    }
}