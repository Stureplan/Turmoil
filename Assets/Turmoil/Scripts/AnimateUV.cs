using UnityEngine;
using System.Collections;

public class AnimateUV : MonoBehaviour {

    [SerializeField]
    public Vector2 offset;
    Material[] mat;
    SkinnedMeshRenderer mr;
    Color col;

    float uvSpeed = 0.0001f;

    // Use this for initialization
    void Start()
    {
        mr = GetComponent<SkinnedMeshRenderer>();
        mat = mr.materials;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float a = 0.3f + Mathf.PingPong(Time.time, 0.5f);
        Color c = new Vector4(1.0f, 1.0f, 1.0f, a);

        offset.y = offset.y + uvSpeed * Time.deltaTime;

        mat[1].SetColor("_TintColor", c);
        mat[1].SetTextureOffset("_MainTex", offset);
    }

    public void SetUVSpeed(float s)
    {
        uvSpeed = s;
    }
}
