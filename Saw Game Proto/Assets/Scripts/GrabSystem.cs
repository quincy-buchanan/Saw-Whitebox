using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Transform slot;

    private PickableItem pickedItem;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (pickedItem)
            {
                DropItem(pickedItem);
            }
            else
            {
                var ray = camera.ViewportPointToRay(Vector3.one * .5f);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1.5f))
                {
                    var pickable = hit.transform.GetComponent<PickableItem>();

                    if(pickable)
                    {
                        PickItem(pickable);
                    }
                }
            }
        }
    }

    void PickItem(PickableItem item)
    {
        pickedItem = item;

        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        item.transform.SetParent(slot);
    }

    void DropItem(PickableItem item)
    {
        pickedItem = null;

        item.transform.SetParent(null);

        item.Rb.isKinematic = false;

        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
