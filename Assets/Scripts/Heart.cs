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

    public HeartbeatMonitor monitor;
    public float scaleSmoothness;

    public int player;
    public Color colour = Color.white;
	
	new private Rigidbody2D rigidbody;
    
    void Start()
    {
        heartRate = (heartRateMax - heartRateMin) / 2;
        rigidbody = GetComponent <Rigidbody2D>();
    }


	void Update()
    {
        heartRate = Mathf.Clamp(heartRate - (heartRateLoss * Time.deltaTime), heartRateMin, heartRateMax);
        if (player == 1 && Input.GetButtonDown("Jump") || player == 2 && Input.GetButtonDown("Jump-2"))
        {
            heartRate += heartRateGain;
            monitor.heartbeats.Add(new HeartbeatMonitor.Heartbeat(Time.time));
        }

        if(heartRate <= heartRateMin || heartRate >= heartRateMax)
        {
            //Debug.Log("game over!");
            //gameObject.SetActive(false);
        }

        float t = (heartRate - heartRateMin) / heartRateMax;
        
        transform.localScale = new Vector3(sizeMin, sizeMin) + new Vector3(sizeMax - sizeMin, sizeMax - sizeMin) * sizeCurve.Evaluate(t);
        transform.Rotate(new Vector3(0, 0, 1), (Mathf.PingPong(Time.time * rotationSpeed, 1f) - 0.5f) * rotationAmount * rotationCurve.Evaluate(t));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotationResetSpeed * (1f - rotationCurve.Evaluate(t)));

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colour * colourGradient.Evaluate(t);       

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
		rigidbody.MovePosition (rigidbody.position + movement*heartMovingSpeed *Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Waves")
        {
			Debug.Log("game over!");
			//gameObject.SetActive (false);
		}
	}
}
