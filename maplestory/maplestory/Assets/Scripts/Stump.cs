using UnityEngine;

public class Stump : Enemy
{
    
    public StumpIdleState idleState { get; private set; }
    public StumpMoveState moveState { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        idleState = new StumpIdleState(this, stateMachine, "Idle", this);
        moveState = new StumpMoveState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
