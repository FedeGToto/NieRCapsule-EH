using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhaseOne_Attack2 : State<EnemyController>
{
    public override void EnterState(EnemyController owner)
    {
        Debug.Log("Attack 2");
        owner.GetBulletSpawner().MakeAttack(AttackLibrary.Boss1Attack2());

        owner.GetBulletSpawner().OnAttackFinished += EnemyPhaseOne_Attack2_OnAttackFinished;
    }

    private void EnemyPhaseOne_Attack2_OnAttackFinished(object sender, System.EventArgs e)
    {
        EnemyBulletSpawner parent = sender as EnemyBulletSpawner;

        parent.GetParent().StateMachine.ChangeState(new EnemyIdle());
    }

    public override void ExitState(EnemyController owner)
    {
        owner.GetBulletSpawner().OnAttackFinished -= EnemyPhaseOne_Attack2_OnAttackFinished;
    }

    public override void UpdateState(EnemyController owner)
    {
    }
}
