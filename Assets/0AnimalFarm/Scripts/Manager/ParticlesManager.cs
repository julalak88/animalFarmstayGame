using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ParticlesManager : SerializedMonoBehaviour
{
    public Dictionary<string, GameObject> particles;

    public Transform CreateParticles(string particlesName) {
        return Instantiate(particles[particlesName]).transform;
    }
}
