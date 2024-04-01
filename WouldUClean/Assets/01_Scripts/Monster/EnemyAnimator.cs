using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private GolemFSM _golemFsm;

    private readonly int walkHash = Animator.StringToHash("speed");
    private readonly int attackHash = Animator.StringToHash("Attack");
    private readonly int deadHash = Animator.StringToHash("Dead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        transform.parent.TryGetComponent<GolemFSM>(out _golemFsm);
    }

    public void DeadTrigger(bool value)
    {
        if (value)
        {
            animator.SetTrigger(deadHash);
        }
    }

    public void SetWalkAniamtion(float speed)
    {
        animator.SetFloat(walkHash, speed);
    }

    public void AttackTrigger(bool value)
    {
        if (value)
            animator.SetTrigger(attackHash);
    }

    public void ThrowRock()
    {
        _golemFsm.RockThrowEvent();
    }
}
