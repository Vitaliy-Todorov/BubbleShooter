using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.UI 
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _levels;
        [SerializeField]
        private GameObject _levelsMenu;

        [SerializeField]
        private Button _exet;

        void Start()
        {
            _levels.onClick.AddListener(LevelsMenu);
            _exet.onClick.AddListener(Exet);
        }

        private void LevelsMenu()
        {
            _levelsMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Exet() => 
            Application.Quit();
    }
}

