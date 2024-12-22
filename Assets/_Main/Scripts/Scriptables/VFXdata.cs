using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dincdev
{
    [CreateAssetMenu(fileName = "VFXdata", menuName = "Scriptables/VFXdata", order = 1)]
    public class VFXdata : ScriptableObject
    {
        [field: SerializeField] public ParticleSystem Vfx { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public string VfxName { get; private set; }
    }
}