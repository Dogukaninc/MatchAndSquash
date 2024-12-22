using System.Collections;
using System.Collections.Generic;
using dincdev._Main.Scripts.Operations;
using UnityEngine;

namespace dincdev
{
    public class VFXPlayer : SingletonMonoBehaviour<VFXPlayer>
    {
        [SerializeField] private List<VFXdata> vfxDatas;

        public void PlayVFX(string effectName, Vector3 position)
        {
            var desiredVFX = vfxDatas.Find(v => v.VfxName == effectName);
            if (desiredVFX != null)
            {
                ParticleSystem vfx = Instantiate(desiredVFX.Vfx, position, Quaternion.identity);
                Destroy(vfx.gameObject, desiredVFX.LifeTime);
            }
            else
            {
                Debug.LogWarning("Desired VFX not found");
            }
        }
    }
}