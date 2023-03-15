using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhaseOne_Attack3 : State<EnemyController>
{
    public override void EnterState(EnemyController owner)
    {
        Debug.Log("Attack 3");
        owner.GetBulletSpawner().MakeAttack(AttackLibrary.Boss1Attack3());

        owner.GetBulletSpawner().OnAttackFinished += EnemyPhaseOne_Attack3_OnAttackFinished;
    }

    private void EnemyPhaseOne_Attack3_OnAttackFinished(object sender, System.EventArgs e)
    {
        EnemyBulletSpawner parent = sender as EnemyBulletSpawner;

        parent.GetParent().StateMachine.ChangeState(new EnemyIdle());
    }

    public override void ExitState(EnemyController owner)
    {
        owner.GetBulletSpawner().OnAttackFinished -= EnemyPhaseOne_Attack3_OnAttackFinished;
    }

    public override void UpdateState(EnemyController owner)
    {
    }
}
