using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float heartRateMin;
    public float heartRateMax;

	public float heartMovingSpeed=1f;


    public float heartRate { get { return _heartRate; } }
    private float _heartRate;

	private Rigidbody2D rgb2d;


	void Start(){
		rgb2d = GetComponent <Rigidbody2D>();

	}

	void Update()
    {
		//move around
		Vector2 movement = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"))*heartMovingSpeed *Time.deltaTime;

		rgb2d.MovePosition (rgb2d.position + movement);


	}
}
