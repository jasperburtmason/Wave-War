using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float heartRateMin;
    public float heartRateMax;

    public float heartRate { get { return _heartRate; } }
    private float _heartRate;
	
	void Update()
    {
		//move around
	}
}
