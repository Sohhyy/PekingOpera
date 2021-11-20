
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
/// <summary>
/// ��3Dģ����Ϳѻ
/// </summary>
public class DrawOn3D : MonoBehaviour
{
    /// <summary>
    /// ���Ƶ�Ŀ��ͼƬ
    /// </summary>
    public RenderTexture rt;
    /// <summary>
    /// ��ˢ
    /// </summary>
    public Texture brushTexture;

    /// <summary>
    /// �հ�ͼ
    /// </summary>
    public Texture blankTexture;

    /// <summary>
    /// �������
    /// </summary>
    public Camera cam;

    /// <summary>
    /// ģ��
    /// </summary>
    public Transform modelTransform;

    public Transform rayOrigin;
    void Start()
    {
        cam = Camera.main;
        DrawBlank();
    }

    /// <summary>
    /// ��ʼ��RenderTexture
    /// </summary>
    private void DrawBlank()
    {
        // ����rt
        RenderTexture.active = rt;
        // ���浱ǰ״̬
        GL.PushMatrix();
        // ���þ���
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);


        // ������ͼ
        Rect rect = new Rect(0, 0, rt.width, rt.height);
        Graphics.DrawTexture(rect, blankTexture);

        // �����ı�
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    /// <summary>
    /// ��RenderTexture��(x,y)���괦����ˢͼ��
    /// </summary>
    private void Draw(int x, int y)
    {
        // ����rt
        RenderTexture.active = rt;
        // ���浱ǰ״̬
        GL.PushMatrix();
        // ���þ���
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);


        // ������ͼ
        x -= (int)(brushTexture.width * 0.5f);
        y -= (int)(brushTexture.height * 0.5f);
        Rect rect = new Rect(x, y, brushTexture.width, brushTexture.height);
        Graphics.DrawTexture(rect, brushTexture);

        // �����ı�
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
                // hit.textureCoord����ײ���uvֵ��uvֵ�Ǵ�0��1�ģ�����Ҫ���Կ�߲��ܵõ����������
                var x = (int)(hit.textureCoord.x * rt.width);
                // ע�⣬uv����ϵ��Graphics����ϵ��y�᷽���෴
                var y = (int)(rt.height - hit.textureCoord.y * rt.height);
                Draw(x, y);
            }
        //}

        // �����ҷ��������תģ��
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
