using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [HideInInspector]
    public float heartRate;
    public float heartRateMin;
    public float heartRateMax;

    public float heartRateGain;
    public float heartRateLoss;

    public float sizeMin;
    public float sizeMax;
    public AnimationCurve sizeCurve;
    public float rotationAmount;
    public AnimationCurve rotationCurve;
    public Gradient colourGradient;
	
    void Start()
    {
        heartRate = (heartRateMax - heartRateMin) / 2;
    }

	void Update()
    {
        heartRate = Mathf.Clamp(heartRate - (heartRateLoss * Time.deltaTime), heartRateMin, heartRateMax);
        if (Input.GetButtonDown("Tap"))
        {
            heartRate += heartRateGain;
        }

        if(heartRate <= heartRateMin || heartRate >= heartRateMax)
        {
            //lose
        }

        float t = (heartRate - heartRateMin) / heartRateMax;
        transform.localScale = new Vector3(sizeMin, sizeMin) + new Vector3(sizeMax - sizeMin, sizeMax - sizeMin) * sizeCurve.Evaluate(t);
        //transform.Rotate(new Vector3(0, 0, 1), Mathf.PingPong(Time.time, rotation)
        GetComponent<SpriteRenderer>().color = colourGradient.Evaluate(t);
	}
}
