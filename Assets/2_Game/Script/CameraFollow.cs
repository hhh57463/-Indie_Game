using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject PlayerGams = null;

    void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x, transform.localPosition.y, -700f),
                                               new Vector3(PlayerGams.transform.localPosition.x, PlayerGams.transform.localPosition.y, -700f),
                                               10f * Time.deltaTime);
    }
}
