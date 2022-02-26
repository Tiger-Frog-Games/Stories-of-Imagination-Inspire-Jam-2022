using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StoriesofImagination
{
    public class StorySubtitles : MonoBehaviour
    {
        #region Variables
        private int currentLine = 0;
        [SerializeField] private TMP_Text lineOne;
        [SerializeField] private TMP_Text lineTwo;

        [SerializeField] private TMP_Text readerOne;
        [SerializeField] private TMP_Text readerTwo;

        [SerializeField] private Animator subTitlesAnimator;

        #endregion

        [SerializeField] private EventChannelSOStory OnNewStory;
        [SerializeField] private EventChannelStoryLine nextLine;
        [SerializeField] private EventChannelSOStory OnStoryEnd;

        [SerializeField] private EventChannelStoryLine singleLine;

        #region Unity Methods
        private void OnEnable()
        {
            nextLine.OnEvent += NextLine_OnEvent;
            OnStoryEnd.OnEvent += OnStoryEnd_OnEvent;
            OnNewStory.OnEvent += OnNewStory_OnEvent;

            singleLine.OnEvent += SingleLine_OnEvent;
        }

        private void OnDisable()
        {
            nextLine.OnEvent -= NextLine_OnEvent;
            OnStoryEnd.OnEvent -= OnStoryEnd_OnEvent;
            OnNewStory.OnEvent -= OnNewStory_OnEvent;

            singleLine.OnEvent -= SingleLine_OnEvent;
        }

        #endregion

        #region Methods

        private void OnNewStory_OnEvent(StorySO story)
        {
            if (hider_Coroutine != null)
            {
                StopCoroutine(hider_Coroutine);
            }

            isFirstDialog = true;
            subTitlesAnimator.SetTrigger("New Story");
            subTitlesAnimator.ResetTrigger("Next Line");
        }

        private void OnStoryEnd_OnEvent(StorySO story)
        {
            lineOne.text = "";
            readerOne.text = "";

            lineTwo.text = "";
            readerTwo.text = "";
        }

        private bool isFirstDialog;

        private void NextLine_OnEvent(StoryLine storyLine, int lineNumber,float percentageComplete)
        {

            if (currentLine == 0)
            {
                lineOne.text = storyLine.getLine();
                readerOne.text = $"{storyLine.getReader()} :";
                currentLine = 1;
            }
            else
            {
                lineTwo.text = storyLine.getLine();
                readerTwo.text = $"{storyLine.getReader()} :";
                currentLine = 0;
            }

            if (isFirstDialog == false)
            {
                subTitlesAnimator.SetTrigger("Next Line");
            }
            else
            {
                isFirstDialog = false;
            }
            
        }

        private void SingleLine_OnEvent(StoryLine storyLine, int lineNumber, float percentComplete)
        {
            isFirstDialog = true;
            subTitlesAnimator.SetTrigger("New Story");
            subTitlesAnimator.ResetTrigger("Next Line");

            lineOne.text = storyLine.getLine();
            readerOne.text = $"{storyLine.getReader()} :";

            singleLineHider(storyLine.getTimeToRead());
        }

        private void singleLineHider(float timeIn)
        {
            if (hider_Coroutine != null)
            {
                StopCoroutine(hider_Coroutine);
            }
            hider_Coroutine = Linehider(timeIn);


            StartCoroutine(hider_Coroutine);
        }

        private IEnumerator hider_Coroutine;
        private IEnumerator Linehider(float timeIn)
        {
            yield return new WaitForSeconds(timeIn);
            subTitlesAnimator.SetTrigger("Next Line");
        }
            #endregion
        }
}