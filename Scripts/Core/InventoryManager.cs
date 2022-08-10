using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    internal static List<IDistributor> AllDistributors = new List<IDistributor>();

    internal static float OffsetCoeeficient = 0.55f;


    internal static void RemoveLastDistributor()
    {
       AllDistributors.Remove(AllDistributors[AllDistributors.Count - 1]);
    }

    internal static IDistributor ReturnLastDistributor()
    {
        return AllDistributors[AllDistributors.Count - 1];
    }
}
