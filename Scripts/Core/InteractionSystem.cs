using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Consumer;
using System.Threading.Tasks;

public class InteractionSystem : MonoBehaviour, IConsumer
{
    [SerializeField]
    private Transform _stackPositionTransform;
    [SerializeField]
    private float _timeDistribution;

    public void TryDistibute(IDistributor distributor)
    {
        distributor.Distribute(_stackPositionTransform, _timeDistribution);
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDistributor distributor))
        {
            if (distributor.ReadyToDistribute)
            {
                distributor.ReadyToDistribute = false;
                InventoryManager.AllDistributors.Add(distributor);
                TryDistibute(distributor);
            }

        }
        else if (other.TryGetComponent(out IDeliverable deliverable))
        {
            print("deliverable");
            await deliverable.TryGiveItem();
         } else if (other.TryGetComponent(out ConveyorController conveyor))
        {
            await conveyor.TryAddSubject();
        }
    }
}
