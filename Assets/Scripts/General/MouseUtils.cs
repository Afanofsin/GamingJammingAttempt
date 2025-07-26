using UnityEngine;

public static class MouseUtils
{
    private static Camera cachedCamera;

    private static Camera GetMainCamera()
    {
        // If cached camera is null or destroyed, get a new one
        if (cachedCamera == null)
        {
            cachedCamera = Camera.main;
        }
        return cachedCamera;
    }
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        Camera camera = GetMainCamera();
        if (camera == null)
        {
            Debug.LogError("No main camera found!");
            return Vector3.zero;
        }

        Plane dragPlane = new(camera.transform.forward, new Vector3(0, 0, zValue));
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
