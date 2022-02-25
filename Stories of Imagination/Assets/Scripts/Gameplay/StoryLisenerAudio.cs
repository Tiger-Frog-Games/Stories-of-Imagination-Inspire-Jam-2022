using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class StoryLisenerAudio : StoryLisener
    {
        [SerializeField] AudioSource audioClip;
        private bool isCurrentlyPlaying;
        protected override void OnPause(bool gamePlayState)
        {
            base.OnPause(gamePlayState);


            if (gamePlayState == true)
            {
                audioClip.UnPause();
            }
            else
            {
                audioClip.Pause();
            }
        }

        protected override void onLineToLisenFor()
        {
            audioClip.Play();
        }
    }
}