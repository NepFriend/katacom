using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombieAnimation : MonoBehaviour
{
    SkeletonAnimation sk;

    private void Start()
    {
        sk = GetComponent<SkeletonAnimation>();

        int dir;

        dir = Random.Range(0,2);

        if (dir < 1)
        {
            sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
        }
        else 
        {
            sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
        }
        sk.AnimationState.SetAnimation(1, "idle", true);
    }

    public virtual void AnimeIdle()
    {
        sk.AnimationState.SetEmptyAnimation(1, 0.2f);
        sk.AnimationState.SetAnimation(1, "idle", true);
    }

    public virtual void AnimeMove()
    {
        sk.AnimationState.SetEmptyAnimation(1, 0.2f);
        sk.AnimationState.SetAnimation(1, "move_f", true);
    }

    public virtual void AnimeAttack()
    {
        sk.AnimationState.SetAnimation(1, "idle", true);
        sk.AnimationState.SetEmptyAnimation(2, 0.2f);
        sk.AnimationState.SetAnimation(2, "combat", true);
        sk.AnimationState.AddEmptyAnimation(2, 0.2f, 0.7f);
    }
}
