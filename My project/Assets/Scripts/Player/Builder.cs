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
    private GameObject wallBlueprint;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        if (isLockedFirstPoint)
        {
            if(wallBlueprint == null)
            {
                wallBlueprint = Instantiate(wallPrefab);
            }
            wallBlueprint.transform.position = (startPoint + hit.point) / 2;
            wallBlueprint.transform.rotation = Quaternion.LookRotation(hit.point - startPoint) * Quaternion.Euler(0, 90f, 0f);
            wallBlueprint.transform.localScale = new Vector3(Vector3.Distance(startPoint, hit.point), 1f, 1f);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startPoint = hit.point;
                wallBlueprint = null;
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
