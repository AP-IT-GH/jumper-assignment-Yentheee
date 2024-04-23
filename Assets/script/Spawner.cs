using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab = null;
    public Transform spawn = null;
    public float minTime = 1.0f;
    public float maxTime = 3.0f;
    public bool rotate90Degrees = true;

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallEnd"))
        {
            Destroy(gameObject); // Verwijder het huidige obstakel
        }
    }
    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            if (prefab != null && spawn != null)
            {
                GameObject go = Instantiate(prefab, spawn.position, Quaternion.identity);

                if (rotate90Degrees)
                {
                    go.transform.Rotate(Vector3.up, -90f);
                }
            }
            else
            {
                Debug.LogError("Prefab or spawn point is not assigned!");
            }
        }
    }
}
