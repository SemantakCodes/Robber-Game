using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform hand;

    GameObject heldItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                TryPickup();
            }
        }}


    void TryPickup()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldItem = hit.collider.gameObject;

                heldItem.transform.SetParent(hand);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.transform.localRotation = Quaternion.identity;

                Rigidbody rb = heldItem.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;
            }
        }
    }

    void OnGUI()
{
    GUIStyle style = new GUIStyle();
    style.fontSize = 40;   // increase cursor size
    style.normal.textColor = Color.white;

    GUI.Label(
        new Rect(Screen.width / 2 - 20, Screen.height / 2 - 20, 40, 40),
        "+",
        style
    );
}
}

