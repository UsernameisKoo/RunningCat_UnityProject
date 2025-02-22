
using UnityEngine;
using static SignPostGenerate;
using static ConeGenerate;


public class obstackleMove : MonoBehaviour
{
    public float speed;
    public int[] obstacleList = new int [2];

    void obstacleListfunc(int a, int b)
    {
        if (a==b)
        {
            if (a == 0)
            {
                for (int i = 0; i < obstacleList.Length; i++)
                {
                    obstacleList[i] = a;
                }
            }
            else if (a == 1)
            {
                for (int i = 0; i < obstacleList.Length; i++)
                {
                    obstacleList[i] = b;
                }
            }
        }
        else
        {
            obstacleList[a] = a;
            obstacleList[b] = b;
        }
    }

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
        if (Time.deltaTime%3==0)
        {
            obstacleListfunc(randomValueSP, randomValueSP);

            for (int i = 0;i < obstacleList.Length; i++)
            {
                if (obstacleList[i] == randomValueSP)
                {
                    GetComponentInChildren(1);
                }
                else
                {
                    GetComponentInChildren(2);
                }
            }
        }


    }


}
