using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 Amount = new Vector3(1f, 1f, 0);
    private float Duration = 1;
    private float Speed = 10;
    private AnimationCurve Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    /// <summary>
    /// Set it to true: The camera position is set in reference to the old position of the camera
    /// Set it to false: The camera position is set in absolute values or is fixed to an object
    /// </summary>
    private bool DeltaMovement = true;

    private Camera Camera;
    private float time = 0;
    private Vector3 lastPos;
    private Vector3 nextPos;
    private float lastFoV;
    private float nextFoV;
    private bool destroyAfterPlay;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    public static void Shake(float duration = 1f, float speed = 10f, Vector3? amount = null, Camera camera = null, bool deltaMovement = true, AnimationCurve curve = null)
    {
        var instance = ((camera != null) ? camera : Camera.main).gameObject.AddComponent<CameraShake>();
        instance.Duration = duration;
        instance.Speed = speed;
        if (amount != null)
            instance.Amount = (Vector3)amount;
        if (curve != null)
            instance.Curve = curve;
        instance.DeltaMovement = deltaMovement;

        instance.destroyAfterPlay = true;
        instance.StartShake();
    }

    private void StartShake()
    {
        ResetCam();
        time = Duration;
    }

    private void LateUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time > 0)
            {
                //next position based on perlin noise
                nextPos = (Mathf.PerlinNoise(time * Speed, time * Speed * 2) - 0.5f) * Amount.x * transform.right * Curve.Evaluate(1f - time / Duration) +
                          (Mathf.PerlinNoise(time * Speed * 2, time * Speed) - 0.5f) * Amount.y * transform.up * Curve.Evaluate(1f - time / Duration);
                nextFoV = (Mathf.PerlinNoise(time * Speed * 2, time * Speed * 2) - 0.5f) * Amount.z * Curve.Evaluate(1f - time / Duration);

                Camera.fieldOfView += (nextFoV - lastFoV);
                Camera.transform.Translate(DeltaMovement ? (nextPos - lastPos) : nextPos);

                lastPos = nextPos;
                lastFoV = nextFoV;
            }
            else
            {
                //last frame
                ResetCam();
                if (destroyAfterPlay)
                    Destroy(this);
            }
        }
    }

    private void ResetCam()
    {
        //reset the last delta
        Camera.transform.Translate(DeltaMovement ? -lastPos : Vector3.zero);
        Camera.fieldOfView -= lastFoV;

        //clear values
        lastPos = nextPos = Vector3.zero;
        lastFoV = nextFoV = 0f;
    }
}

