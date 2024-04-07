
using UnityEngine;

public class Jump : State
{
  private PlayerController controller;
  private bool hasJump;
  private float colldown;
  public Jump(PlayerController controller) : base("Jump")
  {
    this.controller = controller;
  }
  public override void Enter()
  {
    base.Enter();

    hasJump = false;
    colldown = 1f;
    controller.thisAnimator.SetBool("bJumping", true);

  }


  public override void Exit()
  {
    base.Exit();
    controller.thisAnimator.SetBool("bJumping", false);
  }

  public override void Update()
  {
    base.Update();

    colldown -= Time.deltaTime;

    // Switch to idle
    if (hasJump && controller.isGround && colldown <= 0)
    {
      controller.stateMachine.ChangeState(controller.idleState);
      return;
    }


  }

  public override void LateUpdate() => base.LateUpdate();

  public override void FixedUpdate()
  {
    base.FixedUpdate();
    if (!hasJump)
    {
      hasJump = true;
      ApplyImpulse();
    }

    Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
    walkVector = controller.GetForward() * walkVector;
    walkVector *= controller.movementSpeed * controller.jumpMovimentFactor;

    // Apply Input to Character
    controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

    // Rotate Character
    controller.RotateBodyToFaceInput();
  }

  private void ApplyImpulse()
  {
    // Apply Impulse
    Vector3 forceVector = Vector3.up * controller.forceJump;
    controller.thisRigidbody.AddForce(forceVector, ForceMode.Impulse);
  }
}