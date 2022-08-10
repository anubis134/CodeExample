using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ConveyorController:MonoBehaviour
{
    [SerializeField]
    private float _timeDistribution;
    [SerializeField]
    private float _timeRecycling;
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _recyclePoint;
    [SerializeField]
    private Transform _endPoint;
    [SerializeField]
    private List<Transform> _inputPoints = new List<Transform>();
    [SerializeField]
    private List<Transform> _outputPoints = new List<Transform>();
    internal List<IDistributor> _addedSubjects = new List<IDistributor>();
    internal List<IDistributor> _recycledSubjects = new List<IDistributor>();
    private bool _recyclingIsActive = false;

    internal async Task TryAddSubject()
    {
        if(InventoryManager.AllDistributors.Count <= 0) return; 
        while (_addedSubjects.Count < 4)
        {
            if(InventoryManager.AllDistributors.Count <= 0) break; 
            _addedSubjects.Add(InventoryManager.ReturnLastDistributor());
            InventoryManager.ReturnLastDistributor().YOffset = 0f;
            InventoryManager.ReturnLastDistributor().Distribute(_inputPoints[_addedSubjects.Count - 1].transform, _timeDistribution,3f, true);
            //(InventoryManager.ReturnLastDistributor() as InteractableSubject).transform.parent = null;
            InventoryManager.RemoveLastDistributor();
            await Task.Delay(TimeSpan.FromSeconds(_timeDistribution));
        }
        if (!_recyclingIsActive)
        {
            _recyclingIsActive = true;
            await RecycleItem();
        }
    }

    internal async Task RecycleItem()
    {
        while (_addedSubjects.Count > 0 && _recycledSubjects.Count < 4)
        {
            IDistributor current = _addedSubjects[_addedSubjects.Count - 1];
            _addedSubjects.RemoveAt(_addedSubjects.Count - 1);
            _recycledSubjects.Add(current);
            current.Distribute(_startPoint, _timeDistribution, 0.5f, true);
            await Task.Delay(TimeSpan.FromSeconds(_timeDistribution));
            (current as InteractableSubject).transform.DOMove(_recyclePoint.position, _timeRecycling);
            await Task.Delay(TimeSpan.FromSeconds(_timeRecycling));
            (current as InteractableSubject).transform.DOMove(_endPoint.position, _timeRecycling);
            await Task.Delay(TimeSpan.FromSeconds(_timeRecycling));
            current.Distribute(_outputPoints[_recycledSubjects.Count - 1], _timeDistribution, 0.5f, true);
            await Task.Delay(TimeSpan.FromSeconds(_timeDistribution));
            current.ReadyToDistribute = true;
        }
        _recyclingIsActive = false;
    }

}
