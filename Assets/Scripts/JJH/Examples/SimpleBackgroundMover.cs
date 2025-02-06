using UnityEngine;

public class SimpleBackgroundMover : MonoBehaviour
{
    public float speed = 5f;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -40)
        {
            transform.Translate(Vector2.right * 55.4f * 3f);
        }
    }
}
