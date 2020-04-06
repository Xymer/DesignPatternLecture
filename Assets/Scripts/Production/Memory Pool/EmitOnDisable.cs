using System;
using UnityEngine;

class EmitOnDisable : MonoBehaviour
{
    public event Action<GameObject> OnDisableGameObject;

    private void OnDisable()
    {
        OnDisableGameObject?.Invoke(this.gameObject);
    }
}

