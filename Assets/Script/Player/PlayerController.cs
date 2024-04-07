using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public propeties
    public float movementSpeed = 10f;
    public float forceJump = 10f;
    public float jumpMovimentFactor = 1f;
    [HideInInspector] public bool isGround = false;


    // State machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public Dead deadState;

    // Internal Propeties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Collider thiscollider;
    [HideInInspector] public Animator thisAnimator;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisAnimator = GetComponent<Animator>();
        thiscollider = GetComponent<CapsuleCollider>();


    }
    // Start is called before the first frame update
    void Start()
    {
        // StateMachine and this state
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);// mudar o status
    }

    // Update is called once per frame
    void Update()
    {
        // Dead
        if (GameManager.Instance.isGameOver)
        {
            if (stateMachine.currentStateName != deadState.name)
            {
                stateMachine.ChangeState(deadState);
            }
        }

        // Create Inpu Vector 
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        movementVector = new Vector2(inputX, inputY);
        hasJumpInput = Input.GetKey(KeyCode.Space);

        float velocity = thisRigidbody.velocity.magnitude; // pegar a velocidade de acardo com o rigidy body ou melho com a fisica do jogo
        float velocityRate = velocity / movementSpeed; // divide o valor pela velocidade estabelecidade para determinar o limite
        // Passar a velocidade de 0 a 1 para o animator controler
        thisAnimator.SetFloat("fVelocity", velocityRate); // mudar o float no animator para a velocidade que adiquirimos com a fisica do player

        // StateMachine
        stateMachine.Update();
    }

    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }
    void FixedUpdate()
    {
        DetectionGround();
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput()
    {

        if (movementVector.IsZero())
        {
            return;
        }
        // Calculate rotation
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

        //apply Rotation
        thisRigidbody.MoveRotation(newRotation);
    }

    // void OnGUI()
    // {
    //     Rect rect = new Rect(10, 10, 100, 100);
    //     string text = stateMachine.currentStateName;
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = (int)(50f * (Screen.width / 1920f));
    //     GUI.Label(rect, text, style);
    // }
    private void DetectionGround()
    {

        //Reset Is Grounded
        isGround = false;

        // Detect Ground
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thiscollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.66f;

        if (Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance))
        {
            GameObject hitObject = hitInfo.transform.gameObject;

            if (hitObject.CompareTag("Plataform"))
            {
                isGround = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thiscollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.25f;
        Vector3 sheprePosition = direction * maxDistance + origin;
        Gizmos.color = isGround ? Color.magenta : Color.cyan;
        Gizmos.DrawSphere(sheprePosition, radius);

    }
}
