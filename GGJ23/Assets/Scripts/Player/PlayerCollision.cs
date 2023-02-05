using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isDead;

    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject deathCamera;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject speedlineGO;
    [SerializeField] private GameObject predictionPoint;

    [Header("SFX")]
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private AudioSource windSFX;

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
        if ((collision.collider.gameObject.layer != 7
            && collision.collider.gameObject.layer != 8))
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("deathwall"))
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
        deathSFX.Play();
        playerMovementScript.enabled = false;
        predictionPoint.SetActive(false);
        speedlineGO.SetActive(false);
        swingScript.enabled = false;
        playerRenderer.enabled = false;
        rb.velocity = new Vector3(0, 0, 0);
        rb.useGravity = false;
        deathFX.SetActive(true);
        deathCamera.SetActive(true);
        this.gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(PlayerDeath());
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(2);
        deathSFX.Stop();
        deathCanvas.SetActive(true);
    }
}
