using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ControlState
{
    virtual public ControlState Handle()
    {
        return this;
    }
}

class IdleState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return new JumpState();
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
            return new RunState();
        if (Input.GetKey(KeyCode.LeftShift))
            return new CrouchState();

        if (Input.GetMouseButtonDown(1))
            return new InteractState();
        return this;
    }
}

class RunState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return new JumpState();
        if (Input.GetKey(KeyCode.W) ||
        Input.GetKey(KeyCode.A) ||
        Input.GetKey(KeyCode.S) ||
        Input.GetKey(KeyCode.D))
            return new RunState();
        return new IdleState();
    }
}

class JumpState : ControlState
{
    public override ControlState Handle()
    {
        Debug.Log("Jump");
        return this;
    }
}

class LandState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetKeyDown(KeyCode.W) ||
        Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.S) ||
        Input.GetKeyDown(KeyCode.D))
            return new LandMoveState();
        return this;
    }
}

class LandMoveState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetKey(KeyCode.W) ||
        Input.GetKey(KeyCode.A) ||
        Input.GetKey(KeyCode.S) ||
        Input.GetKey(KeyCode.D))
            return this;
        return new LandState();
    }
}


class CrouchState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return new JumpState();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
                return new CrouchRunState();
            return this;
        }
            

        return new IdleState();
    }
}

class CrouchRunState : ControlState
{
    
    public override ControlState Handle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return new JumpState();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
                return new CrouchRunState();
        }
        return new CrouchState();
    }
}

class InteractState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetMouseButton(1))
            return new MoveObjectState();
        return new IdleState();
    }
}

class MoveObjectState : ControlState
{
    public override ControlState Handle()
    {
        if (Input.GetMouseButton(1))
            return new MoveObjectState();
        return new IdleState();
    }
}