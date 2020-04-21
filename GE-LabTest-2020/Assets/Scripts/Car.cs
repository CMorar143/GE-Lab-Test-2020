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
	
	public Vector3 target = new Vector3(10,10,10);

	Vector3 Seek(Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		Vector3 desired = toTarget.normalized * maxSpeed;

		return desired - velocity;
		//return toTarget - velocity;
	}

	public Vector3 CalculateForce()
	{
		Vector3 force = Vector3.zero;
		if (target != null)
		{
			force += Seek(target);
		}
		return force;
	}

	// Start is called before the first frame update
	void Start()
    {
        
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
			//transform.forward = velocity;
			velocity -= (damping * velocity * Time.deltaTime);
		}
	}
}
