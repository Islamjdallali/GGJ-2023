using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] paths;
    [SerializeField] private float timeTillDeath;
    [SerializeField] private Vector3 offset;

    [SerializeField] private int randNo;

    private void Start()
    {
        randNo = Random.Range(0, paths.Length);
    }

    // Update is called once per frame
    void Update()
    {
        timeTillDeath -= Time.deltaTime;
        if (timeTillDeath <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(paths[1], transform.position + offset, Quaternion.identity);
    }
}
