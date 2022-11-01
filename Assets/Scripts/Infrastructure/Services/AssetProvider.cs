using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IService
    {
        public GameObject Initializebl(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}