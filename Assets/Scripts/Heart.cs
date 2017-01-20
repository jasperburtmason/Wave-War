using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [HideInInspector]
    public float heartRate;
    public float heartRateMin;
    public float heartRateMax;


	  public float heartMovingSpeed=1f;


    public float heartRate { get { return _heartRate; } }




    public float heartRateGain;
    public float heartRateLoss;

    public float sizeMin;
    public float sizeMax;
    public AnimationCurve sizeCurve;
    
	  private float _heartRate;

	  private Rigidbody2D rgb2d;
    
    void Start()
    {
        heartRate = (heartRateMax - heartRateMin) / 2;
        rgb2d = GetComponent <Rigidbody2D>();
    }


	void Update()
    {
        if(Input.GetButtonDown("Tap")) //rename?
        {
            heartRate += heartRateGain;
        }
        //maybe only when they're not tapping?
        heartRate = Mathf.Clamp(heartRate - (heartRateLoss * Time.deltaTime), heartRateMin, heartRateMax);

        if(heartRate <= heartRateMin || heartRate >= heartRateMax)
        {
            //lose
        }
        transform.localScale = new Vector3(sizeMin, sizeMin) + new Vector3(sizeMax - sizeMin, sizeMax - sizeMin) * sizeCurve.Evaluate((heartRate - heartRateMin) / heartRateMax);

        Debug.Log(heartRate);

		//move around
		Vector2 movement = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"))*heartMovingSpeed *Time.deltaTime;

		rgb2d.MovePosition (rgb2d.position + movement);


	}
}
