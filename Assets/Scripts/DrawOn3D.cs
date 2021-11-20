
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
/// <summary>
/// 在3D模型上涂鸦
/// </summary>
public class DrawOn3D : MonoBehaviour
{
    /// <summary>
    /// 绘制的目标图片
    /// </summary>
    public RenderTexture rt;
    /// <summary>
    /// 笔刷
    /// </summary>
    public Texture brushTexture;

    /// <summary>
    /// 空白图
    /// </summary>
    public Texture blankTexture;

    /// <summary>
    /// 主摄像机
    /// </summary>
    public Camera cam;

    /// <summary>
    /// 模型
    /// </summary>
    public Transform modelTransform;

    public Transform rayOrigin;
    void Start()
    {
        cam = Camera.main;
        DrawBlank();
    }

    /// <summary>
    /// 初始化RenderTexture
    /// </summary>
    private void DrawBlank()
    {
        // 激活rt
        RenderTexture.active = rt;
        // 保存当前状态
        GL.PushMatrix();
        // 设置矩阵
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);


        // 绘制贴图
        Rect rect = new Rect(0, 0, rt.width, rt.height);
        Graphics.DrawTexture(rect, blankTexture);

        // 弹出改变
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    /// <summary>
    /// 在RenderTexture的(x,y)坐标处画笔刷图案
    /// </summary>
    private void Draw(int x, int y)
    {
        // 激活rt
        RenderTexture.active = rt;
        // 保存当前状态
        GL.PushMatrix();
        // 设置矩阵
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);


        // 绘制贴图
        x -= (int)(brushTexture.width * 0.5f);
        y -= (int)(brushTexture.height * 0.5f);
        Rect rect = new Rect(x, y, brushTexture.width, brushTexture.height);
        Graphics.DrawTexture(rect, brushTexture);

        // 弹出改变
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    private void Update()
    {
        var handJointService = Microsoft.MixedReality.Toolkit.CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if (handJointService != null)
        {
            Transform jointTransform = handJointService.RequestJointTransform(TrackedHandJoint.IndexTip, Handedness.Right);
            rayOrigin = jointTransform;
            
            // ...
        }

        //if (Input.GetMouseButton(0))
        //{
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,0.2f))
            {
                // hit.textureCoord是碰撞点的uv值，uv值是从0到1的，所以要乘以宽高才能得到具体坐标点
                var x = (int)(hit.textureCoord.x * rt.width);
                // 注意，uv坐标系和Graphics坐标系的y轴方向相反
                var y = (int)(rt.height - hit.textureCoord.y * rt.height);
                Draw(x, y);
            }
        //}

        // 按左右方向键，旋转模型
        //if(Input.GetKey(KeyCode.LeftArrow))
        //{
        //    modelTransform.Rotate(0, 360 * Time.deltaTime, 0);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    modelTransform.Rotate(0, -360 * Time.deltaTime, 0);
        //}
    }
}
