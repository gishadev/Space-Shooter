using System;
using System.Collections;
using UnityEngine;

namespace SpaceGame.Effects.Audio
{
    public class AudioManager : ImportTarget
    {
        #region Singleton
        public static AudioManager Instance { private set; get; }
        #endregion

        public AudioData[] musicCollection;
        public AudioData[] sfxCollection;

        public float fadeTransitionSpeed = 0.5f;

        private AudioData currMusic;
        private AudioData prevMusic;

        #region PROPERTIES
        public bool IsMusicMuted { get { return MusicVolume == 0; } }
        public bool IsSfxMuted { get { return SfxVolume == 0; } }

        public float MusicVolume
        {
            get => _musicVolume;
            set { _musicVolume = Mathf.Clamp01(value); }
        }
        float _musicVolume = 1f;

        public float SfxVolume
        {
            get => _sfxVolume;
            set { _sfxVolume = Mathf.Clamp01(value); }
        }
        float _sfxVolume = 1f;
        #endregion

        void Awake()
        {
            CreateInstance();

            SetUpAudioArray(musicCollection);
            SetUpAudioArray(sfxCollection);

            PlayMusic("Music");
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;
            else
            {
                if (Instance != this)
                    Destroy(gameObject);
            }
        }

        private void SetUpAudioArray(AudioData[] _array)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                GameObject child = new GameObject(_array[i].Name);
                child.transform.SetParent(transform);

                AudioSource audioSource = child.AddComponent<AudioSource>();

                _array[i].go = child;
                _array[i].audioSource = audioSource;

                _array[i].audioSource.clip = _array[i].audioClip;
                _array[i].audioSource.volume = _array[i].volume;
                _array[i].audioSource.pitch = _array[i].pitch;
                _array[i].audioSource.loop = _array[i].isLooping;
            }
        }

        #region Play Music
        public void PlayMusic(string _name)
        {
            Debug.Log("Playing Music");
            AudioData data = Array.Find(musicCollection, bgm => bgm.Name == _name);
            if (data == null)
            {
                Debug.LogError("There is no music with name " + _name);
                return;
            }
            else
            {
                prevMusic = currMusic;
                currMusic = data;

                PlayNextMusicTrack();
            }
        }

        public void PlayMusic(int index)
        {
            if (index < 0 || index > musicCollection.Length - 1)
            {
                Debug.LogError("There is no music with index " + index);
                return;
            }
            AudioData data = musicCollection[index];

            prevMusic = currMusic;
            currMusic = data;

            PlayNextMusicTrack();
        }

        private void PlayNextMusicTrack()
        {
            currMusic.audioSource.Play();

            if (!currMusic.isFade && prevMusic != null) prevMusic.audioSource.Stop();
            if (currMusic.isFade)
            {
                StartCoroutine(FadeIn(currMusic));
                if (prevMusic != null) StartCoroutine(FadeOut(prevMusic));
            }
        }
        #endregion

        #region Play SFX
        public void PlaySFX(string _name)
        {
            AudioData data = Array.Find(sfxCollection, sfx => sfx.Name == _name);
            if (data == null)
            {
                Debug.LogError("There is no sfx with name " + _name);
                return;
            }
            else
            {
                data.audioSource.Play();
            }
        }

        public void PlaySFX(int index)
        {
            if (index < 0 || index > sfxCollection.Length - 1)
            {
                Debug.LogError("There is no sfx with index " + index);
                return;
            }

            AudioData data = sfxCollection[index];
            data.audioSource.Play();
        }
        #endregion

        #region Fade Transition
        private IEnumerator FadeIn(AudioData _audioData)
        {
            _audioData.audioSource.volume = 0;
            float volume = _audioData.audioSource.volume;

            while (_audioData.audioSource.volume < _audioData.volume)
            {
                volume += fadeTransitionSpeed * Time.deltaTime;
                _audioData.audioSource.volume = volume;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator FadeOut(AudioData _audioData)
        {
            float volume = _audioData.audioSource.volume;

            while (_audioData.audioSource.volume > 0)
            {
                volume -= fadeTransitionSpeed * Time.deltaTime;
                _audioData.audioSource.volume = volume;
                yield return new WaitForSeconds(0.1f);
            }
            if (_audioData.audioSource.volume == 0)
            {
                _audioData.audioSource.Stop();
                _audioData.audioSource.volume = _audioData.volume;
            }
        }
        #endregion

        #region Volume
        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;

            for (int i = 0; i < musicCollection.Length; i++)
                musicCollection[i].audioSource.volume = MusicVolume;
        }

        public void SetSFXVolume(float volume)
        {
            SfxVolume = volume;

            for (int i = 0; i < sfxCollection.Length; i++)
                sfxCollection[i].audioSource.volume = volume;
        }
        #endregion

        #region ImportTarget
        public override void Import(string _collection, ResourceData[] _resources)
        {
            AudioData[] coll = new AudioData[_resources.Length];

            for (int i = 0; i < _resources.Length; i++)
            {
                AudioData data = new AudioData();
                data.Name = _resources[i].name;
                data.audioClip = _resources[i].o as AudioClip;

                data.isLooping = false;
                data.volume = 1f;
                data.pitch = 1f;

                coll[i] = data;
            }

            GetType().GetField(_collection).SetValue(this, coll);
        }
        #endregion
    }
}