using UnityEngine;

public class Score : MonoBehaviour
{
    public enum PotionType { Normal, Big, Rainbow } // 포션 종류
    public PotionType coinType = PotionType.Normal;
    public int normalPotionValue = 10; // 일반 점수
    public int BigPotionValue = 20; // 큰 포션 점수
    public int RainbowPotionValue = 50; // 아이템 점수

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (coinType == PotionType.Big)
            {
                GameManager.Instance.AddScore(BigPotionValue); 
                Debug.Log("Big Potion Collected! +20 points");
            }
            else if (coinType == PotionType.Rainbow)
            {
                GameManager.Instance.AddScore(RainbowPotionValue); 
                Debug.Log("Rainbow Potion Collected! +10 points");
            }
            else
            {
                GameManager.Instance.AddScore(normalPotionValue); 
                Debug.Log("Normal Potion Collected! +10 points");
            }

            Destroy(gameObject); // 먹은 포션 제거
        }
    }
}
