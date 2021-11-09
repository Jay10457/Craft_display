using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tutorial;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow CameraInstance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CameraFollow>();
            }

            return instance;
        }
    }

    static CameraFollow instance;

    public float FollowSpeed = 5f;

    [SerializeField] Transform target;
    [SerializeField] Transform target2;

    Transform cameraTransform;
    Vector3 followOffset;

	void Start ()
    {
        init();

        if (target != null)
        {
            SetNewTarget(target, true);
        }
	}

    void init()
    {
        cameraTransform = transform;
    }
	
	void FixedUpdate ()
    {
        moveCamera();
	}
    
    void moveCamera()
    {
        if (TutorialSequence.lookAtTarget)
        {
            
            cameraTransform.position = Vector3.Lerp(cameraTransform.localPosition, target2.localPosition - followOffset, Time.fixedDeltaTime * 3);
        }
        else
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.position - followOffset, Time.fixedDeltaTime * FollowSpeed);
        }
       
    }

    public void SetNewTarget(Transform newTarget, bool calcOffset = false)
    {
        target = newTarget;

        if(calcOffset)
        {
            followOffset = newTarget.position - cameraTransform.position;
        }
    }
}
