using UnityEngine;

namespace SpaceGame.ReflectMovement
{
    public class ReflectTarget : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (ReflectHandler.IsInView(_transform.position))
                return;

            _transform.position = ReflectHandler.GetReflectedPosOf(_transform.position);
        }
    }
}