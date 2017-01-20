using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public WaveObject[] waveObjects;
    public int points = 32;

	void Update ()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.numPositions = points;
        for(int p = 0; p < points; p++)
        {
            float x = (float)p / points;
            float y = 0;
            for(int wo = 0; wo < waveObjects.Length; wo++)
            {
                WaveObject waveObject = waveObjects[wo];
                float xx = (Time.time + x) * 360 * Mathf.Deg2Rad * waveObject.frequency;
                switch (waveObject.function)
                {
                    case WaveObject.Function.Sine:
                        y += Mathf.Sin(xx) * waveObject.amplitude;
                        break;

                    case WaveObject.Function.Square:
                        y += Mathf.Sign(Mathf.Sin(xx)) * waveObject.amplitude;
                        break;

                    case WaveObject.Function.Triangle:
                        y += Mathf.Abs((xx++ % 2) - 1);
                        break;
                }
            }
                
            lineRenderer.SetPosition(p, new Vector2(x - 0.5f, y));
        }
	}
}
