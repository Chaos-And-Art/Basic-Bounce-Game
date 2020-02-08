using System;
using UnityEngine;
using UnityEngine.UI;

namespace ExtremeBalls
{
    public class OpenAnimation : MonoBehaviour
    {
        //Title Animation\\
        public Transform Title;
        private float count = 0;
        private Vector3 startScale;

        //Ball Animation\\
        public Rigidbody2D ball;
        private float ScreenxSize;
        private float ScreenySize;
        private float ballSpeedx;
        private float ballSpeedy;
        private float BallSize;
        public SpriteRenderer Renderer;
        private Color RandomColor;
        private float Nballs = 5;

        private void Start()
        {
            SetUpBalls();
            startScale = Title.localScale;
        }

        private void Update()
        {
            count += Time.deltaTime;
            Title.localScale = startScale * (1 + 0.15f * Mathf.Sin(count * 5));
        }

        private void SetUpBalls()
        {
            for (int i = 0; i < Nballs; i++)
            {
                ScreenxSize = UnityEngine.Random.Range(-1, 1);
                ScreenySize = UnityEngine.Random.Range(1, -1);
                ball.transform.position = new Vector3(ScreenxSize, ScreenySize, 100);

                BallSize = UnityEngine.Random.Range(25f, 75f);
                ball.transform.localScale = new Vector3(BallSize, BallSize);

                ballSpeedx = UnityEngine.Random.Range(-10, 10);
                ballSpeedy = UnityEngine.Random.Range(-10, 10);
                ball.velocity = new Vector2(ballSpeedx, ballSpeedy);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            Renderer.GetComponent<SpriteRenderer>();
            RandomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            Renderer.color = RandomColor;
        }
    }
}
