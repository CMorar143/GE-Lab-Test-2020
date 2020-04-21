using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
	public List<Material> trafficColours;
	public int colourCounter;
	private float waitTime;


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
			yield return new WaitForSeconds(time);
			
		}
	}

	private void OnEnable()
	{
		StartCoroutine((ChangeColour(waitTime));
	}

	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
