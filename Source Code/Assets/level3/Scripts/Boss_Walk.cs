using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 2f;
    Transform player;
    Rigidbody rb;
    Transform bossT;
    Boss boss;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        bossT = animator.GetComponent<Transform>();
        boss = animator.GetComponent<Boss>();
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.Flip();
        Vector2 target = new Vector3(player. position.x, rb.position.y, player.position.z);
        Vector3 newPos = Vector3.MoveTowards(rb.position ,target, speed * Time.fixedDeltaTime);  //fixedDeltaTime for better behaviour
        newPos = new Vector3(newPos.x, newPos.y, bossT.position.z);
        rb.MovePosition(newPos);
        if(Vector2.Distance(player.position, rb.position) <= attackRange){
            animator.SetTrigger("Attack");
        }
    }

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack"); //Resets the trigger so it won't bug out and repeat the animation
    }
}
