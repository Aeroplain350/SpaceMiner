using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBarriers : MonoBehaviour
{
    public GameObject[] barriers;
    public Camera cameraObject;

    private void Awake()
    {
        cameraObject = FindObjectOfType<Camera>();
    }

    void Start()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            switch (i)
            {
                case 0:
                    Vector3 topBarrierLocation = cameraObject.ViewportToWorldPoint(new Vector3(0.5f, 0.95f, cameraObject.nearClipPlane));
                    barriers[i].transform.position = topBarrierLocation;
                    break;
                case 1:
                    Vector3 rightBarrierLocation = cameraObject.ViewportToWorldPoint(new Vector3(1f, 0.5f, cameraObject.nearClipPlane));
                    barriers[i].transform.position = rightBarrierLocation;
                    break;
                case 2:
                    Vector3 bottomBarrierLocation = cameraObject.ViewportToWorldPoint(new Vector3(0.5f, 0.025f, cameraObject.nearClipPlane));
                    barriers[i].transform.position = bottomBarrierLocation;
                    break;
                case 3:
                    Vector3 leftBarrierLocation = cameraObject.ViewportToWorldPoint(new Vector3(0f, 0.5f, cameraObject.nearClipPlane));
                    barriers[i].transform.position = leftBarrierLocation;
                    break;
            }
        }
    }
}
