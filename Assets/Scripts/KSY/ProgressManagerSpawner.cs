using UnityEngine;

public class ProgressManagerSpawner : MonoBehaviour
{
    public GameObject progressManagerPrefab;

    void Awake()
    {
        if (FindAnyObjectByType<ProgressManager>() == null)
        {
            Instantiate(progressManagerPrefab);
        }
    }
}
