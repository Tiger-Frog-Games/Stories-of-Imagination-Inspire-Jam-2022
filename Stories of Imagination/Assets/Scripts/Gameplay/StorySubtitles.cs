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

        #region Unity Methods
        private void OnEnable()
        {
            nextLine.OnEvent += NextLine_OnEvent;
            OnStoryEnd.OnEvent += OnStoryEnd_OnEvent;
            OnNewStory.OnEvent += OnNewStory_OnEvent;
        }

        private void OnDisable()
        {
            nextLine.OnEvent -= NextLine_OnEvent;
            OnStoryEnd.OnEvent -= OnStoryEnd_OnEvent;
            OnNewStory.OnEvent -= OnNewStory_OnEvent;
        }

        #endregion

        #region Methods

        private void OnNewStory_OnEvent(StorySO story)
        {
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

        private void NextLine_OnEvent(LINEREADERS reader, string line, int lineNumber, float percentageComplete)
        {

            if (currentLine == 0)
            {
                lineOne.text = line;
                readerOne.text = $"{reader} :";
                currentLine = 1;
            }
            else
            {
                lineTwo.text = line;
                readerTwo.text = $"{reader} :";
                currentLine = 0;
            }
            subTitlesAnimator.SetTrigger("Next Line");
        }

        #endregion
    }
}