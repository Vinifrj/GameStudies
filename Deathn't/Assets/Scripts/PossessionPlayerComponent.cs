using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionPlayerComponent : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] turnOffInPossession = null;
    [SerializeField] float radius = 5f;
    PossessionTarget target = null;
    bool isPossessing = false;
    // Update is called once per frame
    void Update()
    {
        if (isPossessing == false)
        {
            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, radius);
            var newTarget = GetTarget(colls);
            if (target != newTarget) TintTarget(new Color(1, 1, 1, 1), false);
            target = newTarget;
            if (target)
            {
                TintTarget(new Color(0,1f,1,1), true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Possess();
                }
            }
        }
        else
        {
            transform.position = target.transform.position;
        }
    }
    private void OnDestroy()
    {
        Depossess();
    }

    void TintTarget(Color color, bool timeAlpha)
    {
        if (target == null) return;
        SpriteRenderer sr = target.gameObject.GetComponent<SpriteRenderer>();
        if (sr == null) sr = target.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (sr == null) return;
        if (timeAlpha) color.a = 0.75f + 0.25f * Mathf.Sin(10*Time.time);
        sr.color = color;
    }

    void Possess()
    {
        TintTarget(Color.white, false);
        target.enabled = true;
        isPossessing = true;
        foreach (var c in turnOffInPossession)
        {
            c.enabled = false;
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Depossess()
    {
        TintTarget(Color.white, false);
        if (target) target.enabled = false;
        isPossessing = false;
        foreach (var c in turnOffInPossession)
        {
            c.enabled = true;
        }
    }

    PossessionTarget GetTarget(Collider2D[] colls)
    {
        foreach (Collider2D coll in colls)
        {
            PossessionTarget p = coll.gameObject.GetComponent<PossessionTarget>();
            if (p != null) return p;
        }
        return null;
    }
}
