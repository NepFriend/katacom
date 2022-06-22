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

    IEnumerator shoot() // 플레이어 폭탄썼을 시 움직임 일람   이것만큼은 커플링 상관안하고 코딩
    {
        sk.AnimationState.SetAnimation(1, "shoot_top", true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(reload());
    }

    IEnumerator reload() // 플레이어 폭탄썼을 시 움직임 일람   이것만큼은 커플링 상관안하고 코딩
    {
        sk.AnimationState.SetAnimation(1, "reload_top", true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(shoot());
    }
}
