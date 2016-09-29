using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SwingFeedback : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    public bool callculateSurface;

    public Rigidbody rgbd;
    private BoxCollider boxCollider;

    // Start Function
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        boxCollider = rgbd.GetComponent<Collider>() as BoxCollider;

        // Check if Unity resetet input again 
        if (rgbd == null) {
            Debug.Log("Rigidbody is null");
        }
    }

    // Fixed Update
    // Used for air resistance calculation
    void FixedUpdate() {

        if (controller == null) {
            return;
        }

        // Values for air drag
        float surface = 0.225f;
        float dragcoeff = 0.8f; // 0.8 - 1
        float speed = rgbd.velocity.magnitude;

        // (WrkInPrgs)
        // Realtime surface space detection
        if (callculateSurface) {
            if (speed > 1) {
                //surface = CallculateSurface();
            }
        }
        
        // Equation --> force = 0.5 * surface * dragcoeff * airdensity * speed^2
        float force = 0.5f * surface * dragcoeff * 1.2f * Mathf.Pow(speed, 2);

        if (force > 1) {
            float strength = force * 100;
            controller.TriggerHapticPulse((ushort)strength);
        }
    }






    // --- Work in Progress --- //
    // ---    Maybe to OP   --- //
    // Callculate Surface
    // Collect all data from object and send it to cube class
    float CallculateSurface() {

        // 3D object to plane projection 
        Vector3 normal = rgbd.velocity;
        Vector3 ccentr = boxCollider.center;
        Vector3 csize = boxCollider.size;

        float csizeX = csize.x / 2;
        float csizeY = csize.y / 2;
        float csizeZ = csize.z / 2;

        // Set cube vertecies 
        Vector3[] pointPlane = new Vector3[8]; 
        Vector3[] points = {   new Vector3(ccentr.x + csizeX, ccentr.y + csize.y / 2, ccentr.z + csize.z / 2),
                               new Vector3(ccentr.x - csizeX, ccentr.y + csize.y / 2, ccentr.z + csize.z / 2),
                               new Vector3(ccentr.x + csizeX, ccentr.y - csize.y / 2, ccentr.z + csize.z / 2),
                               new Vector3(ccentr.x - csizeX, ccentr.y - csize.y / 2, ccentr.z + csize.z / 2),
                               new Vector3(ccentr.x + csizeX, ccentr.y + csize.y / 2, ccentr.z - csize.z / 2),
                               new Vector3(ccentr.x + csizeX, ccentr.y - csize.y / 2, ccentr.z - csize.z / 2),
                               new Vector3(ccentr.x - csizeX, ccentr.y + csize.y / 2, ccentr.z - csize.z / 2),
                               new Vector3(ccentr.x - csizeX, ccentr.y - csize.y / 2, ccentr.z - csize.z / 2)
                            };


        // Set points to world space and create projection
        for (int i = 0; i < points.Length; i++) {
            points[i] = LocalToWorldSpaceForBoxCollider(points[i]);
            pointPlane[i] = Vector3.ProjectOnPlane(points[i], normal);

            // Delete later just for visualisation
            //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            //sphere.transform.position = pointPlane[i];
        }


        // Cube object with functions
        Cube cube = new Cube();
        cube.Init(points, normal);
        cube.isolatePoints();
        //float surface = cube.calculateSurfacespace();

        return 0.225f;
    } 


    // Translate local point to world point for box collider
    Vector3 LocalToWorldSpaceForBoxCollider(Vector3 localPoint) {
        Vector3 worldPoint = boxCollider.transform.TransformPoint(localPoint);
        return worldPoint; 
    }

}







// Wrong Theorie - Projecting points on plane to callculate the surface space
// (1) Get points of cube an project them on one plane with Unitys Vector.PlaneProject
// (2) Isolate Points wich lie in other cube surface planes
// (3) Take other points find center and create triangles 
// (4) Calculate triangle surface space and add them together



// Plane Class
// Planes should calculate if they have a point 
public class SPlane {
    public List<Vector3> points;

    public bool hasPoint(Vector3 mpoint, Vector3 vnormal) {
        if (points.Contains(mpoint)) {
            return false;
        } else if (Vector3.Dot((mpoint - points[0]), vnormal) == 0) {

            // R.I.P: here lies the brain failure of J.W.D.
            // All points lie on the same plane, so normal form calculation should be always zero
            // Is not zero ... go f** urself 

            return true;
        } else {
            Debug.Log(points[0]);
            Debug.Log(Vector3.Dot((mpoint - points[0]), vnormal));
            return false;
        }
    }
}


// Cube Class
// Callculate surface space
public class Cube {

    public SPlane[] planes;
    private Vector3 normalVec;
    private Vector3[] allPoints;
    private List<Vector3> selPoints;

    // Init cube  with planes array 
    public void Init(Vector3[] points, Vector3 normal) {


        // Create array with planes and initialize them because they are fucking nulled
        planes = new SPlane[6];
        for (int i = 0; i < planes.Length; i++) {
            planes[i] = new SPlane();
        }

        // Fill array elements with cube plane values
        planes[0].points = new List<Vector3> { points[0], points[1], points[2], points[3] };
        planes[1].points = new List<Vector3> { points[1], points[3], points[6], points[7] };
        planes[2].points = new List<Vector3> { points[3], points[2], points[7], points[5] };
        planes[3].points = new List<Vector3> { points[0], points[2], points[4], points[5] };
        planes[4].points = new List<Vector3> { points[1], points[0], points[6], points[4] };
        planes[5].points = new List<Vector3> { points[6], points[7], points[4], points[5] };

        // Set all points and normal vector
        allPoints = points;
        normalVec = normal;
    }

    // Isolate overlapping points 
    public void isolatePoints() {

        // Points that aren't covered
        selPoints = new List<Vector3>();

        // Check if point is overlapping with plane
        foreach (Vector3 point in allPoints) {

            // Add point to list
            selPoints.Add(point);

            // Check if point should be removed
            foreach (SPlane plane in planes) {
                if (plane.hasPoint(point, normalVec)) {
                    selPoints.Remove(point);
                }
            }
        }
    }

    public float calculateSurfacespace() {
        Debug.Log(selPoints);

        return 0.225f;
    }
}