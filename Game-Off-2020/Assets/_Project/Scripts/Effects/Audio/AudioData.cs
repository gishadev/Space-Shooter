using UnityEngine;

namespace SpaceGame.Effects.Audio
{
    [System.Serializable]
    public class AudioData
    {
        [Header("General")]
        public string Name;
        public AudioClip audioClip;

        [Header("Audio Source Settings")]
        public bool isLooping;
        public bool isFade;
        [Range(0f, 1f)]
        public float volume;
        [Range(0.3f, 3f)]
        public float pitch;

        [HideInInspector]
        public GameObject go;
        [HideInInspector]
        public AudioSource audioSource;
    }
}