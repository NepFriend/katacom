using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTest2 : MonoBehaviour
{
    public SkeletonAnimation sk;

    bool aiming;
    bool reloading;
    bool run;

    // Start is called before the first frame update
    void Start()
    {
        sk = GetComponent<SkeletonAnimation>();
        //  sk.AnimationState.SetAnimation(1, "M_moveforward_under", true);
        sk.AnimationState.SetAnimation(2, "M_shoot_top", true);
        //sk.AnimationState.AddAnimation(0, "M_reload_top", false, 0);

        reloading = false;
        aiming = false;
        run = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                sk.AnimationState.SetAnimation(1, "M_moveforward_aiming_under" , true);
                run = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                sk.AnimationState.SetAnimation(1, "M_moveforward_under", true);
                run = true;
            }
        }

        if (!reloading)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                aiming = false;
                sk.AnimationState.AddAnimation(2, "M_shoot_top", true, 0);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                aiming = true;
                sk.AnimationState.AddAnimation(2, "M_shoot_aiming_top", true, 0);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {


                StartCoroutine(reloadingM());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                StopAllCoroutines();
                sk.AnimationState.SetEmptyAnimation(0, 1);
                aiming = false;
                sk.AnimationState.SetAnimation(2, "M_shoot_top", true);
                reloading = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StopAllCoroutines();
                sk.AnimationState.SetEmptyAnimation(0, 1);
                aiming = true;
                sk.AnimationState.SetAnimation(2, "M_shoot_aiming_top", true);
                reloading = false;

            }
           
        }

        
    }
    IEnumerator reloadingM() // 플레이어 폭탄썼을 시 움직임 일람   이것만큼은 커플링 상관안하고 코딩
    {

        reloading = true;
        sk.AnimationState.SetEmptyAnimation(2, 1);
        sk.AnimationState.AddAnimation(0, "M_reload_top", false, 0);
        yield return new WaitForSeconds(3.2f);
        reloading = false;
        sk.AnimationState.SetEmptyAnimation(0, 1);
        if (!aiming)
        {
            sk.AnimationState.SetAnimation(2, "M_shoot_top", true);
        }
        else
        {
            sk.AnimationState.SetAnimation(2, "M_shoot_aiming_top", true);
        }
    }

}
