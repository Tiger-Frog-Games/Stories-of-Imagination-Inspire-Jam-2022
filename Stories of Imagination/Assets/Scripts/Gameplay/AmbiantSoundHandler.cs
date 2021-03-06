using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class AmbiantSoundHandler : MonoBehaviour
    {
        #region Variables

        //private List<AudioSource> allAmbientSounds;
        [SerializeField] private float fadeInTime = 4;

        [SerializeField] private List<AudioSource> insideAmbientSounds;
        [SerializeField] private List<AudioSource> outsideAmbientSounds;

        [SerializeField] private AudioSource outsideOne;
        [SerializeField] private AudioSource outsideTwo;

        [SerializeField] private EventChannelSO OnGameEnding;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

            ambientSound_Coroutine = new Dictionary<AudioSource, IEnumerator>();

            OnGameEnding.OnEvent += OnGameEnding_OnEvent;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;

            OnGameEnding.OnEvent -= OnGameEnding_OnEvent;
        }

        public void addSound(AudioSource audioIn)
        {
            //allAmbientSounds.Add(audioIn);
            StartCoroutine( audioFadeIn(audioIn));
        }

        public void removeSound(AudioSource audioIn)
        {
            //allAmbientSounds.Add(audioIn);
            StartCoroutine(audioFadeOut(audioIn));
        }

        public void addOutSideSound(AudioSource audioIn)
        {
            outsideAmbientSounds.Add(audioIn);
            if(isOutside == true)
            {
                addSound(audioIn);
            }
        }

        public void removeOutsideSound(AudioSource audioIn)
        {
            if (outsideAmbientSounds.Contains(audioIn))
            {
                outsideAmbientSounds.Remove(audioIn);
            }
             
            StartCoroutine(audioFadeOut(audioIn));
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

        private bool isOutside = true;
        private Dictionary<AudioSource, IEnumerator> ambientSound_Coroutine;
        public void goOutside()
        {
            isOutside = true;
            if (isPlayingSoloBranch == true)
            {
                return;
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
            isOutside = false;
            if (isPlayingSoloBranch == true)
            {
                return;
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

        private bool isPlayingSoloBranch = false;
        public void playBranchMelodySolo(AudioSource soloMelody)
        {
            isPlayingSoloBranch = true;
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
            addSound(soloMelody);
        }

        public void donePlayingSoloBranch(AudioSource soloMelody)
        {
            isPlayingSoloBranch = false;
            if (isOutside)
            {
                goOutside();
            }
            else
            {
                goInside();
            }
            
            removeSound(soloMelody);
        }


        private bool didLisenToOutsideStory = false;
        public void OnLisenFirstOutsideStory()
        {
            if (didLisenToOutsideStory == true)
            {
                return;
            }

            removeOutsideSound(outsideOne);

            addOutSideSound(outsideTwo);
            
            didLisenToOutsideStory = true;
        }

        private void OnGameEnding_OnEvent()
        {


            foreach (AudioSource source in outsideAmbientSounds)
            {
                source.volume = 0;
            }

            foreach (AudioSource source in insideAmbientSounds)
            {
                source.volume = 0;
                
            }
        }

        #endregion
    }
}