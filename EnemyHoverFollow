public class EnemyHoverFollow : MonoBehaviour {
 
    [SerializeField]Transform target;
    [SerializeField]float movementSpeed = 10.0f;
    [SerializeField]float rotationalDamp = 0.5f;
    [SerializeField]float rayCastOffset = 2.5f;
    [SerializeField]float detectionDistance = 20.0f;
 
 
    void Start(){
    target = GameObject.Find("Player").transform;
    }
 
    void Update(){
        Pathfinding ();
        Move ();
    }
 
    void Turn(){
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation (pos);
        transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }
 
    void Move(){
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
 
    void Pathfinding(){
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;
 
        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;
 
        Debug.DrawRay (left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay (right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay (up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay (down, transform.forward * detectionDistance, Color.cyan);
 
        //Right and Left testing
        if(Physics.Raycast(left, transform.forward, out hit, detectionDistance)){
            raycastOffset += Vector3.right;
        }
        else if(Physics.Raycast(right, transform.forward, out hit, detectionDistance)){
            raycastOffset -= Vector3.right;
        }
 
        //Up and Down testing
        if(Physics.Raycast(up, transform.forward, out hit, detectionDistance)){
            raycastOffset -= Vector3.up;
        }
        else if(Physics.Raycast(down, transform.forward, out hit, detectionDistance)){
            raycastOffset += Vector3.up;
        }
 
        if (raycastOffset != Vector3.zero) {
            transform.Rotate (raycastOffset * 5.0f * Time.deltaTime);
        } else {
            Turn ();
        }
    }
}
