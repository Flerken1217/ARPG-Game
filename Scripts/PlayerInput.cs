using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string keyUp="w";
    public string keyDown="s";
    public string keyLeft="a";
    public string keyRight="d";

    //控制角色是否能移动
    public bool inputEnabled=true;
    //关于角色的移动
    public float Dup;//在z轴上移动的距离
    public float Dright;//在y轴上移动的距离，左右动
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDup=(Input.GetKey(keyUp)?1.0f:0)-(Input.GetKey(keyDown)?1.0f:0);
        targetDright=(Input.GetKey(keyRight)?1.0f:0)-(Input.GetKey(keyLeft)?1.0f:0);

        //soft开关，因为直接强行取消整个模块所有数值会乱掉，必须用soft开关强制其清零。
        if(inputEnabled==false){
            targetDup=0;
            targetDup=0;
            //因为目标的位移为0，所以不会产生移动，玩家的Dup和Dright都会变成0
            //但是取消的一瞬间不会瞬间为0，因为底下还有smooth函数，会慢慢变成0
        }

        //采用SmoothDamp方法，让数值不是突变地从0~1or-1，而是慢慢地晃过去
        //SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
        Dup=Mathf.SmoothDamp(Dup,targetDup,ref velocityDup,0.1F);
        Dright=Mathf.SmoothDamp(Dright,targetDright,ref velocityDright,0.1f);

    }
}
