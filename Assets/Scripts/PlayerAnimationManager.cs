using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    public Animator animator;

    public PlayerStateMachine stateMachine;

    private const string PARAM_IS_MOVEING = "IsMoveing";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string PARAM_IS_Falling = "IsFalling";
    private const string PARAM_IS_ATTACK_TRIGGER = "ATTACK";

    public void Update()
    {
        UpdateAnimationState();
    }


    private void UpdateAnimationState()
    {
        if (stateMachine.currentState != null)
        {

            ResetAllBoolParameters();

            switch (stateMachine.currentState)
            {
                case IdleState:
                    break;

                case MoveingState:
                    animator.SetBool(PARAM_IS_MOVEING, true);

                    if (Input.GetKey(KeyCode.LeftShift))
                    {

                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;
                case JumpingState:
                    animator.SetBool(PARAM_IS_JUMPING, true);
                    break;
                case FallingState:
                    animator.SetBool(PARAM_IS_Falling, true);
                    break;
            }

        }
    }

        public void TriggerAttack()
        {
            animator.SetTrigger(PARAM_IS_ATTACK_TRIGGER);
        }


        private void ResetAllBoolParameters()
        {
            animator.SetBool(PARAM_IS_MOVEING, false);
            animator.SetBool(PARAM_IS_RUNNING, false);
            animator.SetBool(PARAM_IS_JUMPING, false);
            animator.SetBool(PARAM_IS_Falling, false);
        }

    }

