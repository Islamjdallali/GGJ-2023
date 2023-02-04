using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isDead;

    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject deathCamera;
    [SerializeField] private GameObject deathFX;

    private MeshRenderer playerRenderer;
    private Rigidbody rb;

    [Header("Script References")]
    [SerializeField] private ScoreCounter scoreCounterScript;
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private Swing swingScript;

    // Start is called before the first frame update
    void Start()
    {
        deathCanvas.SetActive(false);
        deathCamera.SetActive(false);
        deathFX.SetActive(false);

        playerRenderer = gameObject.GetComponent<MeshRenderer>();
        rb = gameObject.GetComponent<Rigidbody>();

        playerRenderer.enabled = true;
        rb.useGravity = true;

        isDead = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer != 7
            && collision.collider.gameObject.layer != 8)
        {
            isDead = true;
            playerMovementScript.enabled = false;
            swingScript.enabled = false;
            playerRenderer.enabled = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.useGravity = false;
            deathFX.SetActive(true);
            deathCamera.SetActive(true);
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(2);
        deathCanvas.SetActive(true);
    }
}
