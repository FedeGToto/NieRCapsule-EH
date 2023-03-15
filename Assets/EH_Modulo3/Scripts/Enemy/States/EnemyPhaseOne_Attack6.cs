using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhaseOne_Attack6 : State<EnemyController>
{
    public override void EnterState(EnemyController owner)
    {
        Debug.Log("Attack 6");
        owner.GetBulletSpawner().MakeAttack(AttackLibrary.Boss1Attack6());

        owner.GetBulletSpawner().OnAttackFinished += EnemyPhaseOne_Attack6_OnAttackFinished;
    }

    private void EnemyPhaseOne_Attack6_OnAttackFinished(object sender, System.EventArgs e)
    {
        EnemyBulletSpawner parent = sender as EnemyBulletSpawner;

        parent.GetParent().StateMachine.ChangeState(new EnemyIdle());
    }

    public override void ExitState(EnemyController owner)
    {
        owner.GetBulletSpawner().OnAttackFinished -= EnemyPhaseOne_Attack6_OnAttackFinished;
    }

    public override void UpdateState(EnemyController owner)
    {
    }
}
