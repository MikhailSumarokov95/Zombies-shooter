using UnityEngine;
using UnityEngine.AI;

public class Life : MonoBehaviour
{
    public bool IsDid { private set; get;} 
    public void Did()
    {
        if (IsDid) return;
        IsDid = true;

        foreach (var component in GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;

        if (!CompareTag("Player")) FindObjectOfType<KillCounter>().AddKilled();
    }
}
