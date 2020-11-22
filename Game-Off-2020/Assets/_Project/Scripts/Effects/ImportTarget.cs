using UnityEngine;

namespace SpaceGame.Effects
{
    public abstract class ImportTarget : MonoBehaviour
    {
        public abstract void Import(string _collection, ResourceData[] _resources);
    }
}