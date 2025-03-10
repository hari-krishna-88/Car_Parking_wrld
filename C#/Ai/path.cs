using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public Color lineColor;  // Unity's Color type

    private List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 preveNode = Vector3.zero;
            if (i > 0)
            {
                preveNode = nodes[i - 1].position;
            }
            else if (i == 0 && nodes.Count > 1)
            {
                preveNode = nodes[nodes.Count - 1].position;
            }

            Gizmos.DrawLine(preveNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }
}
