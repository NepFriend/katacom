// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Status : MonoBehaviour
// {

//     float hp = 100;

//     public float stunGuage = 0;
//     public float BurnGuage = 0;

//     int BurnTime = 0;

//     // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
//     private static Status _instance;

//     // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
//     public static Status Instance
//     {
//         get
//         {
//             // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
//             if (!_instance)
//             {
//                 _instance = FindObjectOfType(typeof(Status)) as Status;

//                 if (_instance == null)
//                     Debug.Log("�̱��� ���� ������Ʈ");
//             }
//             return _instance;
//         }
//     }

//     private void Awake()
//     {
//         if (_instance == null)
//         {
//             _instance = this;
//         }
//         // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
//         else if (_instance != this)
//         {
//             Destroy(gameObject);
//         }
//         // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
//         DontDestroyOnLoad(gameObject);
//     }

//     private void Update()
//     {
//         if (stunGuage > 0)
//         {
//             stunGuage -= Time.deltaTime;
//         }

//         if (BurnGuage > 0)
//         {
//             BurnGuage -= Time.deltaTime;
//             if (BurnGuage < 0)
//             {
//                 BurnGuage = 0;
//             }
//         }
//     }

//     public void Damaged(float dmg)
//     {
//         hp -= dmg;
//         Debug.Log("�÷��̾� hp = " + hp);
//     }

//     public void StunGuageIncrease(float increment)
//     {
//         stunGuage += increment;
//         Debug.Log("�÷��̾� ���� ������ = " + stunGuage);

//         if (stunGuage > 100)
//         {

//             Stun();
//         }

//     }

//     private void Stun()
//     {
//         stunGuage = 0;
//         Debug.Log("�÷��̾� ����!");
//     }

//     public void BurnGuageIncrease(float increment)
//     {
//         if (BurnTime == 0)
//         {
//             BurnGuage += increment;
//             Debug.Log("�÷��̾� ȭ�� ������ = " + BurnGuage);


//             if (BurnGuage > 100)
//             {
//                 BurnGuage = 0;
//                 BurnTime = 5;
//                 StartCoroutine(Burn());
//             }
//         }



//     }
//     IEnumerator Burn() // �÷��̾ �����ؾ� �ϴ� Ư�� �׼�
//     {
//         while (BurnTime > 0)
//         {
//             hp -= 5;
//             Debug.Log("�÷��̾� ȭ��!");
//             Debug.Log("�÷��̾� hp = " + hp);
//             yield return new WaitForSeconds(1);
//             BurnTime--;
//         }

//     }

// }
