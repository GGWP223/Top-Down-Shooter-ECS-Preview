using UnityEngine;

namespace DI.Views
{
    public class ParticleView : MonoBehaviour
    {
        [field: SerializeField] public ParticleSystem[] Particles { get; private set; }

        public ParticleView Parent { get; set; }

        public bool IsPlaying()
        {
            foreach (var particle in Particles)
            {
                if(particle.isPlaying)
                   return true; 
            }
            
            return false;
        }
    }
}