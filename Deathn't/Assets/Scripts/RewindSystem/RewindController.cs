using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindController : MonoBehaviour
{
    public static bool stopAll = false;
    public bool triggerParadox = false;
    public static bool globalRewind = false;
    [SerializeField] Recorder recorder = null;
    [SerializeField] MonoBehaviour input = null;
    [SerializeField] AbstratMovement movementBehaviour = null;
    bool isRewinding = false;
    bool isReplaying = false;
    int rewindIndex = 0;
    static float replayPauseTime = 0;
    float timeToReplay = 0;

    Vector2 gizmo = Vector2.zero;

    private void Update()
    {
        if (globalRewind && !isRewinding && !isReplaying)
        {
            StartRewind();
        }     
        if (stopAll)
        {
            StartReplay();
            StopReplay();
        }
    }
    private void LateUpdate()
    {
        stopAll = false;
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            StepRewind();
        }
        else if (isReplaying)
        {
            if (true)
            {
                StepReplay();
            }
        }
    }

    public void StartReplay()
    {
        GetComponent<Rigidbody2D>().simulated = true;
        movementBehaviour.holdSimulation = true;
        isRewinding = false;
        isReplaying = true;
    }
    public void StopReplay()
    {
        movementBehaviour.holdSimulation = false;
        input.enabled = true;
        recorder.enabled = true;
        isReplaying = false;
        globalRewind = false;
    }

    private void StepReplay()
    {
        if (rewindIndex <= 0)
        {
            if(triggerParadox)
            {
                AlivePlayer.instance.AliveAgain();
            }
            StopReplay();
            return;
        }

        
        rewindIndex--;
        PointInTime point = recorder.recordedPoints[rewindIndex];

        gizmo = transform.position;
        //transform.position = point.position;

        if (rewindIndex == recorder.recordedPoints.Count - 1)
        {
            movementBehaviour.rb.velocity = point.velocity;
            Debug.ClearDeveloperConsole();
        }
        else
        {
            PointInTime previousPoint = recorder.recordedPoints[rewindIndex + 1];
            //float lerpFactor = point.deltaTime / Time.fixedDeltaTime;
            //Vector2 lerpedPosition = Vector2.LerpUnclamped(previousPoint.position, transform.position, lerpFactor);
            //gizmo = lerpedPosition;

            float dist = Vector2.Distance(transform.position, point.position);
            //Debug.Log($"{gameObject.name} distance: {dist}");

            if (dist > 0.01f)
            {
                if (triggerParadox)
                {
                    Debug.LogWarning("Alive Again Paradox");
                    AlivePlayer.instance.AliveAgain();
                    //StopReplay();
                    //return;
                }
                //StopReplay();
            }

        }

        // "error corrections"
        transform.position = point.position;
        // movementBehaviour.rb.velocity = point.velocity;
        // input and step
        movementBehaviour.ClearInputs();
        movementBehaviour.Move(point.desiredVelocity); // move input
        if (point.jumpInput) movementBehaviour.Jump(); // jump input
        movementBehaviour.StepSimulation(Time.fixedDeltaTime);
        //movementBehaviour.rb.velocity -= Physics2D.gravity * Time.fixedDeltaTime;
    }


    void StartRewind()
    {
        if (gameObject.tag == "Player") GetComponent<Rigidbody2D>().simulated = false;
        input.enabled = false;
        recorder.enabled = false;
        rewindIndex = 0;
        timeToReplay = 0;
        isRewinding = true;
    }

    void StepRewind()
    {
        if (rewindIndex >= recorder.recordedPoints.Count)
        {
            transform.position = recorder.recordedPoints[rewindIndex-1].position;
            if (timeToReplay == 0) {
                timeToReplay = Time.time + replayPauseTime;
            }
            if (Time.time >= timeToReplay)
            {
                Debug.Log("Replaying");
                StartReplay();
            }
            return;
        }
        else
        {
            transform.position = recorder.recordedPoints[rewindIndex].position;
            rewindIndex++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gizmo, 0.07f);
    }
}
