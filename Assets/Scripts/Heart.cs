﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [HideInInspector]
    public float heartRate;
    public float heartRateMin;
    public float heartRateMax;

	public float heartMovingSpeed=1f;

    public float heartRateGain;
    public float heartRateLoss;

    public float sizeMin;
    public float sizeMax;
    public AnimationCurve sizeCurve;
    public float rotationSpeed;
    public float rotationAmount;
    public float rotationResetSpeed;
    public AnimationCurve rotationCurve;
    public Gradient colourGradient;

    public Wave[] waves;
    public float frequencyMin;
    public float frequencyMax;
    public float frequencySpeed;

	
	private Rigidbody2D rgb2d;
    
    void Start()
    {
        heartRate = (heartRateMax - heartRateMin) / 2;
        rgb2d = GetComponent <Rigidbody2D>();
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

        foreach (Wave wave in waves)
        {
            wave.scale = Mathf.Lerp(wave.scale, frequencyMin + (frequencyMax - frequencyMin) * t, Time.deltaTime * frequencySpeed);
        }


        transform.localScale = new Vector3(sizeMin, sizeMin) + new Vector3(sizeMax - sizeMin, sizeMax - sizeMin) * sizeCurve.Evaluate(t);
        transform.Rotate(new Vector3(0, 0, 1), (Mathf.PingPong(Time.time * rotationSpeed, 1f) - 0.5f) * rotationAmount * rotationCurve.Evaluate(t) * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotationResetSpeed * (1f - rotationCurve.Evaluate(t)));
        GetComponent<SpriteRenderer>().color = colourGradient.Evaluate(t);

		Vector2 movement = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"))*heartMovingSpeed *Time.deltaTime;
		rgb2d.MovePosition (rgb2d.position + movement);
	}
}
