using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct Oscillator
    {
 
        public float t;
        public float frequency;
        public float amplitude;
    }

    public float offset = 0f;
    public float scale = 1f;
    public float smoothness = 1f;
    public int pointCount = 32;
    public Oscillator[] oscillators;

    public float position;

    public float random;
    public float randomScale;
    public float randomSmoothness;

    void Start()
    {
        random = Random.value;
    }

	void Update ()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.numPositions = pointCount;

        random = Mathf.Lerp(random, Random.value, Time.deltaTime * randomSmoothness);

        for (int o = 0; o < oscillators.Length; o++)
        {
            oscillators[o].t += Time.deltaTime * scale;
        }

        Vector2[] points = new Vector2[pointCount];
        for(int p = 0; p < points.Length; p++)
        {
            float x = (float)p / points.Length;
            float y = 0;

            for (int o = 0; o < oscillators.Length; o++)
            {
                Oscillator oscillator = oscillators[o];
                float t = (oscillator.t + x + offset) * 180f * Mathf.Deg2Rad * oscillator.frequency;
                y += Mathf.Sin(t) * oscillator.amplitude * scale;
            }

            Vector3 point = new Vector2(x - 0.5f, y); //Mathf.Lerp(lineRenderer.GetPosition(p).y, y, Time.deltaTime * smoothness)
            points[p] = point;
            lineRenderer.SetPosition(p, point);      
        }

        edgeCollider.points = points;
	}
}
