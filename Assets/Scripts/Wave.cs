using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct Oscillator
    {
        public enum Function { Sine, Square };
        public Function function;
        public float frequency;
        public float amplitude;
    }

    public float startScale;
    public float targetScale = 1f;
    public float scaleSmoothness = 1f;
    public float speed = 1f;
    public float smoothness = 1f;
    public int pointCount = 32;
    public Oscillator[] oscillators;

    private float scale;

    void Start()
    {
        scale = startScale;
        transform.localScale = new Vector3(GameManager.instance.width, transform.localScale.y, transform.localScale.z);
    }

	void Update ()
    {
        scale = Mathf.Lerp(scale, targetScale, Time.deltaTime * scaleSmoothness);

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.numPositions = pointCount;

        Vector2[] points = new Vector2[pointCount];
        for(int p = 0; p < points.Length; p++)
        {
            float x = (float)p / (points.Length - 1);
            float y = 0;

            for (int o = 0; o < oscillators.Length; o++)
            {
                Oscillator oscillator = oscillators[o];
                float t = (Time.time + x) * oscillator.frequency * scale;
                switch(oscillator.function)
                {
                    case Oscillator.Function.Sine:
                        y += Mathf.Sin(t * 180f * Mathf.Deg2Rad);
                        break;
                    case Oscillator.Function.Square:
                        y += Mathf.Sign(Mathf.Sin(t * 180f * Mathf.Deg2Rad));
                        break;
                }
                y *= oscillator.amplitude * scale;
            }

            Vector3 point = new Vector2(x - 0.5f, y);
            points[p] = point;
            lineRenderer.SetPosition(p, point);      
        }

        edgeCollider.points = points;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {

    }
}
