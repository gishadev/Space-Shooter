using UnityEngine;
using System;
using SpaceGame.Optimisation;

namespace SpaceGame.Effects.VFX
{
    public class VFXManager : ImportTarget
    {
        #region Singleton
        public static VFXManager Instance { private set; get; }
        #endregion

        public VFXData[] effectsCollection;

        void Awake()
        {
            Instance = this;
        }

        public void Emit(string effectName, Vector3 position, Quaternion rotation)
        {
            VFXData effect = Array.Find(effectsCollection, x => x.name == effectName);

            if (effect == null)
            {
                Debug.LogErrorFormat("Effect with name {0} wasn't found!", effectName);
                return;
            }

            PoolManager.Instantiate(effect.prefab, position, rotation);
        }
        public void Emit(string effectName, Vector3 position)
        {
            VFXData effect = Array.Find(effectsCollection, x => x.name == effectName);

            if (effect == null)
            {
                Debug.LogErrorFormat("Effect with name {0} wasn't found!", effectName);
                return;
            }

            PoolManager.Instantiate(effect.prefab, position, effect.prefab.transform.rotation);
        }

        #region ImportTarget
        public override void Import(string _collection, ResourceData[] _resources)
        {
            VFXData[] coll = new VFXData[_resources.Length];

            for (int i = 0; i < _resources.Length; i++)
            {
                VFXData data = new VFXData();
                data.name = _resources[i].name;
                data.prefab = _resources[i].o as GameObject;

                coll[i] = data;
            }

            GetType().GetField(_collection).SetValue(this, coll);
        }
        #endregion
    }

    [Serializable]
    public class VFXData
    {
        public string name;
        public GameObject prefab;
    }
}