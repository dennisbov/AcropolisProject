using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public abstract class Selectable : MonoBehaviour
{
    [SerializeField] protected UIControl ControlInterface;

    private Outline outline;
    private void Awake()
    {
        outline = GetComponent<Outline>(); 
        outline.enabled = false;
    }
    public virtual void Select()
    {
        ControlInterface.gameObject.SetActive(true);
        outline.enabled = true;
    }
    public virtual void Deselect() 
    {
        outline.enabled = false;
        ControlInterface.gameObject.SetActive(true);
        foreach (Button button in ControlInterface.Buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
