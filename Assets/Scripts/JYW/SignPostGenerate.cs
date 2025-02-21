using UnityEngine;
using UnityEngine.UIElements;

public class SignPostGenerate: MonoBehaviour
{
    public float speed;
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
