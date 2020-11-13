using UnityEngine;

public class ReflectTarget : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        HandleMovement(_transform);
    }

    private void HandleMovement(Transform trans)
    => trans.position = ReflectHandler.IsInView(trans.position) ? trans.position : ReflectHandler.GetReflectedPosOf(trans.position);
}
