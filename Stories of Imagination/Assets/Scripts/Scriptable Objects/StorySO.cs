using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    [CreateAssetMenu(menuName = "Story")]
    public class StorySO : ScriptableObject
    {
        [SerializeField] private StoryLine[] storyLines;

        public StoryLine[] getStoryLines()
        {
            return storyLines;
        }
    }
}