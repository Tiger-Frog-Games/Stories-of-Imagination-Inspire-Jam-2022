using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class StoryLisener : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int lineToLisenFor;
        [SerializeField] private StorySO storyToLisenTo;

        [SerializeField] private EventChannelSOStory StartLiseningForStory;
        [SerializeField] private EventChannelSOStory StopLiseningForStory;

        [SerializeField] private EventChannelStoryLine lineLisener;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

            StartLiseningForStory.OnEvent += StartLiseningForStory_OnEvent;
            StopLiseningForStory.OnEvent += StopLiseningForStory_OnEvent;

            this.enabled = false;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;

            StartLiseningForStory.OnEvent -= StartLiseningForStory_OnEvent;
            StopLiseningForStory.OnEvent -= StopLiseningForStory_OnEvent;
        }

        private void OnEnable()
        {
            lineLisener.OnEvent += LineLisener_OnEvent;
        }

        private void OnDisable()
        {
            lineLisener.OnEvent -= LineLisener_OnEvent;
        }

        #endregion

        #region Methods

        private void StartLiseningForStory_OnEvent(StorySO obj)
        {
            if (obj == storyToLisenTo)
            {
                this.enabled = true;
            }
        }

        private void StopLiseningForStory_OnEvent(StorySO obj)
        {
            if (obj == storyToLisenTo)
            {
                this.enabled = false;
            }
        }



        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            //this.enabled = (newGameState == GameState.Gameplay);
            OnPause(newGameState == GameState.Gameplay);
        }

        virtual protected void OnPause(bool pauseState)
        {

        }

        private void LineLisener_OnEvent(StoryLine storyLine, int storyLinelineNumber, float percentageComplete)
        {
            onLineRead(percentageComplete);

            if (storyLinelineNumber == lineToLisenFor)
            {
                onLineToLisenFor();
            }
        }

        virtual protected void onLineToLisenFor()
        {
            //print($"Lisened to the specic line{lineToLisenFor}");
        }

        virtual protected void onLineRead(float percentageComplete)
        {
            //print($"Lisened to line: {percentageComplete}");
        }

        #endregion
    }
}