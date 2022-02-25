using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public enum STORYTYPE { GRANDMA_ONE, CAT, DOG, TREE, ROCK }

    public class StoryManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private EventChannelSOStory OnStoryStart;
        [SerializeField] private EventChannelSOStory OnStoryEnd;

        [SerializeField] private EventChannelStoryLine OnStoryReadLine;

        private StorySO currentStory;
        private StoryLine[] currentStoryLines;
        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            OnStoryStart.OnEvent += OnStoryStart_OnEvent;
        }

        private void OnDisable()
        {
            OnStoryStart.OnEvent -= OnStoryStart_OnEvent;
        }
        #endregion

        #region Methods
        private void OnStoryStart_OnEvent(StorySO storyIn)
        {
            //should probally add a check to see if a story is being told

            currentStory = storyIn;
            currentStoryLines = storyIn.getStoryLines(); 

            if (currentStoryLines != null)
            {
                startTellingStory();
            }

        }

        private void startTellingStory()
        {
            if (storyTeller_Coroutine != null)
            {
                StopCoroutine(storyTeller_Coroutine);
            }
            storyTeller_Coroutine = storyTeller();

            
            StartCoroutine(storyTeller_Coroutine);
        }


        private float timeForCurrentLine;
        private IEnumerator storyTeller_Coroutine;
        private IEnumerator storyTeller()
        {
            // Other liseners to OnStory start need a frame to set up
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < currentStoryLines.Length; i++)
            {
                if (currentStoryLines.Length <= 1)
                {
                    OnStoryReadLine.RaiseEvent(currentStoryLines[i], i, 1);
                }
                else
                {
                    OnStoryReadLine.RaiseEvent(currentStoryLines[i], i, (float) i / (currentStoryLines.Length - 1));
                }

                //if the game is paused delay the next line

                timeForCurrentLine = 0;
                while (timeForCurrentLine < currentStoryLines[i].getTimeToRead())
                {
                    while (GameStateManager.Instance.CurrentGameState == GameState.Paused)
                    {
                        yield return new WaitForSeconds(.1f);
                    }

                    yield return new WaitForSeconds(.1f);
                    timeForCurrentLine += .1f;
                }
            }

            OnStoryEnd.RaiseEvent(currentStory);
        }


        #endregion
    }
}