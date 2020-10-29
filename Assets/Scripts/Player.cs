using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    //bug  NavMesh
    private NavMeshAgent navmesh;
    [SerializeField]

    private CharacterController controller;
        [SerializeField]
    private float speed = 3.5f;
    private float gravity = 9.81f;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private GameObject hitMarkerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            muzzleFlash.SetActive(true);
            //Cross_hair shooting
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin,out hitInfo))
            {
                Debug.Log("Ray cast hit something: " + hitInfo.transform.name);
                GameObject hitMarker = (GameObject) Instantiate(hitMarkerPrefab,hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 1f);
            }
            

        }
        else
        {
            muzzleFlash.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }



        CalculateMovement();
        Physics.SyncTransforms();

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * speed;
        velocity.y += gravity;
        velocity = transform.transform.TransformDirection(velocity);

        navmesh.Move(velocity * Time.deltaTime);
    }
}
