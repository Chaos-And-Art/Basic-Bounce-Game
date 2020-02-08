using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExtremeBalls
{
    public class UIManager : MonoBehaviour
    {
        public GoogleAds googleAds;
        //UI\\
        public GameObject MainMenu;
        public GameObject GamemodePanel;
        public GameObject ExtraButtons;
        public GameObject PlayGame;
        public GameObject GameOver;
        public GameObject PauseMenu;
        public GameObject Player;
        public GameObject BallCount;
        public GameObject GameManager;
        public GameObject SoundButton;
        public GameObject BallShopMenu;
        [Space(20)]

        //Actions\\
        public Action PlayButtonClicked;
        public Action RestartButtonClicked;
        public TextMeshPro yourScore;
        public TextMeshPro bestScore;
        public TextMeshPro coins;
        public TextMeshPro MainMenuCoins;
        public TextMeshPro GamemodeText;
        public bool PlayerLost = false;
        [Space(20)]

        public Sprite SoundOn;
        public Sprite SoundOff;

        private bool isDown = false;
        private float timer;


        private void Awake()
        {
            MainMenu.SetActive(true);
            ExtraButtons.SetActive(true);
            BallShopMenu.SetActive(false);
            PlayGame.SetActive(false);
            BallCount.SetActive(false);
            GameOver.SetActive(false);
            PauseMenu.SetActive(false);
            Player.SetActive(false);
            GamemodePanel.SetActive(false);
        }

        private void Start()
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

        private void Update()
        {
            MainMenuCoins.SetText(PlayerPrefs.GetInt("Your Coins", 0).ToString());

            if (PlayerLost == true)
            {
                yourScore.SetText("SCORE~ " + ScoresAndCoins.SCM.Level);
                bestScore.SetText("BEST~ " + PlayerPrefs.GetInt("Best Score", 0).ToString());
                coins.SetText(PlayerPrefs.GetInt("Your Coins", 0).ToString());
                PlayerLost = false;
            }
        }

        public void OnPlayButton()
        {
            if (PlayButtonClicked != null)
                PlayButtonClicked();

            MainMenu.SetActive(false);
            PlayGame.SetActive(true);
            Player.SetActive(true);
            googleAds.gamePlay = true;
        }

        public void OnPauseButton()
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }

        public void OnResumeButton()
        {
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);         
        }

        public void OnRestartButton()
        {
            if (RestartButtonClicked != null)
                RestartButtonClicked();
            Time.timeScale = 1f;
            GameOver.SetActive(false);
            PauseMenu.SetActive(false);
            PlayGame.SetActive(true);

        }

        public void OnMainMenuButton()
        {
            Time.timeScale = 1f;
            Player.SetActive(false);
            PauseMenu.SetActive(false);
            PlayGame.SetActive(false);
            MainMenu.SetActive(true);
            googleAds.gamePlay = false;
            SceneManager.LoadScene("Start");
        }

        public void ToggleSoundButton()
        {
            if(PlayerPrefs.GetInt("Muted", 0) == 0)
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

        public void OnShopButton()
        {
            MainMenu.SetActive(false);
            BallShopMenu.SetActive(true);
            googleAds.ballShop = true;
        }

        public void OnBackButton()
        {
            BallShopMenu.SetActive(false);
            MainMenu.SetActive(true);
            googleAds.ballShop = false;
            SceneManager.LoadScene("Start");
        }

        public void OnGamemodeButton()
        {
            if(isDown == false)
            {
                GamemodeText.SetText("GAMEMODES ▼");
                GamemodePanel.SetActive(true);
                ExtraButtons.SetActive(false);
                isDown = true;
            }
            else
            {
                GamemodeText.SetText("GAMEMODES ▲");
                GamemodePanel.SetActive(false);
                ExtraButtons.SetActive(true);
                isDown = false;
            }
        }
    }
}


