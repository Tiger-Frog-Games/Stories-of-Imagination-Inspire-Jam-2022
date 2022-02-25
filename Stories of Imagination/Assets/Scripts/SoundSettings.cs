using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace StoriesofImagination
{
    public class SoundSettings : MonoBehaviour
    {
        #region Variables

        [SerializeField] private AudioMixer theMixer;

        [SerializeField] private TMP_Text mastLabel, musicLabel, sfxLabel;
        [SerializeField] private Slider mastSlider, musicSlider, sfxSlider;

        #endregion

        #region Unity Methods

        private void Start()
        {

            if (PlayerPrefs.HasKey("MasterVol"))
            {
                theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            }

            if (PlayerPrefs.HasKey("MusicVol"))
            {
                theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            }

            if (PlayerPrefs.HasKey("SFXVol"))
            {
                theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            }

            float vol = 0f;
            
            theMixer.GetFloat("MasterVol", out vol);
            mastSlider.value = vol;
            
            theMixer.GetFloat("MusicVol", out vol);
            musicSlider.value = vol;

            theMixer.GetFloat("SFXVol", out vol);
            sfxSlider.value = vol;

            mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();
            musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
            sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        }

        #endregion

        #region Methods

        public void SetMasterVol()
        {
            mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();

            theMixer.SetFloat("MasterVol", mastSlider.value);

            PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
        }

        public void SetMusicVol()
        {
            musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

            theMixer.SetFloat("MusicVol", musicSlider.value);

            PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        }

        public void SetSFXVol()
        {
            sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

            theMixer.SetFloat("SFXVol", sfxSlider.value);

            PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
        }

        #endregion
    }
}