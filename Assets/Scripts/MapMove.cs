using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject backgroundPrefab; // 왜 적용시키지
    public int poolSize = 3;
    public float speed = 0.5f; //
    public float backgroundWidth;

    private List<GameObject> backgroundPool;
    private float resetPosition;
    private float startPosition;

    // Update is called once per frame

    void Start()
    {
        if (backgroundPrefab == null)
        {
            Debug.LogError("Background prefab is not assigned!");
            return;
        }

        backgroundPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(backgroundPrefab);
            obj.SetActive(true);  // 초기에 활성화 상태로 설정
            obj.transform.position = new Vector3(i * backgroundWidth, 0, 0);  // 초기 위치 설정
            backgroundPool.Add(obj);
        }

        if (backgroundPool.Count > 0)
        {
            SpriteRenderer spriteRenderer = backgroundPool[0].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                backgroundWidth = spriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found on background prefab!");
                return;
            }
        }

        resetPosition = -backgroundWidth;
        startPosition = backgroundWidth * (poolSize - 1);

        // 얘는 배경 연결을 위해  전 화면, 두 번째 화면 x부분 가져오는 역할

        // 초기 배경 설정
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bg = backgroundPool[i];
            bg.SetActive(true);
            bg.transform.position = new Vector3(i * backgroundWidth, 0, 0);
        }
    }

    void Update() // 
    {
        foreach(GameObject bg in backgroundPool) 
        {
            if (bg.activeSelf) // 배경 게임 오브젝트의 현재 활성화 상태 
            {
                bg.transform.Translate(Vector3.left * speed * Time.deltaTime); // 기존 update코드
                if (bg.transform.position.x <= resetPosition) // 현 배경 x좌표가 
                {
                    bg.SetActive(false);
                    ActivateBackground(startPosition);
                }
            }
        }
    }

    void ActivateBackground(float xposition) 
    {
        foreach (GameObject bg in backgroundPool)
        {
            if (!bg.activeSelf)
            {
                bg.SetActive(true);
                bg.transform.position =new Vector3(xposition, 0, 0);
                return;
            }
        }
    }
}
