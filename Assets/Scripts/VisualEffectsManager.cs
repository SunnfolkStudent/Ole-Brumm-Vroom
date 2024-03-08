using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class VisualEffectsManager : MonoBehaviour
{
    public ExposedProperty speedAccordingToPhase;
    VisualEffect _vfx;

    void Start()
    {
        _vfx = GetComponent<VisualEffect>();
        speedAccordingToPhase = "speedAccordingToPhase";
        PhaseUpdate();
    }

    public void PhaseUpdate()
    {
        if (GameManager.phase1Active)
        {
            _vfx.SetFloat(speedAccordingToPhase, 1f);
        }

        if (GameManager.phase2Active)
        {
            _vfx.SetFloat(speedAccordingToPhase, 2.25f);
        }

        if (GameManager.phase3Active)
        {
            _vfx.SetFloat(speedAccordingToPhase, 4.5f);
        }

        if (GameManager.phase4Active) 
        {
            _vfx.SetFloat(speedAccordingToPhase, 7f);    
        }
        
    }
}
