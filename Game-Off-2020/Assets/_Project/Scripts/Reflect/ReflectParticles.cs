using UnityEngine;

namespace SpaceGame.ReflectMovement
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ReflectParticles : MonoBehaviour
    {
        ParticleSystem _pSystem;
        ParticleSystem.Particle[] _particles;

        Transform _transform;

        private void Awake()
        {
            _pSystem = GetComponent<ParticleSystem>();
            _particles = new ParticleSystem.Particle[_pSystem.main.maxParticles];

            _transform = transform;
        }

        private void LateUpdate()
        {
            int nParticles = _pSystem.GetParticles(_particles);

            for (int i = 0; i < nParticles; i++)
            {
                Vector3 worldPos = _transform.TransformPoint(_particles[i].position);

                if (!ReflectHandler.IsInView(worldPos))
                    _particles[i].position = _transform.InverseTransformPoint(ReflectHandler.GetReflectedPosOf(worldPos));
            }

            _pSystem.SetParticles(_particles, nParticles);
        }
    }
}