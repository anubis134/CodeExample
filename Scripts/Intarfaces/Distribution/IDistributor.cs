using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IDistributor
{
    void Distribute(Transform target, float time,float jumpPower = 3, bool lockOffset = false);
    bool ReadyToDistribute { get; set;}
    float YOffset{get; set;}
}


