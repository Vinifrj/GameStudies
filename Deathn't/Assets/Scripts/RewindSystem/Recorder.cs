using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [SerializeField] AbstratMovement movementBehavior = null;
    public List<PointInTime> recordedPoints = new List<PointInTime>();
    static float globalRecordTime = 3f;
    [SerializeField] float maxRecordTime = 5f;
    public float time;

    private void Awake()
    {
        recordedPoints = new List<PointInTime>();
        maxRecordTime = globalRecordTime;
    }

    private void FixedUpdate()
    {
        RecordPoint(Time.fixedDeltaTime);
    }

    void RecordPoint(float deltaTime)
    {
        PointInTime point = new PointInTime();
        point.deltaTime = deltaTime;
        point.position = transform.position;
        point.velocity = movementBehavior.rb.velocity;
        point.jumpInput = movementBehavior.jumpInput;
        point.desiredVelocity = movementBehavior.velocityInput;
        recordedPoints.Insert(0, point);
        ClampTime();
    }

    void ClampTime()
    {
        float time = 0;
        for(int i = 0; i < recordedPoints.Count; i++)
        {
            time += recordedPoints[i].deltaTime;
            if (time > maxRecordTime)
            {
                recordedPoints.RemoveRange(i, recordedPoints.Count - i);
                break;
            }
        }
        this.time = time;
    }

    private void OnDrawGizmos()
    {
        foreach(PointInTime p in recordedPoints)
        {
            Gizmos.DrawSphere(p.position, 0.06f);
        }
    }

}

[System.Serializable]
public class PointInTime
{
    public float deltaTime;
    public Vector2 position;
    public Vector2 velocity;
    public bool jumpInput;
    public Vector2 desiredVelocity;

}