using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class StartuoForLevel : MonoBehaviour
    {
        [SerializeField]
        private MainStartup _mainStartup;

        private void Awake()
        {
            MainStartup mainStartup = FindObjectOfType<MainStartup>();

            if (mainStartup == null)
                Instantiate(_mainStartup);
        }
    }
}