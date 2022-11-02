using System.Collections.Generic;
using Assets.Scripts.Data;
using Data;
using Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class GeneralDataService : IService 
    {
        public PlayerProgressData PlayerProgressData;

        public GeneralData GeneralData;

        public GeneralDataService(StaticDataService staticDataService)
        {
            PlayerProgressData = new PlayerProgressData();
            
            GeneralData = new GeneralData(staticDataService);
            FindEdgesOfScreen();
        }

        private void FindEdgesOfScreen()
        {
            Camera camera = Camera.main;
            GeneralData.lowerLeftCorner = camera.ViewportToWorldPoint(new Vector2(0, 0));
            GeneralData.upperRightCorner = camera.ViewportToWorldPoint(new Vector2(1, 1));
        }
    }
}