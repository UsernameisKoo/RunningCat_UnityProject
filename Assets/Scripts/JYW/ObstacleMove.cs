
using UnityEngine;


public class obstackleMove : MonoBehaviour
{
    public float speed;

   

    void Awake()
    {
       
    }

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.x <= -50)
        {
            transform.Translate(Vector2.right * 55.4f * 3f);
        }
        


    }


}
