using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Speed/Velocity References")]
    [SerializeField] private float speed = 5f;
    float XMov;
    float ZMov;
    private Vector3 velocity;

    [Header("Running")]
    public float runCooldown;
    [SerializeField] private bool allowSprint;

    [Header("Cursor/Sensetivity References")]
    [SerializeField] private float lookSensitivity = 3;
    private int _rotationMax = 85;
    private float _currentCameraRotationX;
    private bool _isCursorLocked;

    [Header("Camera")]
    [SerializeField] private Camera fpsCamera;

    [Header("RigidBody")]
    [SerializeField] private Rigidbody rb;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        runCooldown = 10;
        //get rigidbody component
        rb = GetComponent<Rigidbody>();
        //get the child camera component from the player (parent)
        fpsCamera = gameObject.GetComponentInChildren<Camera>();
        ToggleCursor();
    }

    // Update is called once per frame
    void Update()
    {
        RunCheck();
        CursorCheck();

        XMov = Input.GetAxis("Horizontal");
        ZMov = Input.GetAxis("Vertical");

        //Assigning the transformation positions for the x and z axis
        Vector3 moveHorizontal = transform.right * XMov;
        Vector3 moveVertical = transform.forward * ZMov;

        velocity = (moveHorizontal + moveVertical) * speed;

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
    }

    void RunCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift) && runCooldown > 0)
        {
            allowSprint = true;
        }
        else if (runCooldown <= 0 || !Input.GetKey(KeyCode.LeftShift))
        {
            allowSprint = false;
        }

        if (allowSprint)
        {
            speed = 10;
            runCooldown -= 5 * Time.deltaTime;
        }
        else if (!allowSprint)
        {
            speed = 5;
            runCooldown += Time.deltaTime;

            if (runCooldown >= 10)
            {
                runCooldown = 10;
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
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void ToggleCursor()
    {
        _isCursorLocked = !_isCursorLocked;
    }

    private void FixedUpdate()
    {
        //allow movement to happen
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
}
