using System;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Audio
{
    public interface IAudioService : IDisposable
    {
        void PlaySound(AudioClip soundClip, float volume = 1f, float pitch = 1f);
        void PlayMusic(AudioClip musicClip, float volume = 1f);
        void StopMusic();
        void ResumeMusic();
        void SetVolume(float volume);
        float GetVolume();
    }
}
