using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DeliveryItems : MonoBehaviour, IDeliverable
{
    [SerializeField]
    private GameObject _itemPrefab;
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;
    [SerializeField]
    private int _countOfItems = 3;
    [SerializeField]
    private List<Transform> _pointsOfItems = new List<Transform>();
    internal List<IDistributor> _distributors = new List<IDistributor>();
    [SerializeField]
    private Transform _targetPoint;



    async void Start()
    {
        await StartDelivery();
    }

    private void CreatePullOfItems()
    {
        int indexOfPosition = 0;
        for (int i = 0; i < _countOfItems; i++)
        {

            Transform item = Instantiate(_itemPrefab).transform;
            _distributors.Add(item.GetComponent<IDistributor>());
            item.transform.parent = _pointsOfItems[indexOfPosition].transform;
            item.transform.position = new Vector3(_pointsOfItems[indexOfPosition].position.x, _pointsOfItems[indexOfPosition].position.y + (int)(i / 4), _pointsOfItems[indexOfPosition].position.z);
            indexOfPosition++;
            if (indexOfPosition == 4) indexOfPosition = 0;
        }
    }

    private async Task StartDelivery()
    {
        if (this)
        {
            transform.DOLocalMoveX(_startPoint.localPosition.x, 5f);
            await Task.Delay(TimeSpan.FromSeconds(5f));
            CreatePullOfItems();
            transform.DOLocalMoveX(_endPoint.localPosition.x, 5f);
            await Task.Delay(TimeSpan.FromSeconds(5f));
        }
    }

    public void TryDistibute(IDistributor distributor)
    {
        print(distributor);
        distributor.Distribute(_targetPoint, 0.3f);
    }

    public async Task TryGiveItem()
    {
        if (_distributors.Count is 0) return;

        while (_distributors.Count > 0)
        {
            InventoryManager.AllDistributors.Add(_distributors[_distributors.Count - 1]);
            TryDistibute(_distributors[_distributors.Count - 1]);
            _distributors.RemoveAt(_distributors.Count - 1);
            await Task.Delay(TimeSpan.FromSeconds(0.2f));
        }
        await StartDelivery(); 
    }

}
