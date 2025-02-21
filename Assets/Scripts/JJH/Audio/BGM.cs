using UnityEngine;

public class BGM : AudioPlayer<BGM>
{
    public override void Play(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.loop = true;
        audioSource.Play();
    }
}
