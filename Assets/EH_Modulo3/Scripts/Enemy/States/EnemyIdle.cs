using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : State<EnemyController>
{
    private float nextPhaseTimer;

    private const float PHASE2HP = 0.5f;
    private const float PHASE1TIMERMAX = 10f;
    private const float PHASE1TIMERMIN = 7f;

    private const float PHASE2TIMERMAX = 3f;
    private const float PHASE2TIMERMIN = 1f;


    public EnemyIdle()
    {

    }


    public override void EnterState(EnemyController owner)
    {
        Debug.Log("Entering Idle");

        float healthPercentile = owner.GetHealth() / owner.GetMaxHealth();
        bool isPhase2 = healthPercentile < PHASE2HP;
        nextPhaseTimer = isPhase2 ? Random.Range(PHASE2TIMERMIN, PHASE2TIMERMAX) : Random.Range(PHASE1TIMERMIN, PHASE1TIMERMAX);

        Debug.Log($"Next attack in {nextPhaseTimer} seconds.");
    }

    public override void ExitState(EnemyController owner)
    {
        
    }

    public override void UpdateState(EnemyController owner)
    {
        nextPhaseTimer -= Time.deltaTime;
        if (nextPhaseTimer <= 0)
        {
            Debug.Log($"Going to next attack");

            owner.StateMachine.ChangeState(GetNextState(owner));
        }
    }

    private State<EnemyController> GetNextState(EnemyController owner)
    {
        List<State<EnemyController>> nextStateList = new List<State<EnemyController>>();

        float healthPercentile = owner.GetHealth() / owner.GetMaxHealth();

        nextStateList.Add(new EnemyPhaseOne_Attack1()); // Attack 1
        nextStateList.Add(new EnemyPhaseOne_Attack2()); // Attack 2
        nextStateList.Add(new EnemyPhaseOne_Attack3()); // Attack 3
        nextStateList.Add(new EnemyPhaseOne_Attack4()); // Attack 4

        // Phase 2
        if (healthPercentile <= PHASE2HP)
        {
            nextStateList.Add(new EnemyPhaseOne_Attack5()); // Attack 5
            nextStateList.Add(new EnemyPhaseOne_Attack6()); // Attack 6
        }

        return nextStateList[Random.Range(0, nextStateList.Count)];
    }
}
