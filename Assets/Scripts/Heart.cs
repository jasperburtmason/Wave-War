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

    public int player;
    public Color colour = Color.white;
	
	private Rigidbody2D rgb2d;
    
    void Start()
    {
        heartRate = (heartRateMax - heartRateMin) / 2;
        rgb2d = GetComponent <Rigidbody2D>();
    }


	void Update()
    {
        heartRate = Mathf.Clamp(heartRate - (heartRateLoss * Time.deltaTime), heartRateMin, heartRateMax);
        if (player == 1 && Input.GetButtonDown("Jump") || player == 2 && Input.GetButtonDown("Jump-2"))
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

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colour * colourGradient.Evaluate(t);

		//move around
		Move();

	}

	void Move()
    {
        float x = (player == 1) ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal-2");
        float y = (player == 1) ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical-2");

        // clamp the speed
        if (Mathf.Sqrt(x * x + y * y) >= 1.44f)
        {
            x = 1 / 2 * Mathf.Sqrt(2) * x;
            y = 1 / 2 * Mathf.Sqrt(2) * y;
        }
		Vector2 movement = new Vector2 (x, y );
		rgb2d.MovePosition (rgb2d.position + movement*heartMovingSpeed *Time.deltaTime);
	}
}
