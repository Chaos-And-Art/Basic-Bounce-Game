using UnityEngine;
using System.Collections;
using TMPro;
using System;

namespace ExtremeBalls
{
    public class Shapes : MonoBehaviour
    {
        public Action<Shapes> ShapeDestroyed;
        public Action<Shapes> ShapeHitGround;

        public SpriteRenderer spriteRenderer;
        public TextMeshPro number;
        private Vector3 startScale;
        private int Count;

        private int colorStep;
        private Color[] colors;

        private void Start()
        {
            startScale = spriteRenderer.transform.localScale;
        }


        #region ShapeProperties
        public void SetCount(int count)
        {
            number.text = count.ToString();
            Count = count;
        }

        public Color Color
        {
            get
            {
                return spriteRenderer.color;
            }
            set
            {
                spriteRenderer.color = value;
            }
        }

        public void SetColors(Color[] colors, int colorStep)
        {
            this.colors = colors;
            this.colorStep = colorStep;
            Color = SetColorFromCount(Count);
        }

        private Color SetColorFromCount(int count)
        {
            Color color;
            int max;

            for (int i = 0; i < colors.Length - 1; i++)
            {
                max = (i + 1) * colorStep;
                if (count < max)
                {
                    color = Color.Lerp(colors[i], colors[i + 1], (float)count / colorStep);
                    return color;
                }
            }
            color = colors[colors.Length - 1];
            return color;
        }
        #endregion

        #region Animation
        public IEnumerator DOPunchScaleCoroutine(float amplitude, float time = 1f)
        {
            Vector3 midScale = startScale * (1 - amplitude);

            float count = 0;
            float firstDuration = time / 2;

            while (count < firstDuration)
            {
                count += Time.deltaTime;

                spriteRenderer.transform.localScale = Vector3.Lerp(startScale, midScale, count / firstDuration);
                yield return null;
            }

            count = 0;

            while (count < firstDuration)
            {
                count += Time.deltaTime;

                spriteRenderer.transform.localScale = Vector3.Lerp(midScale, startScale, count / firstDuration);
                yield return null;
            }

            spriteRenderer.transform.localScale = startScale;
        }
        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ball")
            {
                Count--;
                SetCount(Count);
                Color = SetColorFromCount(Count);
                if (ScoresAndCoins.SCM.Level >= 100)
                {
                    Color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                }
                StartCoroutine(DOPunchScaleCoroutine(0.15f, 0.15f));
                if (Count <= 0)
                {
                    if (ShapeDestroyed != null)
                        ShapeDestroyed(this);

                    Destroy(gameObject);
                }
            }

            if(collision.gameObject.tag == "Ground")
            {
                if (ShapeHitGround != null)
                    ShapeHitGround(this);
            }
        }
    }
}

