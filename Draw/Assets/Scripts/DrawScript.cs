using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{

    public float minDist = 0.1f;
    public Camera myCamera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    PolygonCollider2D polygonCollider;
    List<Vector2> colliderPoints = new List<Vector2>();

    void Update() {
        Draw();
    }

    void Draw() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }
        if(Input.GetKey(KeyCode.Mouse0)) {
            Vector2 mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(mousePos, lastPos) > minDist) {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else {
            currentLineRenderer = null;
        }
    }

    void CreateBrush() {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        polygonCollider = brushInstance.GetComponent<PolygonCollider2D>();

        Vector2 mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

        polygonCollider.pathCount++;
        List<Vector2> currentPositions = new List<Vector2> {
            currentLineRenderer.GetPosition(0),
            currentLineRenderer.GetPosition(1)
        };

        List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
        polygonCollider.SetPath(0, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    void AddPoint(Vector2 pointPos) {
        if(currentLineRenderer) {
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount - 1;
            currentLineRenderer.SetPosition(positionIndex, pointPos);

            polygonCollider.pathCount++;

            List<Vector2> currentPositions = new List<Vector2> {
                currentLineRenderer.GetPosition(positionIndex-1),
                currentLineRenderer.GetPosition(positionIndex)
            };

            List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
            polygonCollider.SetPath(positionIndex-1, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
        }
    }

    List<Vector2> CalculateColliderPoints(List<Vector2> positions) {
        float width = currentLineRenderer.startWidth;

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2> {
            new Vector2(positions[0].x + offsets[0].x, positions[0].y + offsets[0].y),
            new Vector2(positions[1].x + offsets[0].x, positions[1].y + offsets[0].y),
            new Vector2(positions[1].x + offsets[1].x, positions[1].y + offsets[1].y),
            new Vector2(positions[0].x + offsets[1].x, positions[0].y + offsets[1].y)            
        };

        return colliderPositions;
    }
/*
    public Vector3[] GetPositions(LineRenderer lineRenderer) {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }
*/
}
