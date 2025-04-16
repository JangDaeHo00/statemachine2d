using UnityEngine;

public class EnemyState
{
    //상속받은 애들이 쓸수있어야하니까 프로텍티드로
    //스테이트 머신과 Enemy를 선언해주기
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected bool triggerCalled;
    private string animBoolName;

    //상태의 지속시간 설정
    protected float stateTimer;

    //상태의 구성요소를 생성자로 받아주기
    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemy.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }


    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

}