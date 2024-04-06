using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private Vector3 startPoint;
    private bool isLockedFirstPoint;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private List<GameObject> wallBlueprints = new List<GameObject>();
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        if (isLockedFirstPoint)
        {
            int fillAmount = Convert.ToInt32(Vector3.Distance(startPoint, hit.point) / wallPrefab.transform.localScale.x);
            if(fillAmount > wallBlueprints.Count)
            {
                while(fillAmount != wallBlueprints.Count)
                {
                    GameObject newBlueprint = Instantiate(wallPrefab);
                    wallBlueprints.Add(newBlueprint);
                }
            }
            else if(fillAmount < wallBlueprints.Count)
            {
                int overAmmount = wallBlueprints.Count - fillAmount;
                for(int x = 0; x < overAmmount; x++)
                {
                    Destroy(wallBlueprints[wallBlueprints.Count - 1]);
                    wallBlueprints.RemoveAt(wallBlueprints.Count - 1);
                }
            }
            for (int i = 0; i < wallBlueprints.Count; i++)
            {
                GameObject blueprint = wallBlueprints[i];
                float t = float.Parse(i + "") / float.Parse(wallBlueprints.Count + "");
                Debug.Log(i + " / " + wallBlueprints.Count + " = " + t);
                float x = (1 - t) * startPoint.x + t * hit.point.x;
                float z = (1 - t) * startPoint.z + t * hit.point.z;
                blueprint.transform.position = new Vector3(x, 0f, z);
                blueprint.transform.rotation = Quaternion.LookRotation(hit.point - startPoint) * Quaternion.Euler(0, 90f, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isLockedFirstPoint)
            {
                isLockedFirstPoint = true;
                startPoint = hit.point;
            }
        }
        
    }
}
