using UnityEngine;

public static class AudioExtensions
{
    public static void Play(this AudioClip sound)
    {
        SFX.Player.Play(sound);
    }

    public static void PlayLoop(this AudioClip sound)
    {
        BGM.Player.Play(sound);
    }
}
