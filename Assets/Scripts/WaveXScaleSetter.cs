using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveXScaleSetter : MonoBehaviour {


	/*
	 * Add this script to the wave
	 * to make the wave always full with the screen view
	 */

	void Start(){

		this.transform.localScale = new Vector3 (GameManager.instance.width, transform.localScale.y, transform.localScale.z);

	}
}
