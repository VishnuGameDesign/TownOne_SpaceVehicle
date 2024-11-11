using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class SpaceVehicle : MonoBehaviour
{
    public bool Fixed = false;
    public float ZotFlashTime = 0.5f;
    public int ToolRequired = 2;

    public GameObject _repairedVehicle;
    
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

    
    IEnumerator HideZot() {
        yield return new WaitForSeconds(ZotFlashTime);
        _repairedVehicle.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(Fixed) {
            GetComponent<SpriteRenderer>().enabled = false;
                _repairedVehicle.SetActive(true);
        }
    }
}
