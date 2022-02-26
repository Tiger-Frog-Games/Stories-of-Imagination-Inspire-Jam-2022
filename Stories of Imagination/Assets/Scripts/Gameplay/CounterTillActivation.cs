using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoriesofImagination
{
    public class CounterTillActivation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private UnityEvent OnAllActivated;
        [SerializeField] private EventChannelSOStory OnStoryEnd;

        [SerializeField] private List<StorySO> storiesToLisenTo;
        private List<StorySO> storiesLeft;
        private List<StorySO> currentStoriesHeard;

        [SerializeField] private SingleLineSender lineSender;

        [SerializeField] private string CatMsgOfAllStoriesCompleted;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnStoryEnd.OnEvent += OnStoryEnd_OnEvent;
            currentStoriesHeard = new List<StorySO>();
            storiesLeft = storiesToLisenTo;
        }

        private void OnDestroy()
        {
            OnStoryEnd.OnEvent -= OnStoryEnd_OnEvent;
        }

        #endregion

        #region Methods
        private void OnStoryEnd_OnEvent(StorySO story)
        {
            if (currentStoriesHeard.Contains(story) || ! storiesToLisenTo.Contains(story))
            {
                return;
            }
            storiesLeft.Remove(story);
            currentStoriesHeard.Add(story);

            //Used for the cat to change his lines
            if (lineSender != null)
            {
                
                if (storiesLeft.Count == 0)
                {
                    lineSender.changeLine(CatMsgOfAllStoriesCompleted);
                }
                else
                {
                    string outMsg = "";
                    for (int i = 0; i < storiesLeft.Count; i++)
                    {
                        outMsg += storiesLeft[i].name;
                        if (i == storiesLeft.Count - 2)
                        {
                            outMsg += " and ";
                        }
                        else if(i != storiesLeft.Count - 1)
                        {
                            outMsg += ", ";
                        }
                    }

                    lineSender.changeLine($"You still have to lisen to {outMsg}");
                }
            }

            if (storiesToLisenTo.Count == 0)
            {
                OnAllActivated.Invoke();
            }
        }
        #endregion
    }
}