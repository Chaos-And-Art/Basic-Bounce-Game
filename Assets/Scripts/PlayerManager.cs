using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExtremeBalls
{
    public class PlayerManager : MonoBehaviour
    {
        public GameManager gameManager;
        public UIManager uiManager;
        public ToggleManager toggleManager;
        public GameObject SpeedUp;

        //Player\\
        public playerState currentPlayerState;
        public SpriteRenderer player;
        public Transform spawnPosition;
        public float speed;
        private Vector3 velocity;
        private Vector3 touchStartPosition;
        private Color currentColor;
        [Space(20)]

        //Ball\\
        public Ball theBall;
        public TextMeshPro ballCount;
        private List<Ball> balls;
        private int stoppedBalls = 0;
        private Transform ballContainer;
        [Space(20)]

        //Arrow\\      
        public GameObject dots;
        public float offsetRotation = 5f;
        public float numberOfDots = 15;
        private List<Transform> ArrowDots;
        private Transform dotContainer;

        //First Time\\
        public Transform HandIcon;
        private float count;
        public TextMeshPro Text1;

        #region UI
        void UIManager()
        {
            uiManager.PlayButtonClicked += OnPlayButtonClicked;
            uiManager.RestartButtonClicked += OnRestartButtonClicked;
        }

        public enum playerState
        {
            aim, fire, wait, move, endshot
        }

        private void OnPlayButtonClicked()
        {
            Start();
        }

        private void OnRestartButtonClicked()
        {
            GameRestart();
        }
        #endregion

        #region Start, BallType, Restart, Gamemodes

        public void Start()
        {
            LoadColor();
            UIManager();
            SetUpPlayer();
            SetUpArrow();
            SpeedUp.SetActive(false);
            player.gameObject.SetActive(true);
            ballCount.gameObject.SetActive(true);
            ballContainer = new GameObject("Ball Container").transform;
            ballCount.text = "x" + (1);
            balls = new List<Ball>();
            AddBall();

            CheckGamemodes();

            ballCount.text = "x" + balls.Count;
            HandIcon.gameObject.SetActive(true);
            Text1.gameObject.SetActive(true);
            currentPlayerState = playerState.aim;
        }

        private void SetUpPlayer()
        {
            if (PlayerPrefs.GetInt("Selected Ball") == 1)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/Circle");
                player.sprite = Sprite as Sprite;
                player.transform.localScale = new Vector3(30, 30);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 3)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/Circle");
                player.sprite = Sprite as Sprite;
                player.color = currentColor;
                player.transform.localScale = new Vector3(30f, 30f);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 4)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/icon_gear");
                player.sprite = Sprite as Sprite;
                player.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + .025f);
                player.transform.localScale = new Vector3(.72f, .72f);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 5)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/openCircle");
                player.sprite = Sprite as Sprite;
                player.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + .025f);
                player.transform.localScale = new Vector3(.3f, .3f);              
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 6)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/Star1");
                player.sprite = Sprite as Sprite;
                player.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + .035f);
                spawnPosition.position = new Vector3(player.transform.position.x, player.transform.position.y + .06f);
                player.transform.localScale = new Vector3(30, 30);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 7)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/Star2");
                player.sprite = Sprite as Sprite;
                player.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + .042f);
                spawnPosition.position = new Vector3(player.transform.position.x, player.transform.position.y + .08f);
                player.transform.localScale = new Vector3(30, 30);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 8)
            {
                var Sprite = Resources.Load<Sprite>("Sprites/Star3");
                player.sprite = Sprite as Sprite;
                player.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + .04f);
                spawnPosition.position = new Vector3(player.transform.position.x, player.transform.position.y + .06f);
                player.transform.localScale = new Vector3(28, 28);
            }
        }

        private void CheckGamemodes()
        {
            if(PlayerPrefs.GetInt("10 Balls") == 1)
            {
                for (int i = 0; i < 9; i++)
                {
                    AddBall();
                }
            }

            if(PlayerPrefs.GetInt("25 Balls") == 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    AddBall();
                }
            }

            if(PlayerPrefs.GetInt("50 Balls") == 1)
            {
                for (int i = 0; i < 49; i++)
                {
                    AddBall();
                }
            }
        }

        public void GameRestart()
        {       
            for (int i = balls.Count - 1; i > -1; i--)
            {
                Destroy(balls[i].gameObject);
                balls.RemoveAt(i);
            }
            Destroy(ballContainer.gameObject);
            Destroy(dotContainer.gameObject);
            player.transform.position = new Vector3(0, transform.position.y, -9);
            spawnPosition.position = player.transform.position;
            ballCount.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + .25f);
            Start();          
        }
        #endregion

        void Update()
        {
            switch (currentPlayerState)
            {
                case playerState.aim:
                    SpeedUp.SetActive(false);
                    dotContainer.gameObject.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlayerTouch();
                    }
                    if (Input.GetMouseButton(0))
                    {
                        PlayerDrag();
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        PlayerRelease();
                    }
                    break;
                case playerState.fire:
                    dotContainer.gameObject.SetActive(false);
                    player.enabled = false;
                    FireBalls();
                    break;
                case playerState.wait:
                    break;
                case playerState.move:
                    break;
                case playerState.endshot:
                    break;
                default:
                    break;
            }

            if (currentPlayerState == playerState.aim)
            {
                count += Time.deltaTime;
                Vector3 startScale = HandIcon.position;
                HandIcon.position = startScale * (1 - 0.025f * Mathf.Sin(count * 3));
            }
            else
            {
                HandIcon.gameObject.SetActive(false);
                Text1.gameObject.SetActive(false);
            }
        }

        #region Aim
        private void PlayerTouch()
        {
            touchStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (Transform dot in ArrowDots)
            {
                dot.position = transform.position;
                dot.gameObject.SetActive(true);
            }
        }

        private void PlayerDrag()
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movement = touchStartPosition - currentPosition;

            if (movement == Vector3.zero)
                return;
            SetArrowPoints(transform.position, movement);
        }

        private void PlayerRelease()
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movement = touchStartPosition - currentPosition;
            movement.Normalize();
            velocity = speed * movement;
            currentPlayerState = playerState.fire;

            if (Vector3.Angle(movement, Vector3.up) > 90 - offsetRotation)
            {
                currentPlayerState = playerState.aim;
            }

            foreach (Transform dot in ArrowDots)
            {
                dot.gameObject.SetActive(false);
            }

            if (Vector2.Distance(touchStartPosition, currentPosition) <= .1)
            {
                currentPlayerState = playerState.aim;
            }
        }
        #endregion

        #region Arrow
        public void SetUpArrow()
        {
            dotContainer = new GameObject("Arrow").transform;
            ArrowDots = new List<Transform>();

            for (int i = 0; i < numberOfDots; i++)
            {
                GameObject dot = Instantiate(dots);
                dot.transform.localScale = new Vector3(.1f, .1f);
                dot.transform.position = Vector3.zero;
                dot.SetActive(false);
                ArrowDots.Add(dot.transform);
                dot.transform.SetParent(dotContainer);
            }

            dotContainer.gameObject.SetActive(true);
        }

        void SetArrowPoints(Vector3 posStart, Vector2 direction)
        {
            float velocity = Mathf.Sqrt((direction.x * direction.x) + (direction.y * direction.y));
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));
            float fTime = 0;

            fTime += 0.1f;
            foreach (Transform dot in ArrowDots)
            {
                float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
                float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 pos = new Vector3(posStart.x + dx, posStart.y + dy, 0);
                dot.position = pos;
                dot.gameObject.SetActive(true);
                dot.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x));
                fTime += 0.25f;
            }
        }
        #endregion

        #region Balls
        private void FireBalls()
        {
            currentPlayerState = playerState.wait;
            StartCoroutine(SpawnBallsCoroutine());
        }

        private IEnumerator SpawnBallsCoroutine()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                Ball ball = balls[i];
                ball.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y);
                ball.gameObject.SetActive(true);
                ball.GetComponent<Rigidbody2D>().velocity = velocity;
                RemainingBalls(balls.Count - 1 - i);
                yield return new WaitForSeconds(.1f);
            }
            ballCount.gameObject.SetActive(false);
        }

        private void RemainingBalls(int count)
        {
            ballCount.text = "x" + count.ToString();
        }

        public void AddBall()
        {
            if(PlayerPrefs.GetInt("Selected Ball") == 1 || PlayerPrefs.GetInt("Selected Ball") == 0)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(1)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 2)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(2)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 3)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(1)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.GetComponent<SpriteRenderer>().color = currentColor;
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 4)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(4)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 5)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(5)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 6)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(6)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 7)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(7)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
            if (PlayerPrefs.GetInt("Selected Ball") == 8)
            {
                var Ball = Resources.Load<Ball>("Balls/Ball(8)");
                theBall = Ball as Ball;
                Ball ball = (Ball)Instantiate(theBall);
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(false);
                ball.HitFloor += OnBallHitWall;
                balls.Add(ball);
                ball.transform.SetParent(ballContainer);
            }
        }

        private void OnBallHitWall(Ball ball)
        {
            ball.gameObject.SetActive(false);

            if (stoppedBalls == 0)
            {
                transform.position = new Vector3(ball.transform.position.x, transform.position.y);
                player.enabled = (true);
                StartCoroutine(SpeedUpGame());
            }

            ball.transform.position = transform.position;
            stoppedBalls++;

            if (AllBallsStoped)
            {
                StopAllCoroutines();
                Time.timeScale = 1;
                SpeedUp.SetActive(false);
                spawnPosition.position = transform.position;
                for (int i = 0; i < gameManager.ballsToAdd; i++)
                {
                    AddBall();
                }
                gameManager.ballsToAdd = 0;         
                stoppedBalls = 0;
                ballCount.transform.position = new Vector3(ball.transform.position.x, transform.position.y + .25f);
                ballCount.text = "x" + balls.Count;
                ballCount.gameObject.SetActive(true);
                gameManager.NextTurn();
            }         
        }

        public bool AllBallsStoped
        {
            get
            {
                return stoppedBalls == balls.Count;
            }
        }
        #endregion

        public void SpeedUpButton()
        {
            Time.timeScale = 4;
        }

        private IEnumerator SpeedUpGame()
        {
            yield return new WaitForSeconds(4f);
            SpeedUp.SetActive(true);
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
    }
}
