using UnityEngine;

public class StumpMoveState : EnemyState
{
    private Stump enemy;

    public StumpMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Stump _enemy) : base(_enemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("Move", true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("Move", false);
    }

    public override void Update()
    {
        base.Update();

        //바라보는 방향 따라서 이동해잇
        //SetVelocity에 FlipController가 들어가있었기 때문에 발작
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rb.linearVelocity.y);

        if(enemy.IsWallDetected())
        {
            enemy.Flip();
        }

    }
}
