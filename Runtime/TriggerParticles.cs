using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
namespace SimpleTriggerCollider.Runtime
{
    [AddComponentMenu("Effects/Trigger Particles")]
    [RequireComponent(typeof(ParticleSystem))]
    public class TriggerParticles : MonoBehaviour
    {
        [SerializeField] private UnityEvent onParticleTriggerEnter = new();
        [SerializeField] private UnityEvent onParticleTriggerExit = new();


        private new ParticleSystem particleSystem;
        private List<ParticleSystem.Particle> particlesEnter = new();
        private List<ParticleSystem.Particle> particlesExit = new();

        private void OnValidate()
        {
            if (TryGetComponent<ParticleSystem>(out ParticleSystem system))
            {
                particleSystem = system;
                var trigger = particleSystem.trigger;
                trigger.enabled = true;
                trigger.enter = ParticleSystemOverlapAction.Callback;
                trigger.exit = ParticleSystemOverlapAction.Callback;
            }
        }

        private void OnParticleTrigger()
        {
            //Populate list
            particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesEnter);
            particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit,particlesExit);

            //For each particle, invoke the event
            foreach (ParticleSystem.Particle particle in particlesEnter)
            {
                onParticleTriggerEnter.Invoke();
            }
            foreach (ParticleSystem.Particle particle in particlesExit)
            {
                onParticleTriggerExit.Invoke();
            }
        }
    }
}
