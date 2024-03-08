using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

    public void TriggerNewPhaseEvent()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].ChangePhase();
        }
    }

    public void AddListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }
}
