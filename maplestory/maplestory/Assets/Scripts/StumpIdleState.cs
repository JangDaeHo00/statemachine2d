using UnityEngine;

public class StumpIdleState : EnemyState
{
    private Stump enemy;

    public StumpIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Stump _enemy) : base(_enemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        //idleTime이 0보다 작아지면 바로 이동상태로 전환
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
