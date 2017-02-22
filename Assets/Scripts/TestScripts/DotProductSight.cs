using UnityEngine;
using System.Collections;

public class DotProductSight : MonoBehaviour {

    [SerializeField]
    private Transform objectToView;

    [SerializeField]
    private float turnSpeed;

    private float fwdDotProduct;
    private float sideDotProduct;

	// Update is called once per frame
	void Update()
	{
        transform.Rotate(Vector3.up * turnSpeed * Input.GetAxis("Horizontal") * Time.smoothDeltaTime);

        fwdDotProduct = Vector3.Dot(transform.forward, (objectToView.position - transform.position).normalized);
        sideDotProduct = Vector3.Dot(transform.right, (objectToView.position - transform.position).normalized);

    }
}