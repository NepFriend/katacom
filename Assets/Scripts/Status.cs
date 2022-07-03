using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    float hp = 100;

    public float stunGuage = 0;
    public float BurnGuage = 0;

    int BurnTime = 0;

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static Status _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static Status Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(Status)) as Status;

                if (_instance == null)
                    Debug.Log("싱글톤 없는 오브젝트");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (stunGuage > 0)
        {
            stunGuage -= Time.deltaTime;
        }

        if (BurnGuage > 0)
        {
            BurnGuage -= Time.deltaTime;
            if (BurnGuage < 0)
            {
                BurnGuage = 0;
            }
        }
    }

    public void Damaged(float dmg)
    {
        hp -= dmg;
        Debug.Log("플레이어 hp = " + hp);
    }

    public void StunGuageIncrease(float increment)
    {
        stunGuage += increment;
        Debug.Log("플레이어 스턴 게이지 = " + stunGuage);

        if (stunGuage > 100)
        {
           
            Stun();
        }

    }

    private void Stun()
    {
        stunGuage = 0;
        Debug.Log("플레이어 스턴!");
    }

    public void BurnGuageIncrease(float increment)
    {
        if (BurnTime == 0)
        {
            BurnGuage += increment;
            Debug.Log("플레이어 화상 게이지 = " + BurnGuage);


            if (BurnGuage > 100)
            {
                BurnGuage = 0;
                BurnTime = 5;
                StartCoroutine(Burn());
            }
        }
       


    }
    IEnumerator Burn() // 플레이어가 정지해야 하는 특수 액션
    {
        while (BurnTime > 0)
        {
            hp -= 5;
            Debug.Log("플레이어 화상!");
            Debug.Log("플레이어 hp = " + hp);
            yield return new WaitForSeconds(1);
            BurnTime--;
        }

    }

}
