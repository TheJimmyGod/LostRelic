using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    protected float mDestroyTime = 5.0f;
    [SerializeField]
    protected int mHealth = 0;
    protected int mVaildDistance = 10;
    public Transform mTarget = null;


    void Start() => OnStart();
    void Update() => OnUpdate();
    protected virtual void OnStart()
    {
        
    }
    protected virtual void OnUpdate()
    {
    }

    protected bool Death
    {
        get { return mHealth <= 0; }
    }

    public virtual void Interact(Transform player) { mTarget = player; }
    public void DestoryObject() { Destroy(gameObject, mDestroyTime); }
}
