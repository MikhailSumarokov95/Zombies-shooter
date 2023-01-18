using UnityEngine;
using UnityEngine.AI;

public class Life : MonoBehaviour
{
    public bool IsDid { private set; get;} 
    public void OnDid()
    {
        IsDid = true;
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }

        GetComponent<NavMeshAgent>().enabled = false;
    }
}
