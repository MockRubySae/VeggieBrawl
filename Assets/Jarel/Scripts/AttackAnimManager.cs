using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackAnimManager : MonoBehaviour
{
    public Attack attacksManager;

    public Animator playerAnimator;

    public bool isAttackFinished = true;
    //private int activeLimb = 0; //for fists; 0 - right; 1 - left
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isAttackFinished)
            {
                isAttackFinished = false;
                if (attacksManager.isFists)
                {
                    if (activeLimb == 0)
                    {
                        playerAnimator.Play("player_attackFistRight");
                        activeLimb = 1;
                    }
                    else if (activeLimb == 1)
                    {
                        playerAnimator.Play("player_attackFistLeft");
                        activeLimb = 0;
                    }
                }
                else if (attacksManager.isPitchFork)
                {
                    playerAnimator.Play("player_attackHoe");
                }
                else if (attacksManager.isCarrotSpear)
                {
                    playerAnimator.Play("player_attackSpear");
                }
            }
        }
        */
    }

    void AttackEnd() //keyframe event
    {
        isAttackFinished = true;
        Debug.Log("Attack has ended");
    }

    void FistsAttack()
    {
        attacksManager.Fists();
    }

    void HoeAttack()
    {
        attacksManager.PitchFork();
    }

    void SpearAttack()
    {
        attacksManager.CarrotSpear();
    }

}
