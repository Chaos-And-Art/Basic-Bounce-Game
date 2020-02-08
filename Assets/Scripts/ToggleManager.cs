using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExtremeBalls
{
    public class ToggleManager : MonoBehaviour
    {
        public Color onColor;
        public Color offColor;

        public GameObject Toggle10;
        public GameObject Toggle25;
        public GameObject Toggle50;
        [Space(20)]

        public GameObject ToggleSwitchOn10;
        public GameObject ToggleSwitchOff10;
        public GameObject ToggleSwitchOn25;
        public GameObject ToggleSwitchOff25;
        public GameObject ToggleSwitchOn50;
        public GameObject ToggleSwitchOff50;
        [Space(20)]

        public GameObject Lock10B;
        public GameObject Lock25B;
        public GameObject Lock50B;
        public TextMeshPro LockedMessage;
        [Space(20)]

        public bool TBalls = false;
        public bool TFiveBalls = false;
        public bool FBalls = false;

        public bool Unlock10 = false;
        public bool Unlock25 = false;
        public bool Unlock50 = false;


        private void Update()
        {
            if (PlayerPrefs.GetInt("Best Score", 0) > 25)
            {
                UnLock10B();
            }

            if(PlayerPrefs.GetInt("Best Score", 0) > 50)
            {
                Unlock25B();
            }

            if(PlayerPrefs.GetInt("Best Score", 0) > 100)
            {
                Unlock50B();
            }

            if(PlayerPrefs.GetInt("10 Balls")== 1)
            {
                On10BallsToggleOn();
            }
            else if(PlayerPrefs.GetInt("10 Balls") == 0){
                On10BallsToggleOff();
            }

            if (PlayerPrefs.GetInt("25 Balls") == 1)
            {
                On25BallsToggleOn();
            }
            else if (PlayerPrefs.GetInt("25 Balls") == 0)
            {
                On25BallsToggleOff();
            }

            if (PlayerPrefs.GetInt("50 Balls") == 1)
            {
                On50BallsToggleOn();
            }
            else if (PlayerPrefs.GetInt("50 Balls") == 0)
            {
                On50BallsToggleOff();
            }
        }

        private void UnLock10B()
        {
            Unlock10 = true;
            Lock10B.SetActive(false);
        }

        private void Unlock25B()
        {
            Unlock25 = true;
            Lock25B.SetActive(false);
        }

        private void Unlock50B()
        {
            Unlock50 = true;
            Lock50B.SetActive(false);
        }

        public void On10BallsToggleOn()
        {
            if(Unlock10 == true)
            {

                PlayerPrefs.SetInt("10 Balls", 1);
                ToggleSwitchOn10.SetActive(false);
                ToggleSwitchOff10.SetActive(true);
                Toggle10.GetComponent<Image>().color = onColor;
                On25BallsToggleOff();
                On50BallsToggleOff();
            }
            else
            {
                StartCoroutine(LockedMessage10());               
            }
        }

        public void On10BallsToggleOff()
        {
            PlayerPrefs.SetInt("10 Balls", 0);
            ToggleSwitchOff10.SetActive(false);
            ToggleSwitchOn10.SetActive(true);
            Toggle10.GetComponent<Image>().color = offColor;
        }

        public void On25BallsToggleOn()
        {
            if(Unlock25 == true)
            {
                PlayerPrefs.SetInt("25 Balls", 1);
                ToggleSwitchOn25.SetActive(false);
                ToggleSwitchOff25.SetActive(true);
                Toggle25.GetComponent<Image>().color = onColor;
                On10BallsToggleOff();
                On50BallsToggleOff();
            }
            else
            {
                StartCoroutine(LockedMessage25());
            }
        }

        public void On25BallsToggleOff()
        {
            PlayerPrefs.SetInt("25 Balls", 0);
            ToggleSwitchOff25.SetActive(false);
            ToggleSwitchOn25.SetActive(true);
            Toggle25.GetComponent<Image>().color = offColor;
        }

        public void On50BallsToggleOn()
        {
            if(Unlock50 == true)
            {
                PlayerPrefs.SetInt("50 Balls", 1);
                ToggleSwitchOn50.SetActive(false);
                ToggleSwitchOff50.SetActive(true);
                Toggle50.GetComponent<Image>().color = onColor;
                On10BallsToggleOff();
                On25BallsToggleOff();
            }
            else
            {
                StartCoroutine(LockedMessage50());
            }
        }

        public void On50BallsToggleOff()
        {
            PlayerPrefs.SetInt("50 Balls", 0);
            ToggleSwitchOff50.SetActive(false);
            ToggleSwitchOn50.SetActive(true);
            Toggle50.GetComponent<Image>().color = offColor;
        }


        private IEnumerator LockedMessage10()
        {
            LockedMessage.gameObject.SetActive(true);
            LockedMessage.SetText("YOU MUST BEAT LEVEL 25 TO UNLOCK");
            yield return new WaitForSeconds(3f);
            LockedMessage.gameObject.SetActive(false);
        }

        private IEnumerator LockedMessage25()
        {
            LockedMessage.gameObject.SetActive(true);
            LockedMessage.SetText("YOU MUST BEAT LEVEL 50 TO UNLOCK");
            yield return new WaitForSeconds(3f);
            LockedMessage.gameObject.SetActive(false);
        }

        private IEnumerator LockedMessage50()
        {
            LockedMessage.gameObject.SetActive(true);
            LockedMessage.SetText("YOU MUST BEAT LEVEL 100 TO UNLOCK");
            yield return new WaitForSeconds(3f);
            LockedMessage.gameObject.SetActive(false);
        }

    }
}
