using System.Collections.Generic;
using Assets.Scripts.Data;
using Data;
using Infrastructure.Services;

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
        }
    }
}