using System;
using UnityEngine;
using TMPro;

namespace ExtremeBalls
{
    public class ScoresAndCoins : MonoBehaviour
    {
        public static ScoresAndCoins SCM;

        public static Action onCoinAdded;
        public TextMeshPro currentScore;
        public TextMeshPro bestScore;
        public TextMeshPro coins;
        public int coinPlus;
        public int Level;

        public void Start()
        {
            SCM = this;
            bestScore.SetText(PlayerPrefs.GetInt("Best Score", 0).ToString());
            coins.SetText(PlayerPrefs.GetInt("Your Coins", 0).ToString());
            coinPlus = PlayerPrefs.GetInt("Your Coins");
        }


        private void Update()
        {
            //PlayerPrefs.DeleteKey("Your Coins");
            currentScore.SetText(Level.ToString());

            if (Level > PlayerPrefs.GetInt("Best Score", 0))
            {
                PlayerPrefs.SetInt("Best Score", Level);
                bestScore.SetText(Level.ToString());
            }
        }

        public void AddCoins()
        {
            coinPlus++;
            PlayerPrefs.SetInt("Your Coins", coinPlus);
            coins.SetText(PlayerPrefs.GetInt("Your Coins").ToString());
        }
    }
}
