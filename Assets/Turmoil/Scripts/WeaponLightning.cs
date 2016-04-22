using UnityEngine;

public class WeaponLightning
{
    LineRenderer lr;
    Material mat;
    ParticleSystem hitPS;
    Light light;

    int nrOfPoints = 5;
    float range = 5;
    Color start = new Vector4(0.0f, 0.5f, 1.0f, 0.0f);
    Color end = new Vector4(0.0f, 0.5f, 1.0f, 0.0f);
    Vector2 offset;
    float uvSpeed = 0.05f;
    float alphaSpeed = 3.0f;

    
    public WeaponLightning(GameObject WEAPON)
    {
        //Set References
        lr = WEAPON.transform.Find("FX/LR_Lightning").GetComponent<LineRenderer>();
        light = WEAPON.GetComponentInChildren<Light>();
        hitPS = lr.GetComponentInChildren<ParticleSystem>();
        mat = lr.material;

        //Activate
        WEAPON.SetActive(true);
        lr.SetVertexCount(nrOfPoints);
        lr.useWorldSpace = true;
	}
	
	public void Update ()
    {
	    if (start.a > 0.0f)
        {
            start.a -= alphaSpeed * Time.deltaTime;
            end.a -= alphaSpeed * Time.deltaTime;
        }

        lr.SetColors(start, end);
	}

    public void LightningAttack(bool cdReady)
    {

    }

    public void StopLightningAttack()
    {

    }
}
