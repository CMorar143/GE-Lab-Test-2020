//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	private Vector3 velocity = Vector3.zero;
	private Vector3 acceleration = Vector3.zero;
	private Vector3 force = Vector3.zero;

	private float mass = 1.0f;
	private float damping = 0.1f;
	private float banking = 0.1f;

	public float maxSpeed = 5;
	public float maxForce = 10;
	public float speed = 0;
	public float slowingDistance = 5;

	private Transform target = null;

	public GameObject[] greenCones;

	Vector3 Arrive(Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		float dist = toTarget.magnitude;

		float ramped = (dist / slowingDistance) * maxSpeed;
		float clamped = Mathf.Min(ramped, maxSpeed);
		Vector3 desired = (toTarget / dist) * clamped;

		return desired - velocity;
	}

	public Vector3 CalculateForce()
	{
		Vector3 force = Vector3.zero;

		// Find target
		if (target.tag != "GreenLight")
		{
			SetTarget();
		}

		force += Arrive(target.position);

		return force;
	}

	private void SetTarget()
	{
		greenCones = GameObject.FindGameObjectsWithTag("GreenLight");

		if (greenCones.Length != 0)
		{
			target = greenCones[Random.Range(0, greenCones.Length)].transform;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "GreenLight")
		{
			other.gameObject.tag = "Untagged";
			SetTarget();
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		SetTarget();
	}

    // Update is called once per frame
    void Update()
    {
		force = CalculateForce();
		acceleration = force / mass;
		velocity += acceleration * Time.deltaTime;

		transform.position += velocity * Time.deltaTime;
		speed = velocity.magnitude;
		if (speed > 0)
		{
			Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
			transform.LookAt(transform.position + velocity, tempUp);
			velocity -= (damping * velocity * Time.deltaTime);
		}
	}
}
