using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float MoveSpeed = 3.5f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Sla de oorspronkelijke positie en rotatie op wanneer het obstakel wordt gespawnd
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallEnd"))
        {
            Destroy(gameObject); // Verwijder het huidige obstakel

            // Spawn een nieuw obstakel op de oorspronkelijke positie en rotatie
            GameObject newObstacle = Instantiate(gameObject, originalPosition, originalRotation);
            newObstacle.transform.localScale = transform.localScale; // Behoud dezelfde schaal als het origineel
        }
    }
}
