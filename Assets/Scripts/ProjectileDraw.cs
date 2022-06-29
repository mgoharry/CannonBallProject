using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDraw : MonoBehaviour
{
    CannonController controller;
    LineRenderer lr;

    [SerializeField] int lineNumberOfPoints = 60;
    [SerializeField] float timeInBetweenPoints = 0.1f;
    [SerializeField] LayerMask CollidableLayer;
    [SerializeField] SceneManagerScript sm;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CannonController>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && sm.isGameRunning && !Input.GetMouseButtonDown(0))
        {
            DrawLine();
        } else
        {
            lr.positionCount = 0;
        }
    }

    void DrawLine()
    {
        lr.positionCount = lineNumberOfPoints;
        List<Vector3> points = new List<Vector3>();

        Vector3 startPos = controller.Barrel.position;
        Vector3 startVelocity = controller.Barrel.transform.up * controller.power;

        for (float i = 0; i < lineNumberOfPoints; i += timeInBetweenPoints)
        {
            Vector3 newPoint = startPos + i * startVelocity;

            newPoint.y = startPos.y + startVelocity.y * i + Physics.gravity.y / 2f * i * i;

            //lr.transform.rotation = Quaternion.Euler(0f, 0f, controller.CannonBall.transform.rotation.z);

            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, CollidableLayer).Length > 0)
            {
                lr.positionCount = points.Count;
                break;
            }
        }



        lr.SetPositions(points.ToArray());
    }
}
