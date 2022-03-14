using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProScript : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;

    [SerializeField] private PlayerAbility player;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out vignette);
        vignette.smoothness.value = 0.6f;
        vignette.intensity.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentEnergy >= 80f)
        {
            vignette.smoothness.value = .6f;
            vignette.intensity.value = .4f;
        }
        else if (player.currentEnergy <= 80f)
        {
            vignette.smoothness.value = 0f;
            vignette.intensity.value = 0f;
        }
        
    }
}
