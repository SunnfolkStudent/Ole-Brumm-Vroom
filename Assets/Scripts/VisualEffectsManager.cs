using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class VisualEffectsManager : MonoBehaviour
{
    ExposedProperty _speedAccordingToPhase;
    VisualEffect _vfx;

    void Start()
    {
        _vfx = GetComponent<VisualEffect>();
        _speedAccordingToPhase = "speedAccordingToPhase";
        PhaseUpdate();
    }

    public void PhaseUpdate()
    {
        if (GameManager.phase1Active)
        {
            _vfx.SetFloat(_speedAccordingToPhase, 1f);
        }

        if (GameManager.phase2Active)
        {
            _vfx.SetFloat(_speedAccordingToPhase, 2.25f);
        }

        if (GameManager.phase3Active)
        {
            _vfx.SetFloat(_speedAccordingToPhase, 4.5f);
        }

        if (GameManager.phase4Active) 
        {
            _vfx.SetFloat(_speedAccordingToPhase, 7f);    
        }
        
    }
}
