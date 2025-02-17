using UnityEngine;

public class EventCall : MonoBehaviour
{
    public GameObject clone;
    // 유니티 게임 오브젝트에 생명을 넣어주는 코드
    // 어떠한 동작에 대한 타이밍

    //제일 먼저 실행
    void Awake() // 어떤 오브젝트나 컴포넌트가 깨어나자 마자
    {
        Debug.Log("Awake");
    }

    //그 다음으로 실행
    //
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    //세번째로 실행
    void Start()
    {
        Debug.Log("Start");
        var randomValue = Random.Range(0, 100);
        if (randomValue < 30) // 30%
        {
            Instantiate(clone);
        }
    }

    //네 번째로 실행
    //매 프레임 마다 실행
    void Update() // 1초에 약 50프레임
    { // 컴퓨터 마다 성능이 다르다. 상황에 따라서 성능이 달라진다.
        // Debug.Log("Update");
        Instantiate(clone);
        Debug.Log(Time.deltaTime);
    }

    //오브젝트가 사라질 때 첫번째로 실행
    //오브젝트 풀링 (최적화 기법)
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    //오브젝트가 사라지고 나서 실행
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
