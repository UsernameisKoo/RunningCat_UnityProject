using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.tag); // 충돌한 객체의 태그 출력

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.transform.position.y <= 0) // 낭떠러지에 떨어졌는지 확인
            {
                Debug.Log("Player Collision Detected");
                gameManager.HeartDown();
                gameManager.HeartDown();
                gameManager.HeartDown();
                Invoke("Gameover", 0.5f);
            }
        }


        if (collision.gameObject.CompareTag("finish"))
        {
            Debug.Log("Finish Collision Detected");
            gameManager.heart = 3;
            gameManager.Me.SetActive(false);
            gameManager.Stages[gameManager.stageIndex].SetActive(false);
            gameManager.StageClear();
            Debug.Log("Game Clear");
        }
    }
}
