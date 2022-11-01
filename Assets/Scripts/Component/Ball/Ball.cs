using System.Collections.Generic;
using UnityEngine;

namespace Component
{
    public class Ball : MonoBehaviour
    {
        public Color Color;
        public bool Stationary;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().color = Color;
        }

        public void Construct(Color color)
        {
            Color = color;
            GetComponent<SpriteRenderer>().color = Color;
        }
    }
}