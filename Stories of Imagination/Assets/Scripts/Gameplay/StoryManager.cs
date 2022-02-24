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
        [SerializeField] private EventChannelSO OnStoryEnd;

        [SerializeField] private EventChannelStoryLine OnStoryReadLine;


        //not the biggest fan of this but it works for a jam game. 
        [SerializeField] private StoryLine[] storyGrandma;
        [SerializeField] private StoryLine[] storyRock;
        [SerializeField] private StoryLine[] storyCat;
        [SerializeField] private StoryLine[] storyTree;
        #endregion
        private StoryLine[] currentStory;

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
        private void OnStoryStart_OnEvent(STORYTYPE obj)
        {
            switch( obj)
            {
                case STORYTYPE.GRANDMA_ONE:
                    currentStory = storyGrandma;
                    break;
                default:
                    currentStory = null;
                    break;
            }

            if (currentStory != null)
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


        private IEnumerator storyTeller_Coroutine;
        private IEnumerator storyTeller()
        {
            for (int i = 0; i < currentStory.Length; i++)
            {
                if (currentStory.Length <= 1)
                {
                    OnStoryReadLine.RaiseEvent(currentStory[i].getReader(), currentStory[i].getLine(), i, 1);
                }
                else
                {
                    OnStoryReadLine.RaiseEvent(currentStory[i].getReader(), currentStory[i].getLine(), i, i / (currentStory.Length - 1));
                }

                yield return new WaitForSeconds( currentStory[i].getTimeToRead() );
            }
            OnStoryEnd.RaiseEvent();
        }


        #endregion
    }
}