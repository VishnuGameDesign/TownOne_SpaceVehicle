using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairMiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _rightEndPivotPoint;
    [SerializeField] private Transform _leftEndPivotPoint;
    [SerializeField] private GameObject _movingTarget;
    [SerializeField] private GameObject _matchingTarget;

    [SerializeField] private float _smallerBarSpeed = 1.5f;
    [SerializeField] private float _biggerBarSpeed = 2f;
    [SerializeField] private float _toMatchThreshold = .1f;
    [SerializeField] private List<GameObject> _toolImages;
    public Dictionary<int, GameObject> _toolImageIds = new Dictionary<int, GameObject>();
    
    private bool _isMoving;

    private void Awake()
    {
        for (int i = 0; i < _toolImages.Count; i++)
        {
            _toolImageIds.Add(i, _toolImages[i]);
            _toolImageIds[i].gameObject.SetActive(false);
            
        }
    }

    private void Start()
    {
        _movingTarget.transform.position = _leftEndPivotPoint.position;
        _matchingTarget.transform.position = _rightEndPivotPoint.position;
        
    }
    

    private IEnumerator MoveTarget()
    {
        while (_isMoving)
        {
            float distance = Vector2.Distance(_leftEndPivotPoint.position, _rightEndPivotPoint.position);
            float pingPongForSmallBar = Mathf.PingPong(Time.time * _smallerBarSpeed, distance);    
            float pingPongForBigBar = Mathf.PingPong(Time.time * _biggerBarSpeed, distance);    
            
            _movingTarget.transform.position = Vector3.Lerp(_leftEndPivotPoint.position, _rightEndPivotPoint.position, pingPongForSmallBar / distance);
            _matchingTarget.transform.position = Vector3.Lerp(_rightEndPivotPoint.position, _leftEndPivotPoint.position, pingPongForBigBar / distance);
            yield return null;
        }
        
    }

    private void WhenTargetMatched()
    {
        float distance = Vector3.Distance(_movingTarget.transform.position, _matchingTarget.transform.position);
        if (distance <= _toMatchThreshold)
        {
            _isMoving = false;
            Debug.Log("Spawn Tool");
            StopCoroutine(MoveTarget());
            
            int randomToolIndex = Random.Range(0, _toolImages.Count);
            _toolImages[randomToolIndex].SetActive(true);
            
            var spaceVehicle = FindObjectOfType<SpaceVehicle>();
            spaceVehicle?.ApplyTool(randomToolIndex);
            Debug.Log(randomToolIndex);
                
            Invoke(nameof(DisableImage), 2f);
        }
        else
        {
            Debug.Log("Match failed");
        }
        
    }

    public void Interact(InputValue value)
    {
        if (!_isMoving)
        {
            _isMoving = true;
            StartCoroutine(MoveTarget());
        }
        else
        {
           WhenTargetMatched(); 
        }
    }

    private void DisableImage()
    {
        foreach (var tools in _toolImages)
        {
            tools.SetActive(false);
        }
    }
}
