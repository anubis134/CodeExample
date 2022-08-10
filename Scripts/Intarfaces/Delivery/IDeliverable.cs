using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consumer;
using System.Threading.Tasks;

public interface IDeliverable:IConsumer
{
    Task TryGiveItem();
}
