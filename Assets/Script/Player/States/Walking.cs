
using System;
using System.Reflection.Emit;
using UnityEngine;

public class Walking : State
{
  private PlayerController controller;
  public Walking(PlayerController controller) : base("walking")
  {
    this.controller = controller;
  }
  public override void Enter()
  {
    base.Enter();
  }

  public override void Exit()
  {
    base.Exit();
  }

  public override void Update() //para lidar com logica
  {
    base.Update();

    if (controller.hasJumpInput)
    {
      controller.stateMachine.ChangeState(controller.jumpState);
      return;
    }
    // Switch to Walking
    if (controller.movementVector.IsZero())
    {
      controller.stateMachine.ChangeState(controller.idleState);
      return;
    }

  }

  public override void LateUpdate()
  {
    base.LateUpdate();
  }

  public override void FixedUpdate() // para lidar com fisica
  {
    base.FixedUpdate();

    Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
    walkVector = controller.GetForward() * walkVector;
    walkVector *= controller.movementSpeed;

    // Apply Input to Character
    controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

    // Rotate Character
    controller.RotateBodyToFaceInput();
  }


}