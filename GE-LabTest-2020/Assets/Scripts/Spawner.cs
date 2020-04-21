using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public int numTrafficLights = 10;
	public float radius = 10;
	private List<Vector3> trafficCones = new List<Vector3>();
	public List<Material> trafficColours = new List<Material>();
	

	// Update is called once per frame
	void Update()
    {
        
    }

	public void OnDrawGizmos()
	{
		foreach (Vector3 pos in trafficCones)
		{
			Gizmos.DrawWireSphere(pos, 2);
		}
	}

	private void CreateTrafficCones()
	{
		foreach (Vector3 pos in trafficCones)
		{
			int index = Random.Range(0, trafficColours.Count);
			GameObject trafficCone = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			trafficCone.transform.position = pos;

			trafficCone.GetComponent<Renderer>().material = trafficColours[index];
			trafficCone.transform.parent = this.transform;

			// Set the tag if they're green
			if (index == 0)
			{
				trafficCone.tag = "GreenLight";
			}

			// For detecting collision
			trafficCone.AddComponent<Rigidbody>().useGravity = false;
			trafficCone.GetComponent<CapsuleCollider>().isTrigger = true;

			// Set colour array and colour counter
			trafficCone.AddComponent<TrafficLight>().trafficColours = trafficColours;
			trafficCone.GetComponent<TrafficLight>().colourCounter = index;
		}
	}

	// Use this for initialization
	void Awake()
	{
		// Get the positions
		float thetaInc = (Mathf.PI * 2) / (float)numTrafficLights;
		for (int i = 0; i < numTrafficLights; i++)
		{
			float theta = i * thetaInc;
			Vector3 pos = new Vector3(Mathf.Sin(theta) * radius, 0, Mathf.Cos(theta) * radius);
			pos = transform.TransformPoint(pos);
			trafficCones.Add(pos);
		}

		// Create Traffic Cones
		CreateTrafficCones();
	}
}
