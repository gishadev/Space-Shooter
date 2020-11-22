using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceGame.ReflectMovement
{
    public class ReflectTarget : MonoBehaviour
    {
        Transform _transform;

        TrailRenderer[] _trails;

        private void Awake()
        {
            _transform = transform;
            _trails = GetComponentsInChildren<TrailRenderer>();
        }

        private void Update()
        {
            if (ReflectHandler.IsInView(_transform.position))
                return;

            DeactivateTrails();
            _transform.position = ReflectHandler.GetReflectedPosOf(_transform.position);
            Invoke("ActivateTrails", 0.001f);
        }


        void ActivateTrails()
        {
            if (_trails != null && _trails.Length > 0)
                foreach (TrailRenderer tr in _trails)
                {
                    var positions = new Vector3[tr.positionCount];
                    for (int i = 0; i < positions.Length; i++)
                        positions[i] = _transform.position;

                    tr.SetPositions(positions);
                    tr.enabled = true;
                }
        }

        void DeactivateTrails()
        {
            if (_trails != null && _trails.Length > 0)
                foreach (TrailRenderer tr in _trails)
                    tr.enabled = false;
        }

        bool IsTrailsActive() => _trails.Any(x => x.enabled);
    }
}