using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);

        if (player.IsGroundDetected() || !player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
        
        if (yInput < 0)
            player.SetVelocity(0, rb.linearVelocityY);
        else
            player.SetVelocity(0, rb.linearVelocityY * 0.7f);
        
    }
}
