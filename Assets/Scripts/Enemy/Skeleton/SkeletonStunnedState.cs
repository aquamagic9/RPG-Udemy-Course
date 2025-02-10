using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    override public void Enter()
    {
        base.Enter();

        enemy.entityFX.InvokeRepeating("RedColorBlink", 0, 0.1f);
        stateTimer = enemy.stunDuration;
        enemy.rb.linearVelocity = new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    override public void Exit()
    {
        base.Exit();

        enemy.entityFX.Invoke("CancelRedBlink", 0);
    }

    override public void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
