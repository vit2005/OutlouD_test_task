using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AppControllerVFX : MonoBehaviour
{
    [SerializeField] Volume volume;

    private Vignette vg;

    public void Init()
    {
        volume.profile.TryGet<Vignette>(out vg);
    }

    public void SetVignette(float value)
    {
        vg.intensity.value = value;
    }
}
