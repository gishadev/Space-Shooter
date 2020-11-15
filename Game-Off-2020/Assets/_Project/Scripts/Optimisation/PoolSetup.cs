using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Optimisation
{
    public class PoolSetup : MonoBehaviour
    {
        [SerializeField] private List<PoolObject> poolObjects = new List<PoolObject>();

        private void Awake()
        {
            PoolManager.Init(poolObjects);
        }
    }
}