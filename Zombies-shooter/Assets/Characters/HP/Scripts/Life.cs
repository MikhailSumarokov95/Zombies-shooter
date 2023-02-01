using UnityEngine;
using UnityEngine.AI;

public class Life : MonoBehaviour
{
    public bool IsDid { private set; get;} 

    public void Did()
    {
        if (IsDid) return;

        IsDid = true;

        if (CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().Did();
        }

        else
        {
            foreach (var component in GetComponents<MonoBehaviour>())
            {
                component.enabled = false;
            }

            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<KillCounter>().AddKilled();
        }
    }

    public void Respawn()
    {
        IsDid = false;
        var healthPoint = GetComponent<HealthPoints>();
        healthPoint.TakeHealth(healthPoint.MaxHealth);
    }
}
