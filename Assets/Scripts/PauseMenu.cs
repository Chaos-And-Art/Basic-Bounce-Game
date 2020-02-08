using UnityEngine;
using UnityEngine.UI;

namespace ExtremeBalls
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject SoundButton;
        public Sprite SoundOn;
        public Sprite SoundOff;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("Muted", 0) == 1)
            {
                SoundButton.GetComponent<Image>().sprite = SoundOff;
                AudioListener.volume = 0;
            }
            if (PlayerPrefs.GetInt("Muted", 0) == 0)
            {
                SoundButton.GetComponent<Image>().sprite = SoundOn;
                AudioListener.volume = 1;
            }
        }

        public void ToggleSoundButton()
        {
            if (PlayerPrefs.GetInt("Muted", 0) == 0)
            {
                PlayerPrefs.SetInt("Muted", 1);
                SoundButton.GetComponent<Image>().sprite = SoundOff;
                AudioListener.volume = 0;
            }
            else
            {
                PlayerPrefs.SetInt("Muted", 0);
                SoundButton.GetComponent<Image>().sprite = SoundOn;
                AudioListener.volume = 1;
            }
        }
    }
}
