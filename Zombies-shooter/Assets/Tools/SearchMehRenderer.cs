using Unity.VisualScripting;
using UnityEngine;

public class SearchMeshRenderer : MonoBehaviour
{
    [SerializeField] private LODGroup lodPref;

    [SerializeField] private MeshRenderer meshRenderer;
    [ContextMenu("MeshSearch")]
    public void MeshSearch()
    {
        var meshs = FindObjectsOfType<MeshRenderer>();

        for (var i = 0; i < meshs.Length; i ++)
        {
            if (meshs[i].transform.parent?.GetComponent<MeshRenderer>() == null)
            {
                var meshLod = meshs[i].AddComponent<LODGroup>();
                meshLod.SetLODs(lodPref.GetLODs());
                
                var meshRed = meshs[i].GetComponent<MeshRenderer>();

                var meshLods = meshLod.GetLODs();

                meshLods[0].renderers[0] = meshRed;

                meshLod.size = 1;

                meshLod.SetLODs(meshLods);
            }
        }
    }
}
