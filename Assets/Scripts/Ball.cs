using System;
using UnityEngine;

namespace ExtremeBalls
{
    public class Ball : MonoBehaviour
    {
        public Action<Ball> HitFloor;

        public AudioSource source;
        public AudioClip shapeHit;
        public AudioClip ExtraBall;
        public AudioClip Coin;

        public SpriteRenderer Renderer;
        private Color RandomColor;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                if (HitFloor != null)
                    HitFloor(this);
            }

            if(collision.gameObject.tag == "Shapes")
            {
                source.PlayOneShot(shapeHit);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "ExtraBall")
            {
                source.PlayOneShot(ExtraBall);
            }

            if(collision.gameObject.tag == "Coin")
            {
                source.PlayOneShot(Coin);
            }
        }
    }
}
