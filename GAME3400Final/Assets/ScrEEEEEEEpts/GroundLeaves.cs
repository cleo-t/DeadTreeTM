using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLeaves : MonoBehaviour
{
    [HideInInspector]
    public float leafPercent = 1;

    [SerializeField]
    private GameObject leafPrefab;
    [SerializeField]
    private float maxLeafCount = 100;
    [SerializeField]
    private float radius = 15;
    [SerializeField]
    private float heightOffset = 0.01f;
    [SerializeField]
    private List<string> groundLayers;

    private List<GameObject> leafObjects;
    private float prevLeafPercent;

    private struct LeafSpot
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    void Start()
    {
        this.leafObjects = new List<GameObject>();
        this.prevLeafPercent = this.leafPercent;

        List<LeafSpot> leafSpots = this.GetLeafSpots();
        this.PlaceLeaves(leafSpots);
    }

    private void PlaceLeaves(List<LeafSpot> leafSpots)
    {
        foreach(LeafSpot ls in leafSpots)
        {
            GameObject obj = Instantiate(this.leafPrefab, ls.position, ls.rotation);
            obj.transform.parent = this.transform;
            this.leafObjects.Add(obj);
        }
    }

    private List<LeafSpot> GetLeafSpots()
    {
        List<LeafSpot> result = new List<LeafSpot>();
        for(int i = 0; i < maxLeafCount; i++)
        {
            // Get random position
            float randTheta = Random.Range(0, 2 * Mathf.PI);
            float randMag = Random.Range(0, this.radius);
            Vector3 randOffset = (Vector3.right * Mathf.Cos(randTheta)
                + Vector3.forward * Mathf.Sin(randTheta)) * randMag;
            Vector3 raycastOrigin = this.transform.position + randOffset;
            // Raycast down to find ground
            Ray ray = new Ray(raycastOrigin, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask(this.groundLayers.ToArray()))) {
                // Otherwise, get normal and calculate rotation
                // Add to list
                LeafSpot ls;
                ls.position = hit.point + (hit.normal.normalized * this.heightOffset);
                Quaternion randRotation = Quaternion.AngleAxis(Random.Range(0, 2 * Mathf.PI), Vector3.up);
                ls.rotation = this.RotationFromNormal(hit.normal) * randRotation;
                result.Add(ls);
            }
            // If no ground found, continue
        }
        return result;
    }

    // Point the "Up" vector along the normal
    private Quaternion RotationFromNormal(Vector3 normal)
    {
        return Quaternion.LookRotation(normal, Vector3.right) * Quaternion.AngleAxis(90, Vector3.right);
    }

    void Update()
    {
        this.leafPercent = Mathf.Clamp(this.leafPercent, 0, 1);
        if (leafPercent == 0)
        {
            this.leafPercent = this.prevLeafPercent;
        }
        if (this.leafPercent != this.prevLeafPercent)
        {
            this.FadeLeaves();
        }
        this.prevLeafPercent = leafPercent;
    }

    private void FadeLeaves()
    {
        int lowestIndex = (int)Mathf.Ceil(this.prevLeafPercent * this.leafObjects.Count);
        int highestIndex = (int)Mathf.Floor(this.leafPercent * this.leafObjects.Count);
        bool setActive = true;
        if (lowestIndex > highestIndex)
        {
            int t = lowestIndex;
            lowestIndex = highestIndex;
            highestIndex = t;
            setActive = false;
        }
        for(int i = lowestIndex; i < highestIndex; i++)
        {
            this.leafObjects[i].SetActive(setActive);
        }
    }
}
