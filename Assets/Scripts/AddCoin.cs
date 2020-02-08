using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtremeBalls
{
    public class AddCoin : MonoBehaviour
    {
            public Action<AddCoin> OnCollision;
            void OnTriggerEnter2D(Collider2D other)
            {
                if (other.CompareTag("Ball"))
                {
                    if (OnCollision != null)
                        OnCollision(this);

                    Destroy(gameObject);
                }
            }
        }
    }
