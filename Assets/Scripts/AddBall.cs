using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtremeBalls
{
    public class AddBall : MonoBehaviour
    {
        public Transform border;
        public Action<AddBall> OnCollision;
        private Vector3 startScale;
        private float count = 0;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                if (OnCollision != null)
                    OnCollision(this);

                Destroy(gameObject);
            }
        }

        void Start()
        {
            count = UnityEngine.Random.Range(0, Mathf.PI);
            startScale = border.localScale;
        }

        void Update()
        {
            count += Time.deltaTime;
            border.localScale = startScale * (1 + 0.1f * Mathf.Sin(count * 10));
        }
    }
}
