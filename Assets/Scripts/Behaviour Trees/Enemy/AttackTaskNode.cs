using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class AttackTaskNode : Node
{
    private Transform transform;
    private Transform lastTarget;
    private Player player;

    private float attackTime = 1f;
    private float attackCounter = 0f;

    private float ShootTime = 0.5f;
    private float timeBetweenShots = 0f;

    public AttackTaskNode(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;
        Transform target = (Transform)GetData("target");
        if (target != lastTarget)
        {
            player = target.GetComponent<Player>();
            lastTarget = target;
        }
        transform.LookAt(target.position);
        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            timeBetweenShots += Time.deltaTime;
            if (timeBetweenShots >= ShootTime)
            {
                transform.LookAt(target.position);
                Physics.Raycast(this.transform.position, this.transform.forward, out hit);
                Player player = hit.transform.GetComponent<Player>();
                if (player != null)
                {
                    bool PlayerIsDead = player.TakeDamage();
                    if (PlayerIsDead)
                    {
                        timeBetweenShots = 0f;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    else
                    {
                        attackCounter = 0f;
                        timeBetweenShots = 0f;
                    }
                }
            }
        }

        State = NodeState.RUNNING;
        return State;
    }
}
