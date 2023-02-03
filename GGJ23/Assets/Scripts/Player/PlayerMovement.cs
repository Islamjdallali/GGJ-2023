using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Speed/Velocity References")]
    public bool isSwinging;
    [SerializeField] private float speed = 5f;
    float XMov;
    float ZMov;
    private Vector3 velocity;

    [Header("Dashing")]
    public float dashCooldown;
    [SerializeField] private bool allowDash;
    [SerializeField] private float dashForce;

    [Header("Cursor/Sensetivity References")]
    [SerializeField] private float lookSensitivity = 3;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Animator pauseMenuAnim;
    private int _rotationMax = 85;
    private float _currentCameraRotationX;
    public bool _isCursorLocked;

    [Header("Jumping")]
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private int numberOfJumps;
    [SerializeField] private bool isAbleToJump;

    [Header("Camera")]
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Camera secondaryCamera;

    [Header("RigidBody")]
    [SerializeField] private Rigidbody rb;

    [Header("Script References")]
    [SerializeField] private Swing swingScript;

    [Header("FX")]
    [SerializeField] private GameObject speedlineGO;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        dashCooldown = 0;
        speedlineGO.SetActive(false);
        //get rigidbody component
        rb = GetComponent<Rigidbody>();
        //get the child camera component from the player (parent)
        fpsCamera = gameObject.GetComponentInChildren<Camera>();
        ToggleCursor();
    }

    // Update is called once per frame
    void Update()
    {
        DashCheck();
        CursorCheck();
        Jump();

        if (rb.velocity.magnitude > 70)
        {
            speedlineGO.SetActive(true);
        }
        else
        {
            speedlineGO.SetActive(false);
        }

        XMov = Input.GetAxis("Horizontal");
        ZMov = Input.GetAxis("Vertical");

        //Assigning the transformation positions for the x and z axis
        Vector3 moveRight = transform.right * XMov;
        Vector3 moveForward = transform.forward * ZMov;

        velocity = (moveRight + moveForward) * speed;

        //Camera Rotation (looking left and right)
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotationX = new Vector3(0f, yRot, 0) * lookSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationX));

        //Camera Rotation (looking up and down)

        float xRot = Input.GetAxisRaw("Mouse Y");
        float rotationY = xRot * lookSensitivity;
        _currentCameraRotationX -= rotationY;

        //Clamp the cameras Y rotation so that the player's Y rotation is see up to +- 85 degrees
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_rotationMax, _rotationMax);
        fpsCamera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
        secondaryCamera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumps > 0 && !isSwinging)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(jumpForce, ForceMode.Impulse);
            numberOfJumps--;
        }

        if (isSwinging)
        {
            numberOfJumps = 2;
        }
    }

    void DashCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            allowDash = true;
        }
        else if (dashCooldown > 0 || !Input.GetKey(KeyCode.LeftShift))
        {
            allowDash = false;
        }

        if (allowDash)
        {
            Vector3 dashDir = transform.forward.normalized;

            swingScript.StopSwing();

            rb.velocity = new Vector3(0, 0, 0);

            rb.AddForce(dashDir * dashForce, ForceMode.Impulse);
            dashCooldown = 1;
        }
        else if (!allowDash)
        {
            dashCooldown -= Time.deltaTime;

            if (dashCooldown <= 0)
            {
                dashCooldown = 0;
            }
        }
    }

    void CursorCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursor();
        }

        if (_isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuAnim.Play("PauseOut");
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
        }
    }

    void ToggleCursor()
    {
        _isCursorLocked = !_isCursorLocked;
    }

    private void FixedUpdate()
    {
        //allow movement to happen
        if (!isSwinging)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
    }
}
