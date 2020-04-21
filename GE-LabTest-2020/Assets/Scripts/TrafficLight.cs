using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
	public List<Material> trafficColours;
	public int colourCounter;
	private float waitTime;

	// How long will the current light/colour last
	private float GetTimer()
	{
		// If the traffic light is yellow
		if (colourCounter == 1)
		{
			waitTime = 4;
		}

		// If the traffic light is green or red
		else if (colourCounter == 0 || colourCounter == 2)
		{
			waitTime = Random.Range(5.0f, 10.0f);
		}

		return waitTime;
	}

	private IEnumerator ChangeColour(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(GetTimer());

			// Cycle through the colours
			colourCounter = (colourCounter + 1) % trafficColours.Count;
			gameObject.GetComponent<Renderer>().material = trafficColours[colourCounter];

			// Set tag for green cones
			if (colourCounter == 0)
			{
				gameObject.tag = "GreenLight";
			}

			// Set tag for yellow and red cones
			else
			{
				gameObject.tag = "Untagged";
			}
		}
	}

	private void OnEnable()
	{
		StartCoroutine(ChangeColour(GetTimer()));
	}
}
