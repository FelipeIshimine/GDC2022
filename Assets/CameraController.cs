using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : BaseMonoSingleton<CameraController>
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float dDuration =1;
    [SerializeField] private float dScale = 1;
    [SerializeField] private float dSpeed = 1;
    [SerializeField] private PhysicsRaycaster physicsRaycaster;
    
    private float _duration;
    private float _scale;
    private float _speed;

    private IEnumerator _routine;

    public static void SetRaycast(bool value) => Instance.physicsRaycaster.enabled = value;
    
    [Button]
    public static void Shake() => Shake(Instance.dDuration, Instance.dScale, Instance.dSpeed);

    [Button]
    public static void Shake(float duration) => Shake(duration, Instance.dScale, Instance.dSpeed);
    
    [Button]
    public static void Shake(float duration, float scale, float speed)
    {
        Instance._duration = duration;
        Instance._scale = scale;
        Instance._speed = speed;
        Instance.PlayCoroutine(ref Instance._routine, Instance.ShakeAnimation);
    }

    IEnumerator ShakeAnimation()
    {
        float t = 0;
        float axisOffset = Time.time % 1;
        Vector3 startPosition = transform.position;
        do
        {
            t += Time.deltaTime /_duration;
            Vector3 nPosition = new Vector3((Mathf.PerlinNoise(Time.time * _speed, axisOffset) * _scale * 2)-1, (Mathf.PerlinNoise(axisOffset, Time.time * _speed * .327f) * _scale*2)-1,transform.position.z);
            transform.position = Vector3.Lerp(startPosition, nPosition, curve.Evaluate(t));
            yield return null;
        } while (t<1);
    }
}
