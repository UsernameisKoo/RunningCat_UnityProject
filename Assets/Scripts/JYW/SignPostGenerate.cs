using UnityEngine;
using UnityEngine.UIElements;

public class SignPostGenerate: MonoBehaviour
{
    public float speed;
    int randomValue = Random.Range(0, 1);
    void OnEnable()
    {
        
    }

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnDisable()
    {
        
    }

}
