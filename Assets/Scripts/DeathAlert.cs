using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class DeathAlert : MonoBehaviour {
    // properties of class
    public float grain = 0.317f;

    private Grain GrainSettings;
    private Vignette VignetteSettings;
    private ChromaticAberration ChromaticAberrationSettings;
    private Bloom BloomSettings;

    // Use this for initialization
    void Start () {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        if (volume.profile == null) {
            enabled = false;
            Debug.Log("Cant load PostProcess volume");
            return;
        }

        volume.profile.TryGetSettings<Grain>(out GrainSettings);
        volume.profile.TryGetSettings<Vignette>(out VignetteSettings);
        volume.profile.TryGetSettings<ChromaticAberration>(out ChromaticAberrationSettings);
        volume.profile.TryGetSettings<Bloom>(out BloomSettings);
    }

    // Update is called once per frame
    void Update () {
        float PlayerHealthPercent = PlayerHealth.health / PlayerHealth.healthMax;
        if (PlayerHealthPercent < 0.5) {
            GrainSettings.intensity.Override(0.5f - PlayerHealthPercent);
        }
        if (PlayerHealthPercent < 0.3) {
            VignetteSettings.intensity.Override(0.3f - PlayerHealthPercent);
        }
        ChromaticAberrationSettings.intensity.Override(1 - PlayerHealthPercent);
        BloomSettings.dirtIntensity.Override((1 - PlayerHealthPercent) * 5);
        BloomSettings.softKnee.Override(1 - PlayerHealthPercent);

    }
}
