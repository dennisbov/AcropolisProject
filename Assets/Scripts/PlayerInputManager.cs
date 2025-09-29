using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private LayerMask _selectableLayer;
    [SerializeField] private LayerMask _tileLayer;
    [SerializeField] private Camera _camera;


    private Selectable _currentSelectable = null;

    private bool TryPickSelectable()
    {
        if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if (hit.transform.TryGetComponent<Selectable>(out Selectable selectable))
            {
                _currentSelectable.Deselect();
                selectable.Select();
                _currentSelectable = selectable;
                return true;
            }
        }
        return false;
    } 

    private bool AddRouteTile()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, _tileLayer))
        {
            
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (TryPickSelectable() == false && _currentSelectable != null) 
            { 
                AddRouteTile();
            }
        }
    }
}
