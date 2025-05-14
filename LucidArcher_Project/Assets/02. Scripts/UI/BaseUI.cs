using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void InIt(UIManager uIManager)
    {
        this.uiManager = uIManager;
    }

    protected abstract UIState GetUIState();


    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
