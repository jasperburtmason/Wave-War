using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct Oscillator
    {
        [HideInInspector]
        public float t;
        public enum Function { Sine, Square };
        public Function function;
        public float frequency;
        public float amplitude;
    }

    public float scale = 1f;
    public float speed = 1f;
    public float smoothness = 1f;
    public int pointCount = 32;
    public Oscillator[] oscillators;

	void Update ()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.numPositions = pointCount;
        for (int o = 0; o < oscillators.Length; o++)
        {
            oscillators[o].t += Time.deltaTime * speed;
        }

        Vector2[] points = new Vector2[pointCount];
        for(int p = 0; p < points.Length; p++)
        {
            float x = (float)p / (points.Length - 1);
            float y = 0;

            for (int o = 0; o < oscillators.Length; o++)
            {
                Oscillator oscillator = oscillators[o];
                float t = oscillator.t + x * oscillator.frequency * scale;
                switch(oscillator.function)
                {
                    case Oscillator.Function.Sine:
                        y += Mathf.Sin(t * 180f * Mathf.Deg2Rad) * oscillator.amplitude * scale;
                        break;
                    case Oscillator.Function.Square:
                        y += Mathf.Sign(Mathf.Sin(t * 180f * Mathf.Deg2Rad) * oscillator.amplitude * scale);
                        break;
                }
         
            }

            Vector3 point = new Vector2(x - 0.5f, y); //Mathf.Lerp(lineRenderer.GetPosition(p).y, y, Time.deltaTime * smoothness)
            points[p] = point;
            lineRenderer.SetPosition(p, point);      
        }

        edgeCollider.points = points;
	}
}
