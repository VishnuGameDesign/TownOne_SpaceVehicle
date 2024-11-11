using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceVehicle : MonoBehaviour, IInteractable
{
    public bool Fixed = false;
    public float ZotFlashTime = 0.5f;
    public int ToolRequired = 2;
    public int VehicleGFX = 1;

    public Sprite[] gfx;

    public GameObject _repairedVehicle;
    public GameObject _player;
    
    public void ApplyTool(int i)
    {
        Debug.Log(i);
        if (!Fixed && i == ToolRequired)
        {
            Debug.Log("Player fixed vehicle");
            Fixed = true;

            GameManager.Instance.AddVehicleRepaired();
        }
        else if (Fixed)
        {
            Debug.Log("Player applied tool to fixed vehicle");
        }
        else
        {
            Debug.Log("Player applied wrong tool to vehicle");
            _repairedVehicle.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(HideZot());
        }
    }

    public void Interact(InputValue value)
    {
        if (_player.GetComponent<Player>().heldTool != -1) {
            ApplyTool(_player.GetComponent<Player>().heldTool);
        }
    }
    
    IEnumerator HideZot() {
        yield return new WaitForSeconds(ZotFlashTime);
        _repairedVehicle.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = gfx[VehicleGFX];
        if(Fixed) {
            GetComponent<SpriteRenderer>().enabled = false;
            _repairedVehicle.SetActive(true);
        }
    }
}
