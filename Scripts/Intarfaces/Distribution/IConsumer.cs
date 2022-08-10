using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Consumer
{
public interface IConsumer
{
   void TryDistibute(IDistributor distributor);
}
}