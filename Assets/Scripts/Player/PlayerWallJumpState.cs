using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f;
        player.SetVelocity(-player.facingDir * 5f, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
