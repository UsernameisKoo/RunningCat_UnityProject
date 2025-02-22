using UnityEngine;

public class ConeGenerate : MonoBehaviour
{
    public float speed;
    public static int randomValue = UnityEngine.Random.Range(0, 1);

    void Awake()
    {

    }


    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

    }
}
