using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance; // Use the singleton instance
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.tag); 

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.transform.position.y <= 0) 
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