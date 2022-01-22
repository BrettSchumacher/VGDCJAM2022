using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{
    [SerializeField]
    protected float request_radius;
    [SerializeField]
    protected float use_radius;

    protected GameObject user;
    protected Vector3 user_position;
    protected float signed_user_distance;
    protected float user_distance;

    protected float usability_progress;

    protected UnityEvent use_event;

    public void RequestUse()
    {
        if(usability_progress >= 1)
        {
            use_event.Invoke();
        }
    }

    protected virtual void Awake()
    {
        use_event = new UnityEvent();

        user = GameObject.FindWithTag("Player");
    }

    protected virtual void Update()
    {
        user_position = user.transform.position;
        signed_user_distance = transform.position.x - user_position.x;
        user_distance = Mathf.Abs(signed_user_distance);

        float request_slice = request_radius - use_radius;
        float slice_distance = Mathf.Clamp(request_radius - user_distance, 0, request_slice);
        usability_progress = slice_distance / request_slice;
    }

    protected virtual void OnDrawGizmos()
    {
        Vector3 center = transform.position;
        Vector3 request_arm = Vector3.right * request_radius;
        Vector3 use_arm = Vector3.right * use_radius;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center - request_arm, center + request_arm);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(center - use_arm, center + use_arm);
    }
}
