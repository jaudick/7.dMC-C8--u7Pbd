using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRigidbody : MonoBehaviour
{
    float forwardSpeed = 12.5f;
    float sideToSideSpeed = 11f;
    float backSpeed = 10f;
    private float targetSpeed = 0f;
    public bool canDoInput = true;
    public PlayerAudio playerAudio;


    public bool isGrounded = true;
    public bool tempIsGrounded = true;
    [SerializeField] Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask ground;
    public Rigidbody rbody;
    private const float jumpForce = 1400f;
    private const float slideForce = 90f;
    private const float dashForce = 37.5f;
    private bool canDash = true;
    private bool dashing = false;
    private bool canSlide = true;
    Vector3 move;
    public bool canJumpAgain = true;
    private bool holdingSlideTriggerInAir = false;


    [Header("Parkour")]
    //public GameObject lastWall1;
    //public GameObject lastWall2;
    public GameObject lastWall;
    public GameObject currentWall;
    //public int oneOrTwoSwitchForWalls= 1;
    //public int oneOrTwoSwitchForNormalVectors = 1;
    public bool isWallRunning;
    public bool isWallRunningRight;
    public bool isWallRunningLeft;
    private const float wallRunUpForce = 12f;
    public float currentWallRunUpForce = 0f;
    private const float wallRunDecreaseRate = 25f;
    private const float jumpOffWallUpForce = 30.5f;
    private const float jumpOffWallForwardForce = 23.5f;
    public bool justJumpedOffWall = false;
    [SerializeField] private Animator headCamera;
    [SerializeField] private CapsuleCollider capsuleCollider; 
    public Vector3 wallRunVelocity;
    public WallRunBaseBox wallRunBaseBox;
    public GameObject wallRunRig;
    public Quaternion rigRotation;
    public bool getNextWall = true;
    //private Vector3 jumpedOfWallVelocity = Vector3.zero;
    public bool lastFrameWasHoldingRightTigger = false;
    public bool wait = false;
    public bool isTouchingWall;

    [Header("Enemy Parkour")]
    [SerializeField] private BulletEnemyJumpBox bulletEnemyJumpBox;
    private float jumpOffEnemyUpForce = 45f;
    private float jumpOffEnemySpeedBoost = 2.5f;
    private bool justJumpedOffEnemy = false;

    void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        getNextWall = true;
        wait = false;
        isTouchingWall = false;
    }

    void Update()
    {
        Collider[] walls = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, 1 << 10);
        if(walls.Length==1)
        {
            isTouchingWall = true;
        }
        else
        {
            isTouchingWall = false;
        }
        if (!GameCanvas.paused)
        {
            if(Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.F3) || Input.GetAxis("RespawnController") < 0)
            {
                CheckPointManager.instance.Respawn();
            }
            if (isWallRunningRight)
            {
                justJumpedOffEnemy = false;
                justJumpedOffWall = false;
                canDoInput = true;
                headCamera.SetBool("Right", true);
            }
            else if (isWallRunningLeft)
            {
                justJumpedOffEnemy = false;
                justJumpedOffWall = false;
                canDoInput = true;
                headCamera.SetBool("Left", true);
            }
            else
            {
                headCamera.SetBool("Right", false);
                headCamera.SetBool("Left", false);
            }

            if (isWallRunning && !isGrounded && !justJumpedOffWall)
            {
                if ((!KeyBindingManager.instance.HOLD_WALL && ((Input.GetKeyDown(KeyBindingManager.instance.JUMP) || (Input.GetAxis("JumpController") > 0))) ||
                    (KeyBindingManager.instance.HOLD_WALL && ((Input.GetKeyUp(KeyBindingManager.instance.JUMP) || (Input.GetAxis("JumpController") <= 0 && lastFrameWasHoldingRightTigger))) && canJumpAgain)))
                {
                    lastWall = currentWall;
                    float x = Input.GetAxisRaw("Horizontal");
                    float z = Input.GetAxisRaw("Vertical");

                    if (Mathf.Abs(x) > 0 && (Mathf.Abs(x) > Mathf.Abs(z / 2)))
                    {
                        Vector3 jumpOffWallSideForce = isWallRunningRight ? -transform.right : transform.right;
                        rbody.velocity = jumpOffWallForwardForce / 50 * transform.forward + transform.up * jumpOffWallUpForce * 1.55f + jumpOffWallSideForce * 10f;
                    }
                    else
                    {
                        Vector3 jumpOffWallSideForce = isWallRunningRight ? -transform.right : transform.right;
                        rbody.velocity = jumpOffWallForwardForce * transform.forward + transform.up * jumpOffWallUpForce + jumpOffWallSideForce * 2.5f;
                    }

                    playerAudio.PlayJump();
                    wallRunRig.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    currentWallRunUpForce = wallRunUpForce;
                    justJumpedOffWall = true;
                    getNextWall = true;

                    isWallRunning = false;
                    isWallRunningLeft = false;
                    isWallRunningRight = false;
                    canDash = true;

                    StartCoroutine(ResetJump());
                }
            }

            else if ((Input.GetKeyDown(KeyBindingManager.instance.JUMP) || Input.GetAxis("JumpController") > 0) && (isGrounded || tempIsGrounded) && canJumpAgain)
            {
                playerAudio.PlayJump();
                rbody.AddForce(0, jumpForce * 2, 0, ForceMode.Impulse);
                canJumpAgain = false;
                StartCoroutine(ResetJump());
            }

            if ((isGrounded && (Input.GetKeyDown(KeyBindingManager.instance.SLIDE) || ((Input.GetAxis("SlideController") > 0) && !holdingSlideTriggerInAir)) && !isWallRunning && canSlide))
            {
                capsuleCollider.height = 0.5f;
                capsuleCollider.center = new Vector3(0, 0.25f, 0);
                rbody.velocity = (transform.forward.normalized * slideForce / 2) + -transform.up * 5f;
                StartCoroutine(Sliding());
            }

            else if ((Input.GetKey(KeyBindingManager.instance.SLIDE) || Input.GetAxis("SlideController") > 0) && !isWallRunning && !isGrounded)
            {
                rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y - 100f * Time.deltaTime, rbody.velocity.z);
                holdingSlideTriggerInAir = Input.GetAxis("SlideController") > 0;
            }

            if (Input.GetAxis("SlideController") <= 0 && isGrounded)
                holdingSlideTriggerInAir = false;

            if ((Input.GetKeyDown(KeyBindingManager.instance.DASH_LEFT) || Input.GetButtonDown("DashLeftController")) && canDash && !isWallRunning)
            {
                if (canSlide) headCamera.SetBool("DashLeft", true);
                rbody.velocity = -transform.right.normalized * dashForce + transform.forward.normalized * 1.5f + move;
                StartCoroutine(Dashing("DashLeft"));
            }

            if ((Input.GetKeyDown(KeyBindingManager.instance.DASH_RIGHT) || Input.GetButtonDown("DashRightController")) && canDash && !isWallRunning)
            {
                if (canSlide) headCamera.SetBool("DashRight", true);
                rbody.velocity = transform.right.normalized * dashForce + transform.forward.normalized * 1.5f + move;
                StartCoroutine(Dashing("DashRight"));
            }

            if (bulletEnemyJumpBox.canJumpOffEnemy && !isGrounded && !isWallRunning && ((Input.GetKey(KeyBindingManager.instance.JUMP) || Input.GetAxis("JumpController") > 0) && !justJumpedOffEnemy && canJumpAgain))
            {
                playerAudio.PlayJumpOffEnemy();
                ResetWallRun();
                rbody.velocity = transform.up * jumpOffEnemyUpForce;
                StartCoroutine(JustJumpedOffEnemy());
                StartCoroutine(ResetJump());
            }

            if (isGrounded)
            {
                wallRunRig.transform.localRotation = Quaternion.Euler(0, 0, 0);
                justJumpedOffEnemy = false;
                canDoInput = true;
                getNextWall = true;
                ResetWallRun();
            }
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float resultSpeedBasedOnDirection = ProcessMovment(x, z);
        move = (transform.right * x + transform.forward * z).normalized;

        if ((z != 0 || x != 0) && isGrounded)
            headCamera.SetBool("Running", true);
        else
            headCamera.SetBool("Running", false);

        if (justJumpedOffWall && !dashing)
        {
            Vector3 jumpedOffWallAirSpeed = new Vector3(move.x * Time.deltaTime * 100f * resultSpeedBasedOnDirection, 0, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            rbody.velocity = new Vector3(jumpedOffWallAirSpeed.x * 1.5f, rbody.velocity.y - 20f * Time.deltaTime, jumpedOffWallAirSpeed.z * 1.5f); //+ jumpedOffWallAirSpeed;
        }

        else if (canDoInput && !dashing)
        {
            if (isGrounded && !isWallRunning) 
            {
                rbody.velocity = new Vector3(move.x * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            }

            else if (!isGrounded && !isWallRunning && justJumpedOffEnemy)//enemy jump air speed
            {
                move = (transform.right * x *0.75f + transform.forward * z * 1.25f).normalized;
                rbody.velocity = new Vector3(move.x * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection * jumpOffEnemySpeedBoost, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection * jumpOffEnemySpeedBoost);
            }

            else if (!isGrounded && !isWallRunning)//air speed
            {
                rbody.velocity = new Vector3(move.x * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            }

            else if (isWallRunning)
            {
                wallRunRig.transform.rotation = rigRotation;
                if (isWallRunningRight && z > float.Epsilon)
                {
                    rbody.velocity = new Vector3(wallRunVelocity.x * 2f, currentWallRunUpForce, wallRunVelocity.z * 2f) + transform.right.normalized * 2;
                }
                else if(isWallRunningLeft && Mathf.Abs(z) > float.Epsilon)
                {
                    rbody.velocity = new Vector3(wallRunVelocity.x * 2f, currentWallRunUpForce, wallRunVelocity.z * 2f) + -transform.right.normalized * 2;
                }
                currentWallRunUpForce -= wallRunDecreaseRate * Time.fixedDeltaTime;
            }

            if (!isWallRunning)
            {
                getNextWall = true;
                wallRunRig.transform.localRotation = Quaternion.Euler(0,0,0);
                currentWallRunUpForce = wallRunUpForce;
            }
        }
    }

    public void ResetWallRun()
    {
        canDoInput = true;
        isWallRunning = false;
        isWallRunningLeft = false;
        isWallRunningRight = false;
        SetLastWalls(null);
        justJumpedOffWall = false;
        getNextWall = true;
    }

    public float ProcessMovment(float x = 0f, float z = 0f)
    {
        if (x > 0.1f || x < -0.1f)
        {
            targetSpeed = sideToSideSpeed;
        }

        if (z < -0.1f)
        {
            targetSpeed = backSpeed;
        }

        if (z > 0.1f)
        {
            targetSpeed = forwardSpeed;
        }

        targetSpeed = isWallRunning ? targetSpeed * 1.7f : justJumpedOffWall ? targetSpeed * 1.2f : !isGrounded ? targetSpeed * 0.9f : targetSpeed;
        return targetSpeed;
    }
    IEnumerator ChangeCanDoInput()
    {
        canDoInput = false;
        yield return new WaitForSeconds(0.2f);
        canDoInput = true;
    }

    IEnumerator Sliding()
    {
        playerAudio.PlaySlide();
        canSlide = false;
        headCamera.SetTrigger("Slide");
        canDoInput = false;
        yield return new WaitForSeconds(1f);
        canDoInput = true;
        capsuleCollider.height = 2f;
        capsuleCollider.center = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        canSlide = true;
    }

    IEnumerator Dashing(string direction)
    {
        playerAudio.PlayDash();
        dashing = true;
        canDash = false;
        yield return new WaitForSeconds(0.3f);
        headCamera.SetBool(direction, false);

        dashing = false;

        yield return new WaitForSeconds(0.35f);
        canDash = true;
    }

    public Vector3 GetVelocity()
    {
        return transform.forward.normalized * 15f;
    }

    public void SetLastWalls(GameObject wall)
    {
        lastWall = wall;
    }

    private IEnumerator JustJumpedOffEnemy()
    {
        justJumpedOffEnemy = true;
        yield return new WaitForSeconds(1f);
        justJumpedOffEnemy = false;
    }

    private IEnumerator ResetJump()
    {
        canJumpAgain = false;
        yield return new WaitForSeconds(0.2f);
        canJumpAgain = true;
    }

    public void Wait()
    {
        StartCoroutine(WaitCo());
    }

    private IEnumerator WaitCo()
    {
        wait = true;
        yield return new WaitForSeconds(0.2f);
        wait = false;
    }
}


