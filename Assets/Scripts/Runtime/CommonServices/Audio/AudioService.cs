using System;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Audio
{
    public class AudioService : IAudioService, IDisposable
    {
        private AudioSource _musicSource;
        private AudioSource _soundSource;

        public void InitMusicSource(AudioSource audioSource)
        {
            _musicSource = audioSource;
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
        }

        public void InitSoundSource(AudioSource audioSource)
        {
            _soundSource = audioSource;
            _soundSource.playOnAwake = false;
        }

        public void Dispose()
        {
            if (_musicSource != null)
            {
                _musicSource.Stop();
                _musicSource = null;
            }

            if (_soundSource != null)
            {
                _soundSource.Stop();
                _soundSource = null;
            }
        }

        public void PlaySound(AudioClip soundClip, float volume = 1f, float pitch = 1f)
        {
            if (_soundSource == null || soundClip == null) return;

            _soundSource.clip = soundClip;
            _soundSource.volume = volume;
            _soundSource.pitch = pitch;
            _soundSource.Play();
        }

        public void PlayMusic(AudioClip musicClip, float volume = default)
        {
            if (_musicSource == null || musicClip == null) return;

            if (volume != default)
                _musicSource.volume = volume;

            _musicSource.clip = musicClip;

            _musicSource.Play();
        }

        public void StopMusic()
        {
            if (_musicSource != null)
            {
                _musicSource.Stop();
            }
        }

        public void ResumeMusic()
        {
            if (_musicSource != null && !_musicSource.isPlaying)
            {
                _musicSource.Play();
            }
        }
        
        public void SetVolume(float volume)
        {
            if (_musicSource != null)
            {
                _musicSource.volume = volume;
            }

            if (_soundSource != null)
            {
                _soundSource.volume = volume;
            }
        }
        public float GetVolume()
        {
            if (_musicSource != null)
            {
                return _musicSource.volume;
            }

            return 0f;
        }
    }
}
