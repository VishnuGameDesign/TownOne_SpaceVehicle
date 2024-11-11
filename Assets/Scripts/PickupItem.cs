using System.Net.NetworkInformation;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _pickUpPoint;
    public int toolId;
    public Sprite default_sprite;
    public Sprite hovered_sprite;
    private bool _pickedUpItem;
    
    public void CheckForToolMatch(InputValue value)
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
        Debug.Log($"{_pickedUpItem}, {toolId}");

        _player.GetComponent<Player>().heldTool = toolId;
    }

    private void DropItem()
    {
        transform.SetParent(null);
        _pickedUpItem = false;

        _player.GetComponent<Player>().heldTool = -1;
    }
    
    
    
    
}
