using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline), typeof(Selectable))]
public class SelectableObject : MonoBehaviour
{
    [SerializeField] private UIControl ControlInterface;

    private Outline outline;
    private Selectable userManageble;

    private void Awake()
    {
        userManageble = GetComponent<Selectable>();
        outline = GetComponent<Outline>();
    }

    public void Select()
    {
        
    }
}
