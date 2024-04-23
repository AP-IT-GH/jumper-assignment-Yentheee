using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class MLPlayer : Agent
{
    public float force = 15f;
    public Transform reset = null;
    public TextMesh score = null;
    public GameObject thrust = null;
    private Rigidbody rb = null;
    private float points = 0;
    private bool shouldJump = false;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        ResetMyAgent();
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var vectorAction = actionBuffers.DiscreteActions;
        if (vectorAction[0] == 1)
        {
            UpForce();
            shouldJump = true;
            thrust.SetActive(true);
            Debug.Log("Spacebar pressed");
        }
        else
        {
            shouldJump = false;
            thrust.SetActive(false);
        }
    }

    public override void OnEpisodeBegin()
    {
        ResetMyAgent();
    }

 /*   public override void Heuristic()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            UpForce();
        }
    }
*/
    private void Update()
    {
        // Remove if redundant based on action space configuration
        if (shouldJump)
        {
            UpForce();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-1.0f);
            Destroy(collision.gameObject);
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("WallTop"))
        {
            AddReward(-0.9f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallReward"))
        {
            AddReward(0.1f);
            points++;
            score.text = points.ToString();
        }
    }

    private void UpForce()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }

    private void ResetMyAgent()
    {
        transform.position = reset.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
