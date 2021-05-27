using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public List<Light> lights;

    public virtual void toggleHeadLights() {
        foreach (Light light in lights) {
            light.intensity = light.intensity == 0 ? 2 : 0;
        }
    }
}
