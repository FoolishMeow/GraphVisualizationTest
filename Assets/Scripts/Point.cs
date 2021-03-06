using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
    public string Id;
    public JSONObject Label;
    public string Type;
    public Vector3 SphericalPosition;
    public Vector3 HierarchyPosition;
    public List<GameObject> RelatedLines;
}
