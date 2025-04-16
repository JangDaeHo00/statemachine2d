using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttaacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttaacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1;

        float attackDir = player.facingDir;

        if(xInput != 0)
            attackDir = xInput;

        // 적한테 넉백을 준다던가, 앞으로 공격을 할 수 있게 될 수 있다.
        // x, y 값을 정해줘서 플레이어가 공격할 때 x, y 방향으로 조금씩 움직이게 할 수 있다.
        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);

        stateTimer = 0.1f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", 0.1f);

        comboCounter++;
        lastTimeAttaacked = Time.time;
    }

    
}
