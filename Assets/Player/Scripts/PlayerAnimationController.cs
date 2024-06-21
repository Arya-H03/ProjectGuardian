using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] PlayerFootSteps footSteps;
    private PlayerController playerController;

    [SerializeField] RuntimeAnimatorController withSwordAC;
    [SerializeField] RuntimeAnimatorController withoutSwordAC;


    [SerializeField] GameObject LeftHand;

    private string [] animConditions = {"isRunning","isJumping","isHit"};   

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void ChangeAnimatorAC(bool hasSword)
    {
        if(hasSword)
        {
            animator.runtimeAnimatorController = withSwordAC;
        }

        if (!hasSword)
        {
            animator.runtimeAnimatorController = withoutSwordAC;
        }
        
    }

    public void SetBoolForAnimations(string name,bool value)
    {
        animator.SetBool(name,value);
    }

    public void SetTriggerForAnimations(string name)
    {
        animator.SetTrigger(name);
    }

    public void ManageParryAttack()
    {
        SetBoolForAnimations("isParrySuccessful", false);
    }

    public void EndSwordAttack()
    {
        playerController.PlayerSwordAttackState.EndAttack();
    }

   
    public void BoxCastForFirstAttackSwing()
    {
        playerController.PlayerSwordAttackState.FirstSwingAttack();
    }

    public void BoxCastForSecondAttackSwing()
    {
        playerController.PlayerSwordAttackState.SecondSwingAttack();
    }
    public void OnParryEnd()
    {
        playerController.PlayerParryState.OnParryEnd();
    }
    public void ActivateParryShield()
    {
        playerController.PlayerParryState.ActivateParryShield();
    }

    public void EndAnimations(string name)
    {
        foreach(string condition in animConditions)
        {
            if(condition != name)
            {
                animator.SetBool(condition, false);
            }      
        }
    }

    public void SetPlayerFallStatus()
    {
        playerController.PlayerJumpState.SetPlayerFallStatus();
    }

    
}
