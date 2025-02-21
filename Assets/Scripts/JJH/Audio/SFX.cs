using UnityEngine;
using System;

public class SFX : AudioPlayer<SFX>
{
    public static AudioClipStorage.UISound UI => Player.ui;
    public static AudioClipStorage.PropSound Prop => Player.prop;

    [SerializeField] private AudioClipStorage.UISound ui;
    [SerializeField] private AudioClipStorage.PropSound prop;

    public override void Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}

public class AudioClipStorage
{
    [Serializable]
    public class UISound
    {
        public AudioClip open;
        public AudioClip close;
    }

    [Serializable]
    public class PropSound
    {
        public AudioClip box;
    }
}