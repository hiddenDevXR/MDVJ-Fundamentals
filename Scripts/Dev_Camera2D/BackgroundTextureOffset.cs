using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTextureOffset : MonoBehaviour
{
    MeshRenderer meshRenderer;
    float Offset = 0f;
    public float speedOffset = 5.0f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * speedOffset;
        float horizontal = Input.GetAxis("Horizontal") * step;
        Offset += horizontal;
        meshRenderer.material.SetVector("_Offset", new Vector2(Offset, 0));
    }
}
