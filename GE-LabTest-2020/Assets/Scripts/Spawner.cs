using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public int numTrafficLights = 10;
	public float radius = 10;
	private List<Vector3> TrafficCones = new List<Vector3>();

	// Update is called once per frame
	void Update()
    {
        
    }

	public void OnDrawGizmos()
	{
		foreach (Vector3 pos in TrafficCones)
		{
			Gizmos.DrawWireSphere(pos, 2);
		}
	}

	private void CreateTrafficCones()
	{
		foreach (Vector3 pos in TrafficCones)
		{
			GameObject trafficCone = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			trafficCone.transform.position = pos;
			trafficCone.transform.parent = this.transform;
		}
	}

	// Use this for initialization
	void Awake()
	{
		float thetaInc = (Mathf.PI * 2) / (float)numTrafficLights;
		for (int i = 0; i < numTrafficLights; i++)
		{
			float theta = i * thetaInc;
			Vector3 pos = new Vector3(Mathf.Sin(theta) * radius, 0, Mathf.Cos(theta) * radius);
			pos = transform.TransformPoint(pos);
			TrafficCones.Add(pos);
		}

		CreateTrafficCones();
	}
}
