using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    
    public float swordAttackTimer = 0f;
    private float swordAttackCooldown = 2f;

    private SwordAttack swordAttack;

    public AttackState() : base()
    {
        stateEnum = EnemyStateEnum.Attack;

    }

    private void Awake()
    {
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    public override void OnEnterState()
    {
        statesManager.animationManager.SetBoolForAnimation("isRunning", false);
    }

    public override void OnExitState()
    {

    }

    public override void HandleState()
    {
        if (swordAttackTimer >= swordAttackCooldown)
        {
            OnBeginSwordAttack();
        }

        if (Vector2.Distance(statesManager.player.transform.position, gameObject.transform.position) >= 1)
        {
            statesManager.ChangeState(EnemyStateEnum.Chase);
        }
     // swordAttack.DrawCast();
    }

    private void OnBeginSwordAttack()
    {
        statesManager.animationManager.SetBoolForAnimation("isAttackingSword", true);
    }

    public void OnEndSwordAttack()
    {
        statesManager.animationManager.SetBoolForAnimation("isAttackingSword", false);
        swordAttackTimer = 0f;
    }

    public void ManageSwordAttackCooldown()
    {
        swordAttackTimer += Time.deltaTime;
    }

    public void EnableBoxCastingForSwordAttack()
    {
        swordAttack.SwordAttackBoxCast();
    }

}