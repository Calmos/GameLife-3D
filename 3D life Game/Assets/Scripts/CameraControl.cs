using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector2 rotation;
    [SerializeField] float turnSpeed = 1;
    [SerializeField] float speed = 10;

    [SerializeField] GameObject bloc;
    [SerializeField] LayerMask mask;

    GameManager gm;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        #region deplacements
        rotation.x += -Input.GetAxisRaw("Mouse Y");
        rotation.y += Input.GetAxisRaw("Mouse X");

        gameObject.transform.eulerAngles = rotation * turnSpeed;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float height = 0;
        if (Input.GetKey(KeyCode.Space)) height = 1;
        if (Input.GetKey(KeyCode.C)) height = -1;

        if (h != 0 || v != 0 || height != 0)
        {
            Vector3 direction = Vector3.zero;

            direction = new Vector3(h, height, v);

            transform.Translate(direction * Time.deltaTime * speed, Space.Self);
        }
        #endregion

        #region blocs
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
            {
                if (hit.collider.gameObject.tag != "Unbreakable")
                {
                    gm.blocs.Remove(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
            {
                Instantiate(bloc, hit.collider.gameObject.transform.position + hit.normal, Quaternion.identity);
            }
        }
        #endregion
    }
}
