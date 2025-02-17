using UnityEngine;

public class AudioPlayer<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Player
    {
        get
        {
            if (_Player == null)
            {
                _Player = Instantiate(Resources.Load<T>(typeof(T).Name));
            }
            return _Player;
        }
    }

    protected AudioSource audioSource;

    private static T _Player;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Play(AudioClip audio)
    {

    }
}