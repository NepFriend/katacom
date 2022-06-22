using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTest : MonoBehaviour
{
    public SkeletonAnimation sk;

    // Start is called before the first frame update
    void Start()
    {
        sk = GetComponent<SkeletonAnimation>();
        sk.AnimationState.SetAnimation(0, "walk_under", true);
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator shoot() // �÷��̾� ��ź���� �� ������ �϶�   �̰͸�ŭ�� Ŀ�ø� ������ϰ� �ڵ�
    {
        sk.AnimationState.SetAnimation(1, "shoot_top", true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(reload());
    }

    IEnumerator reload() // �÷��̾� ��ź���� �� ������ �϶�   �̰͸�ŭ�� Ŀ�ø� ������ϰ� �ڵ�
    {
        sk.AnimationState.SetAnimation(1, "reload_top", true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(shoot());
    }
}
