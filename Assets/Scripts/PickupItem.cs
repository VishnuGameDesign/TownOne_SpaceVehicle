using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _pickUpPoint;
    private bool _pickedUpItem;

    public void Interact(InputValue value)
    {
        if(_player== null) return;
    
        if(!_pickedUpItem) PickUp();
        else DropItem();
        
    }

    private void PickUp()
    { 
        transform.SetParent(_player.transform);
        transform.position = _pickUpPoint.position;
        _pickedUpItem = true;
    }

    private void DropItem()
    {
        transform.SetParent(null);
        _pickedUpItem = false;
    }
}