using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatMonitor : MonoBehaviour
{
    public struct Heartbeat
    {
        public int id;
        public float time;

        public Heartbeat(float time)
        {
            id = 0;
            this.time = time;
        }
    }

    public int pointCount = 32;
    public float range;

    public int heartbeatMin;
    public int heartbeatMax;
    public float heartbeatHeightMin;
    public float heartbeatHeightMax;
    public float heartbeatWidthMin;
    public float heartbeatWidthMax;

    public List<Heartbeat> heartbeats = new List<Heartbeat>();

    private List<Vector3> points = new List<Vector3>();

    void Update ()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        points.Add(new Vector2(0f, 0f));
        foreach (Heartbeat heartbeat in heartbeats)
        {
            if (Time.time >= heartbeat.time && Time.time <= heartbeat.time + range)
            {
                float t = (Time.time - heartbeat.time) / range;

                Random.InitState(heartbeat.time.GetHashCode());
                float w = Random.Range(heartbeatWidthMin, heartbeatWidthMax);
                float hw = w / 2;

                Vector3 a = new Vector3(t - hw, 0f);
                Vector3 b = new Vector3(t, Random.Range(heartbeatHeightMin, heartbeatHeightMax));
                Vector3 c = new Vector3(t + hw, 0f);

                points.Add(a);
                points.Add(b);
                points.Add(c);
            }
        }

        points.Add(new Vector2(1f, 0f));
        //points.Sort((x, y) => x.x.CompareTo(y.x));
        /*
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 a1 = points[i];
            Vector3 b1 = points[i + 1];

            for (int ii = points.Count - 1; ii >= 1; ii--)
            {
                Vector3 a2 = points[ii];
                Vector3 b2 = points[ii - 1];

                Vector3 intersection;
                if(LineLineIntersection(out intersection, a1, b1, a2, b2))
                {
                    points[i + 1] = intersection;
                    points[ii - 1] = intersection;
                }
            }
        }
        */

        lineRenderer.numPositions = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
    }

    //http://wiki.unity3d.com/index.php/3d_Math_functions - Bit Barrel Media 
    //Calculate the intersection point of two lines. Returns true if lines intersect, otherwise false.
    //Note that in 3d, two lines do not intersect most of the time. So if the two lines are not in the 
    //same plane, use ClosestPointsOnTwoLines() instead.
    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {
        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parrallel
        if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }
}
