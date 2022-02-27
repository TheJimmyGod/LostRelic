using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    protected float mDestroyTime = 5.0f;
    [SerializeField]
    protected int mHealth = 0;
    public Transform mTarget = null;
    public bool mCoolTime = false;

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

    public virtual void Interact(Transform player) {
        transform.parent = null;
        mTarget = player; 
    }
    public void DestoryObject() { Destroy(gameObject, mDestroyTime); }
    public void Disconnect() { mTarget = null; }
}
