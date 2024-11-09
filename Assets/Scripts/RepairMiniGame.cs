using System.Collections;
using UnityEngine;

public class RepairMiniGame : MonoBehaviour
{
    [SerializeField] private Transform _rightEndPivotPoint;
    [SerializeField] private Transform _leftEndPivotPoint;
    [SerializeField] private GameObject _movingTarget;
    [SerializeField] private GameObject _matchingTarget;

    [SerializeField] private float _smallerBarSpeed = 1.5f;
    [SerializeField] private float _biggerBarSpeed = 2f;

    private void Start()
    {
        _movingTarget.transform.position = _leftEndPivotPoint.position;
        _matchingTarget.transform.position = _rightEndPivotPoint.position;
    }

    private void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        float distance = Vector2.Distance(_leftEndPivotPoint.position, _rightEndPivotPoint.position);
        float pingPongForSmallBar = Mathf.PingPong(Time.time * _smallerBarSpeed, distance);    
        float pingPongForBigBar = Mathf.PingPong(Time.time * _biggerBarSpeed, distance);    
        
        _movingTarget.transform.position = Vector3.Lerp(_leftEndPivotPoint.position, _rightEndPivotPoint.position, pingPongForSmallBar / distance);
        _matchingTarget.transform.position = Vector3.Lerp(_rightEndPivotPoint.position, _leftEndPivotPoint.position, pingPongForBigBar / distance);
    }
    
    
}
