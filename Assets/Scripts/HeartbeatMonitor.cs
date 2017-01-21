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
                float width = Random.Range(heartbeatWidthMin, heartbeatWidthMax);
                float halfWidth = width / 2;
                Random.InitState(heartbeat.time.GetHashCode());
                float t = (Time.time - heartbeat.time) / range;

                Vector3 a1 = new Vector3(t - halfWidth, 0f);
                Vector3 b1 = new Vector3(t, Random.Range(heartbeatHeightMin, heartbeatHeightMax));
                Vector3 c1 = new Vector3(t + halfWidth, 0f);

                foreach (Heartbeat other in heartbeats)
                {
                    Vector3 a2 = new Vector3(t - halfWidth, 0f);
                    Vector3 b2 = new Vector3(t, Random.Range(heartbeatHeightMin, heartbeatHeightMax));
                    Vector3 c2 = new Vector3(t + halfWidth, 0f);
                }
            }
        }
        points.Add(new Vector2(1f, 0f));
        points.Sort((x, y) => x.x.CompareTo(y.x));
        lineRenderer.numPositions = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
        //edgeCollider.points = points;
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
