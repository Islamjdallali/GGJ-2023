using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform swingTip;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGrappleable;

    [Header("Swinging")]
    [SerializeField] private float maxSwingDistance = 25f;
    [SerializeField] Vector3 swingPoint;
    [SerializeField] SpringJoint joint;

    [Header("SFX")]
    [SerializeField] private AudioSource grappleSFX;

    private Vector3 currentGrapplePosition;

    [Header("SwingMovement")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float horizThrustForce;
    [SerializeField] private float forwardThrustForce;
    [SerializeField] private float extendCableSpeed;

    [Header("Scrip Reference")]
    [SerializeField] private PlayerMovement playerMovementScript;

    [Header("Prediction")]
    [SerializeField] private RaycastHit predictionHit;
    [SerializeField] private float predictionSphereCastRadius;
    [SerializeField] private Transform predictionPoint;

    [Header("Rope Anim")]
    [SerializeField] private int quality;
    [SerializeField] private float damper;
    [SerializeField] private float strength;
    [SerializeField] private float velocity;
    [SerializeField] private float waveCount;
    [SerializeField] private float waveHeight;
    [SerializeField] private AnimationCurve effectCurve;
    [SerializeField] private Spring springScript;

    [Header("Highlight")]
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material highlightMat;

    private void Awake()
    {
        springScript = new Spring();
        springScript.SetTarget(0);
    }

    // Update is called once per frame
    void Update()
    {
        StartSwing();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopSwing();
        }

        if (joint != null)
        {
            SwingMovement();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartSwing()
    {
        CheckForSwingPoint();

        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerMovementScript.isSwinging = true;

            grappleSFX.Play();

            predictionPoint.gameObject.SetActive(false);

            swingPoint = predictionHit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            currentGrapplePosition = swingTip.position;
        }
    }

    void CheckForSwingPoint()
    {
        if (joint != null)
        {
            return;
        }

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward, out sphereCastHit, maxSwingDistance, whatIsGrappleable);

        RaycastHit raycastHit;
        Physics.Raycast(cam.position,cam.forward,out raycastHit,maxSwingDistance, whatIsGrappleable);

        Vector3 realHitPoint;

        if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        }
        else if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        else
        {
            realHitPoint = Vector3.zero;
        }

        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.transform.position = realHitPoint;
        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

    public void StopSwing()
    {
        playerMovementScript.isSwinging = false;

        lr.positionCount = 0;
        Destroy(joint);
    }

    void SwingMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(orientation.right * horizThrustForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-orientation.right * horizThrustForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            float extendDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;

            joint.maxDistance = extendDistanceFromPoint * 0.8f;
            joint.minDistance = extendDistanceFromPoint * 0.25f;
        }
    }

    void DrawRope()
    {
        if (!joint)
        {
            springScript.Reset();
            if (lr.positionCount > 0)
            {
                lr.positionCount = 0;
            }

            return;
        }

        if (lr.positionCount == 0)
        {
            springScript.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }

        springScript.SetDamper(damper);
        springScript.SetStrength(strength);
        springScript.Update(Time.deltaTime);

        Vector3 grapplePoint = swingPoint;
        Vector3 grappleTipPoint = swingTip.position;
        Vector3 right = Quaternion.LookRotation((grapplePoint - grappleTipPoint).normalized) * Vector3.right;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        for (int i = 0; i < quality + 1; i++)
        {
            float delta = i / (float)quality;
            Vector3 offset = right * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI * springScript.Value * effectCurve.Evaluate(delta));
            lr.SetPosition(i, Vector3.Lerp(swingTip.position, currentGrapplePosition,delta) + offset);
        }
    }
}
