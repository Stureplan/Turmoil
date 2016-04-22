using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour
{
    WeaponLightning WPN_LIHGTNING;


    GameObject WPN_LIGHTNING;
    GameObject WPN_FIRE;
    GameObject WPN_ICE;
    ParticleSystem WPN_LIGHTNING_HitPS;
    LineRenderer lr;
    Material mat;
    Light l;
    
    int nrOfPoints = 5;
    float range = 5;
    Color start = new Vector4(0.0f, 0.5f, 1.0f, 0.0f); 
    Color end = new Vector4(0.0f, 0.5f, 1.0f, 0.0f);
    Vector2 offset;
    float uvSpeed = 0.05f;
    float alphaSpeed = 3.0f;

    void Start ()
    {
        WPN_LIGHTNING = transform.Find("Camera/Weapons/Lightning").gameObject;
        lr = WPN_LIGHTNING.transform.Find("FX/LR_Lightning").GetComponent<LineRenderer>();
        WPN_LIGHTNING.SetActive(true);
        WPN_LIGHTNING_HitPS = lr.GetComponentInChildren<ParticleSystem>();
        l = lr.GetComponentInChildren<Light>();
        mat = lr.material;
        lr.SetVertexCount(nrOfPoints);
        lr.useWorldSpace = true;
    }
	
	void Update ()
    {
        if (start.a > 0.0f)
        {
            start.a -= alphaSpeed * Time.deltaTime;
            end.a -= alphaSpeed * Time.deltaTime;
        }
        
        lr.SetColors(start, end);
        //mat.SetTextureOffset("_MainTex", offset);
    }

    public void Attack(WeaponTypes type)
    {
        switch (type)
        {
            case WeaponTypes.LIGHTNING_PRIMARY:
                //attack lightning
                break;

        }

    }

    public void LightningAttack(bool cdReady)
    {
        lr.enabled = true;

        start = new Vector4(0.0f, 0.5f, 1.0f, 1.0f);
        end = new Vector4(0.0f, 0.5f, 1.0f, 1.0f);
        
        lr.SetColors(start, end);

        offset.x = offset.x - uvSpeed;
        mat.SetTextureOffset("_MainTex", offset);

        Ray ray = new Ray(lr.transform.position, lr.transform.forward);
        RaycastHit hit;

        lr.SetPosition(0, lr.transform.position);

        if (Physics.Raycast(ray, out hit, range))
        {
            //Hit something
            Vector3 dist = hit.point - lr.transform.position;
            Vector3 len = dist / (nrOfPoints - 1);

            for (int i = 0; i < nrOfPoints; i++)
            {
                Vector3 pos = lr.transform.position + len * i;
                if (i != 0 && i != nrOfPoints)
                {
                    pos = RandomizeFrom(pos, 0.02f);
                }
                lr.SetPosition(i, pos);


            }

            WPN_LIGHTNING_HitPS.transform.position = hit.point;
            WPN_LIGHTNING_HitPS.transform.rotation = Quaternion.LookRotation(hit.normal);


            if (cdReady)
            {
                //Deal damage
                WPN_LIGHTNING_HitPS.Emit(15);
                l.enabled = true;
            }
        }
        else
        {
            Vector3 dist = ray.direction * range;
            Vector3 len = dist / (nrOfPoints - 1);

            for (int i = 0; i < nrOfPoints; i++)
            {
                Vector3 pos = lr.transform.position + len * i;
                lr.SetPosition(i, pos);
            }

            WPN_LIGHTNING_HitPS.transform.position = lr.transform.position + dist;
            l.enabled = true;

        }




    }

    public void StopAttack(WeaponTypes type)
    {
        switch (type)
        {
            case WeaponTypes.LIGHTNING_PRIMARY:
                WPN_LIHGTNING.StopLightningAttack();
                break;
        }
    }

    public void StopLightningAttack()
    {
       // lr.enabled = false;
        ParticleSystem.EmissionModule em = WPN_LIGHTNING_HitPS.emission;
        em.enabled = false;
        WPN_LIGHTNING_HitPS.transform.position = transform.position;
        l.enabled = false;

    }

    Vector3 RandomizeFrom(Vector3 origin, float amount)
    {
        Vector3 offset = new Vector3(0, 0, 0);
        offset.x = Random.Range(-amount, amount);
        offset.y = Random.Range(-amount, amount);

        return origin + offset;
    }
}
