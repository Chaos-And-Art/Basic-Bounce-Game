using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtremeBalls
{
    public class GameManager : MonoBehaviour
    {
        public GoogleAds googleAds;
        public UIManager uiManager;
        public PlayerManager playerManager;
        public AudioSource source;
        public AudioClip shapesHitGround;
        public GameObject secondChance;
        [Space(20)]
 
        public int numberOfColumn = 7;
        public Color[] shapeColors;
        public int colorStep = 10;
        public float brickProbability = 0.3f;
        public float powerUpProbability = 0.1f;
        public float ballProbability = .1f;
        public float emptyProbabilty = 0.2f;
        private float addPowerupProbability = 0.50f;
        private float maxSpawnProbability;
        private int oneBall;
        private bool hardShape = false;
        [Space(20)]

        public Shapes[] shapePrefab;
        public float[] shapeProbabilities;
        public AddBall addBallPrefab;
        public AddCoin addCoinPrefab;
        [Space(20)]

        private Rect screenRect;
        private float stepX;
        private float maxCellProbability;
        private List<Transform> shapes;
        private Transform gridContainer;
        private bool continuePressed;
        private int numIgnoreCreateLine;
        [Space(20)]

        //Misc\\
        public int ballsToAdd;
        private int Level;
        public bool CheckLoose = false;
        private int SecondChances = 0;
        private float count = 0;
        private Vector3 startScale;

        void Start()
        {
            UIManager();

            SetUpScreen();

            SetUpGrid();

            SetUpLevelBounds();

            startScale = secondChance.transform.localScale;
        }
        private void Update()
        {
            ScoresAndCoins.SCM.Level = Level;
            if (CheckLoose == true)
            {
                CheckLoose = false;
                GameOver();
            }

            count += Time.deltaTime;
            secondChance.transform.localScale = startScale * (1 + 0.15f * Mathf.Sin(count * 5));
        }

        #region UI
        void UIManager()
        {
            uiManager.PlayButtonClicked += OnPlayButtonClicked;
            uiManager.RestartButtonClicked += OnRestartButtonClicked;

        }

        private void OnPlayButtonClicked()
        {
            StartGame();
        }

        private void OnRestartButtonClicked()
        {
            StartGame();
        }
        #endregion

        #region GridControl
        void StartGame()
        {
            SecondChances = 0;
            gridContainer.gameObject.SetActive(true);

            for (int i = shapes.Count - 1; i > -1; i--)
            {
                Destroy(shapes[i].gameObject);
                shapes.RemoveAt(i);
            }
            Level = 0;
            NextTurn();
        }

        void SetUpGrid()
        {
            stepX = screenRect.width / (numberOfColumn);

            gridContainer = new GameObject("Grid").transform;
            gridContainer.position = new Vector3(screenRect.xMin, screenRect.yMax);
            shapes = new List<Transform>();

            for (int i = 0; i < shapeProbabilities.Length; i++)
            {
                maxCellProbability += shapeProbabilities[i];
            }

            maxSpawnProbability = brickProbability + ballProbability + powerUpProbability + emptyProbabilty;
        }

        private void CreateShape()
        {
            float random;
            float probability;
            for (int x = 0; x < numberOfColumn; x++)
            {
                probability = brickProbability;
                random = Random.Range(0, maxSpawnProbability);
                if (random < probability)
                {
                    CreateShape(x, 0);
                    continue;
                }
                probability += powerUpProbability;
                if (random < probability)
                {
                    CreatePowerUp(x, 0);
                    continue;
                }
                if (oneBall == 0)
                {
                    probability += ballProbability;
                    if (random < probability || random > emptyProbabilty)
                    {
                        CreateBall(x, 0);
                        oneBall++;
                        continue;
                    }
                }
            }
        }

        private void MoveGrid()
        {
            Vector3 endPosition;
            foreach (Transform gridCell in shapes)
            {
                endPosition = gridCell.position - Vector3.up * stepX;
                gridCell.DOMove(gridCell.position, endPosition, 0.5f);
            }
        }
        private void MoveGrid_Up()
        {
            Vector3 endPosition;
            foreach (Transform gridCell in shapes)
            {
                endPosition = gridCell.position + Vector3.up * stepX * 2.59f;
                gridCell.DOMove(gridCell.position, endPosition, .5f);
            }
        }
            #endregion

        #region OutComes
            public void NextTurn()
        {
            Level++;
            oneBall = 0;
            StartCoroutine(NextTurnCoroutine());
        }

        private IEnumerator NextTurnCoroutine()
        {
            if(SecondChances == 0)
            {
                CreateShape();
                MoveGrid();
            }

            if (SecondChances == 1 || SecondChances == 2 || SecondChances == 3 || SecondChances == 4)
            {
                MoveGrid();
                SecondChances++;
            }
            if(SecondChances == 5)
            {
                CreateShape();
                MoveGrid();
            }

            yield return new WaitForSeconds(.2f);
            playerManager.currentPlayerState = PlayerManager.playerState.aim;

        }

        private void PlayerLost(Shapes collision)
        {
            CheckLoose = true;
        }

        public void GameOver()
        {
            if (SecondChances == 0)
            {
                secondChance.SetActive(true);
                source.PlayOneShot(shapesHitGround);
                playerManager.currentPlayerState = PlayerManager.playerState.endshot;
                playerManager.player.gameObject.SetActive(false);
                playerManager.ballCount.gameObject.SetActive(false);
                gridContainer.gameObject.SetActive(false);

                uiManager.PlayerLost = true;
                uiManager.PlayGame.SetActive(false);
                uiManager.GameOver.SetActive(true);
            }
            else
            {
                secondChance.SetActive(false);
                source.PlayOneShot(shapesHitGround);
                playerManager.currentPlayerState = PlayerManager.playerState.endshot;
                playerManager.player.gameObject.SetActive(false);
                playerManager.ballCount.gameObject.SetActive(false);
                gridContainer.gameObject.SetActive(false);

                uiManager.PlayerLost = true;
                uiManager.PlayGame.SetActive(false);
                uiManager.GameOver.SetActive(true);

                googleAds.ShowIntersitialAd();
            }
        }

        #endregion

        #region CreateObject

        Vector3 GetPositionFromModel(int x, int y)
        {
            Vector3 position = new Vector3(stepX + x * stepX, -y * stepX, 0);
            return position;
        }

        private void CreateShape(int x, int y)
        {
            float random = Random.Range(0f, maxCellProbability);
            float probability = 0;
            Shapes shape = null;

            for (int i = 0; i < shapePrefab.Length; i++)
            {
                probability += shapeProbabilities[i];

                if (random < probability)
                {
                    shape = Instantiate(shapePrefab[i]);
                    break;
                }
                if(random > probability)
                {
                    hardShape = true;
                }
            }
            shape.transform.SetParent(gridContainer);
            shape.gameObject.name += "_" + x.ToString();
            shape.transform.localScale *= stepX;
            shape.transform.localPosition = GetPositionFromModel(x, y);
            shape.ShapeDestroyed += ShapeDestroyedByBall;
            shape.ShapeHitGround += PlayerLost;
            shapes.Add(shape.transform);

            if(hardShape == false)
            {
                int count = Level;
                shape.SetCount(count);
                shape.SetColors(shapeColors, colorStep);
            }
            if(hardShape == true)
            {
                int count = Level * 2;
                shape.SetCount(count);
                shape.SetColors(shapeColors, colorStep);
                hardShape = false;
            }

        }

        private void ShapeDestroyedByBall(Shapes collision)
        {
            shapes.Remove(collision.transform);
        }

        private void CreateBall(int x, int y)
        {
            Transform BallTransform;
            AddBall addBall = Instantiate(addBallPrefab);
            addBall.OnCollision += AddBallCollision;
            BallTransform = addBall.transform;

            BallTransform.SetParent(gridContainer);
            BallTransform.localPosition = GetPositionFromModel(x, y);
            BallTransform.localScale *= stepX;
            shapes.Add(BallTransform);                
        }


        private void CreatePowerUp(int x, int y)
        {
            float random = Random.value;
            Transform powerupTransform;

            if (random < addPowerupProbability)
            {
                AddCoin addCoin = Instantiate(addCoinPrefab);
                addCoin.OnCollision += AddCoinCollision;
                powerupTransform = addCoin.transform;

                powerupTransform.SetParent(gridContainer);
                powerupTransform.localPosition = GetPositionFromModel(x, y);
                powerupTransform.localScale *= stepX;
                shapes.Add(powerupTransform);
            }
        }

        private void AddBallCollision(AddBall collision)
        {
            shapes.Remove(collision.transform);
            ballsToAdd++;
        }

        private void AddCoinCollision(AddCoin collision)
        {
            shapes.Remove(collision.transform);
            ScoresAndCoins.SCM.AddCoins();
        }
        #endregion

        #region GridLayout
        void SetUpScreen()
        {
            screenRect = GridSetUp.GetScreenRect();
        }

        void SetUpLevelBounds() //Sets up the screen
        {
            gridContainer.position = new Vector3(screenRect.xMin - .31f, screenRect.yMax - 1.2f);
        }
        #endregion

        public void SecondChance()
        {
            googleAds.ShowRewardVideo();
            WatchedVideo();
        }

        public void WatchedVideo()
        {
            playerManager.player.gameObject.SetActive(true);
            playerManager.ballCount.gameObject.SetActive(true);
            gridContainer.gameObject.SetActive(true);

            MoveGrid_Up();
            StartCoroutine(waitForGrid());
            SecondChances = 1;
        }

        private IEnumerator waitForGrid()
        {
            yield return new WaitForSeconds(.1f);
            playerManager.currentPlayerState = PlayerManager.playerState.aim;
            uiManager.PlayGame.SetActive(true);
            uiManager.GameOver.SetActive(false);
        }
    }
}