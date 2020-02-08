using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExtremeBalls
{
    public class BallShop : MonoBehaviour
    {
        public GoogleAds googleAds;
        public TextMeshPro ShopCoins;
        public TextMeshPro CoinsError;
        public TextMeshPro Ballinfo;
        public GameObject BallScroll;
        public GameObject VideoForCoins;
        public GameObject ColorPicker;
        public GameObject BuyorSelect;
        public UIColorPicker colorPicker;
        private Color currentColor;
        [Space(20)]

        //BuyButtons\\
        public GameObject BuyB3;
        public GameObject BuyB4;
        public GameObject BuyB5;
        public GameObject BuyB6;
        public GameObject BuyB7;
        public GameObject BuyB8;
        [Space(20)]

        //SelectButtons\\
        public GameObject SelectB1;
        public GameObject SelectB2;
        public GameObject SelectB3;
        public GameObject SelectB4;
        public GameObject SelectB5;
        public GameObject SelectB6;
        public GameObject SelectB7;
        public GameObject SelectB8;
        [Space(20)]

        public GameObject ActiveCheck;

        private void Start()
        {
            PlayerPrefs.GetInt("Unlocked Ball 3", 0);
            CoinsError.gameObject.SetActive(false);
            ActiveLocation();
            LoadColor();
        }
        #region ActiveCheck
        private void ActiveLocation()
        {
            if(PlayerPrefs.GetInt("Selected Ball") == 1)
            {
                ActiveCheck.transform.position = new Vector3(SelectB1.transform.position.x, SelectB1.transform.position.y);
                ActiveCheck.transform.parent = SelectB1.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 2)
            {
                ActiveCheck.transform.position = new Vector3(SelectB2.transform.position.x, SelectB2.transform.position.y);
                ActiveCheck.transform.parent = SelectB2.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 3)
            {
                ActiveCheck.transform.position = new Vector3(SelectB3.transform.position.x, SelectB3.transform.position.y);
                ActiveCheck.transform.parent = SelectB3.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 4)
            {
                ActiveCheck.transform.position = new Vector3(SelectB4.transform.position.x, SelectB4.transform.position.y);
                ActiveCheck.transform.parent = SelectB4.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 5)
            {
                ActiveCheck.transform.position = new Vector3(SelectB5.transform.position.x, SelectB5.transform.position.y);
                ActiveCheck.transform.parent = SelectB5.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 6)
            {
                ActiveCheck.transform.position = new Vector3(SelectB6.transform.position.x, SelectB6.transform.position.y);
                ActiveCheck.transform.parent = SelectB6.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 7)
            {
                ActiveCheck.transform.position = new Vector3(SelectB7.transform.position.x, SelectB7.transform.position.y);
                ActiveCheck.transform.parent = SelectB7.transform;
            }

            if (PlayerPrefs.GetInt("Selected Ball") == 8)
            {
                ActiveCheck.transform.position = new Vector3(SelectB8.transform.position.x, SelectB8.transform.position.y);
                ActiveCheck.transform.parent = SelectB8.transform;
            }
        }
        #endregion

        #region UpdatePlayerPrefs
        private void Update()
        {
            ShopCoins.SetText(PlayerPrefs.GetInt("Your Coins", 0).ToString());

            if (PlayerPrefs.GetInt("Unlocked Ball 3") == 1)
            {
                BuyB3.GetComponent<Image>().color = currentColor;
            }

            if (PlayerPrefs.GetInt("Bought Ball 1") == 0)
            {
                SelectB1.SetActive(true);
            }
            if(PlayerPrefs.GetInt("Bought Ball 1") == 1)
            {
                SelectB1.SetActive(true);
            }
            //\\

            if (PlayerPrefs.GetInt("Bought Ball 4") == 0)
            {
                BuyB4.SetActive(true);
                SelectB4.SetActive(false);
            }
            if (PlayerPrefs.GetInt("Bought Ball 4") == 1)
            {
                BuyB4.SetActive(false);
                SelectB4.SetActive(true);
            }
            //\\

            if (PlayerPrefs.GetInt("Bought Ball 5") == 0)
            {
                BuyB5.SetActive(true);
                SelectB5.SetActive(false);
            }
            if (PlayerPrefs.GetInt("Bought Ball 5") == 1)
            {
                BuyB5.SetActive(false);
                SelectB5.SetActive(true);
            }

            if (PlayerPrefs.GetInt("Bought Ball 6") == 0)
            {
                BuyB6.SetActive(true);
                SelectB6.SetActive(false);
            }
            if (PlayerPrefs.GetInt("Bought Ball 6") == 1)
            {
                BuyB6.SetActive(false);
                SelectB6.SetActive(true);
            }
            //\\
            if (PlayerPrefs.GetInt("Bought Ball 7") == 0)
            {
                BuyB7.SetActive(true);
                SelectB7.SetActive(false);
            }
            if (PlayerPrefs.GetInt("Bought Ball 7") == 1)
            {
                BuyB7.SetActive(false);
                SelectB7.SetActive(true);
            }
            //\\
            if (PlayerPrefs.GetInt("Bought Ball 8") == 0)
            {
                BuyB8.SetActive(true);
                SelectB8.SetActive(false);
            }
            if (PlayerPrefs.GetInt("Bought Ball 8") == 1)
            {
                BuyB8.SetActive(false);
                SelectB8.SetActive(true);
            }

            if (PlayerPrefs.GetInt("Random Ball") == 1)
            {
                ActiveCheck.transform.position = new Vector3(SelectB2.transform.position.x, SelectB2.transform.position.y);
                ActiveCheck.transform.parent = SelectB2.transform;
            }
        }
        #endregion

        #region BuyBalls
        public void AddCoins()
        {
            int coins = PlayerPrefs.GetInt("Your Coins");
            PlayerPrefs.SetInt("Your Coins", coins + 200);
        }

        public void BuyBall1()
        {
            //Ball 1 is Free
        }
//\\
        public void BuyBall2()
        {
            //Random Ball Picked
        }
//\\
        public void BuyBall3()
        {
                BallScroll.SetActive(false);
                VideoForCoins.SetActive(false);
                BuyorSelect.SetActive(true);
        }
        public void BuyBall4()
        {
            if (PlayerPrefs.GetInt("Your Coins") >= 200)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                PlayerPrefs.SetInt("Your Coins", coins - 200);
                PlayerPrefs.SetInt("Bought Ball 4", 1);
                SelectBall4();
            }
            else
            {
                StartCoroutine(CoinError());
            }
        }
        public void BuyBall5()
        {
            if (PlayerPrefs.GetInt("Your Coins") >= 300)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                PlayerPrefs.SetInt("Your Coins", coins - 300);
                PlayerPrefs.SetInt("Bought Ball 5", 1);
                SelectBall5();
            }
            else
            {
                StartCoroutine(CoinError());
            }
        }

        public void BuyBall6()
        {
            if (PlayerPrefs.GetInt("Your Coins") >= 200)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                PlayerPrefs.SetInt("Your Coins", coins - 200);
                PlayerPrefs.SetInt("Bought Ball 6", 1);
                SelectBall6();
            }
            else
            {
                StartCoroutine(CoinError());
            }
        }

        public void BuyBall7()
        {
            if (PlayerPrefs.GetInt("Your Coins") >= 200)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                PlayerPrefs.SetInt("Your Coins", coins - 200);
                PlayerPrefs.SetInt("Bought Ball 7", 1);
                SelectBall7();
            }
            else
            {
                StartCoroutine(CoinError());
            }
        }

        public void BuyBall8()
        {
            if (PlayerPrefs.GetInt("Your Coins") >= 200)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                PlayerPrefs.SetInt("Your Coins", coins - 200);
                PlayerPrefs.SetInt("Bought Ball 8", 1);
                SelectBall8();
            }
            else
            {
                StartCoroutine(CoinError());
            }
        }
        #endregion

        #region SelectBalls
        public void SelectBall1()
        {
            PlayerPrefs.SetInt("Selected Ball", 1);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB1.transform.position.x, SelectB1.transform.position.y);
            ActiveCheck.transform.parent = SelectB1.transform;
        }
//\\
        public void SelectBall2()
        {
            PlayerPrefs.SetInt("Random Ball", 1);
            float random;
            random = random = UnityEngine.Random.Range(1, 8);
            if(random == 1)
            {
                PlayerPrefs.SetInt("Selected Ball", 1);
            }
            if (random == 2)
            {
                if (PlayerPrefs.GetInt("Unlocked Ball 3") == 1)
                {
                    PlayerPrefs.SetInt("Selected Ball", 3);
                }
                else
                    PlayerPrefs.SetInt("Selected Ball", 1);
            }
            if (random == 3)
            {
                if (PlayerPrefs.GetInt("Bought Ball 4") == 1)
                {
                    PlayerPrefs.SetInt("Selected Ball", 4);
                }
                else
                    PlayerPrefs.SetInt("Selected Ball", 1);
            }
            if (random == 4)
            {
                if (PlayerPrefs.GetInt("Bought Ball 5") == 1)
                {
                    PlayerPrefs.SetInt("Selected Ball", 5);
                }
                else
                    PlayerPrefs.SetInt("Selected Ball", 1);
            }
            if (random == 5)
            {
                if (PlayerPrefs.GetInt("Bought Ball 6") == 1)
                {
                    PlayerPrefs.SetInt("Selected Ball", 6);
                }
                else
                    PlayerPrefs.SetInt("Selected Ball", 1);
            }
            if (random == 6)
            {
                if (PlayerPrefs.GetInt("Bought Ball 7") == 1)
                {
                    PlayerPrefs.SetInt("Selected Ball", 7);
                }
                else
                    PlayerPrefs.SetInt("Selected Ball", 1);
            }
            if (random == 7)
            {
                if (PlayerPrefs.GetInt("Bought Ball 8") == 1)
                {
                    PlayerPrefs.SetInt("Selected Ball", 8);
                }
                else
                    PlayerPrefs.SetInt("Selected Ball", 1);
            }
        }
//\\
        public void SelectBall3()
        {
            SelectB3.SetActive(true);
            PlayerPrefs.SetInt("Selected Ball", 3);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB3.transform.position.x, SelectB3.transform.position.y);
            ActiveCheck.transform.parent = SelectB3.transform;
        }

        public void SelectBall4()
        {
            PlayerPrefs.SetInt("Selected Ball", 4);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB4.transform.position.x, SelectB4.transform.position.y);
            ActiveCheck.transform.parent = SelectB4.transform;
        }

        public void SelectBall5()
        {
            PlayerPrefs.SetInt("Selected Ball", 5);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB5.transform.position.x, SelectB5.transform.position.y);
            ActiveCheck.transform.parent = SelectB5.transform;
        }

        public void SelectBall6()
        {
            PlayerPrefs.SetInt("Selected Ball", 6);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB6.transform.position.x, SelectB6.transform.position.y);
            ActiveCheck.transform.parent = SelectB6.transform;
        }

        public void SelectBall7()
        {
            PlayerPrefs.SetInt("Selected Ball", 7);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB7.transform.position.x, SelectB7.transform.position.y);
            ActiveCheck.transform.parent = SelectB7.transform;
        }

        public void SelectBall8()
        {
            PlayerPrefs.SetInt("Selected Ball", 8);
            PlayerPrefs.SetInt("Random Ball", 0);
            ActiveCheck.transform.position = new Vector3(SelectB8.transform.position.x, SelectB8.transform.position.y);
            ActiveCheck.transform.parent = SelectB8.transform;
        }

        #endregion

        public void OnBallInfoClicked()
        {
            StartCoroutine(BallInfo());
        }

        public void OnBuyColor()
        {
            BuyorSelect.SetActive(false);
            ColorPicker.SetActive(true);
        }
        public void OnSelectBall()
        {
            if (PlayerPrefs.GetInt("Unlocked Ball 3") == 0)
            {
                StartCoroutine(UnlockBall3());
                BuyorSelect.SetActive(false);
                BallScroll.SetActive(true);
                VideoForCoins.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Unlocked Ball 3") == 1)
            {
                BuyorSelect.SetActive(false);
                BallScroll.SetActive(true);
                VideoForCoins.SetActive(true);
                SelectBall3();
            }
        }

        public void VideoForCoinsAd()
        {
            googleAds.ShowRewardVideo();
        }

        #region Utils
        //Color Picker Buttons\\
        public void BuyColor()
        {
            if (PlayerPrefs.GetInt("Your Coins") >= 25)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                PlayerPrefs.SetInt("Your Coins", coins - 25);
                PlayerPrefs.SetInt("Selected Ball", 3);
                PlayerPrefs.SetInt("Unlocked Ball 3", 1);

                BuyorSelect.SetActive(false);
                ColorPicker.SetActive(false);
                BallScroll.SetActive(true);
                VideoForCoins.SetActive(true);

                LoadColor();

                BuyB3.GetComponent<Image>().color = currentColor;
                SelectBall3();
            }
            else
            {
                StartCoroutine(CoinError());
            }
        }     

        public void NoColor()
        {
            BuyorSelect.SetActive(false);
            ColorPicker.SetActive(false);
            BallScroll.SetActive(true);
            VideoForCoins.SetActive(true);
        }

        private IEnumerator BallInfo()
        {
            Ballinfo.gameObject.SetActive(true);
            Ballinfo.SetText("Use Coins to Purchase any color you want! Purchase unlimited times!");
            yield return new WaitForSeconds(3f);
            Ballinfo.gameObject.SetActive(false);
        }

        private IEnumerator CoinError()
        {
            CoinsError.gameObject.SetActive(true);
            CoinsError.SetText("NOT ENOUGH COINS TO BUY BALL");
            yield return new WaitForSeconds(1f);
            CoinsError.gameObject.SetActive(false);
        }
        
        private IEnumerator UnlockBall3()
        {
            Ballinfo.gameObject.SetActive(true);
            Ballinfo.SetText("YOU MUST UNLOCK BALL BEFORE SELECTING");
            yield return new WaitForSeconds(2f);
            Ballinfo.gameObject.SetActive(false);
        }

        Color LoadColor()
        {
            float r = PlayerPrefs.GetFloat("red");
            float b = PlayerPrefs.GetFloat("green");
            float g = PlayerPrefs.GetFloat("blue");
            Color col = new Color(r, b, g);
            currentColor = col;
            return col;
        }
        #endregion
    }
}
