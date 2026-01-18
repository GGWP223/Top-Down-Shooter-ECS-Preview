using UnityEngine;

namespace ECS.Events
{
    public struct PlayAudioEvent
    {
        public AudioClip Clip;
        
        public Vector3 Position;
        
        public float Volume;
        public float Pitch;
        
        public float MaxDistance;
    }
}