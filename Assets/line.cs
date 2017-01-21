using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer l = GetComponent<LineRenderer> ();
		for (int i = 0; i < 10; i++) {

			l.SetPosition (i, (new Vector3 (i, 0, 0)));


		}
		l.transform.Translate (new Vector3 (1, 0, 0));
	}
}
