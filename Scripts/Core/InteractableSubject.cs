using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class InteractableSubject : MonoBehaviour, IDistributor
{
    private bool _readyToDistribute = true;
    public bool ReadyToDistribute
    {
        get => _readyToDistribute;

        set => _readyToDistribute = value;
    }
    private float _yOffset;

    public float YOffset
    {

        get => _yOffset;

        set
        {
            _yOffset = value;
        }

    }

    public void Distribute(Transform target, float time, float jumpPower = 3, bool lockOffset = false)
    {
        if (!lockOffset)
        {
            YOffset = (InventoryManager.AllDistributors.Count - 1) * InventoryManager.OffsetCoeeficient;
        }

        transform.DOJump(target.position + Vector3.up * _yOffset, 3, 1, time).OnComplete(() =>
               {
                   transform.parent = target.transform;
                   transform.localPosition = Vector3.zero + Vector3.up * _yOffset;
               });
        print("Distrib");
    }

}
