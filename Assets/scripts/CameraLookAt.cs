using UnityEngine;
using System.Collections;
//using DG.Tweening;

public class CameraLookAt : MonoBehaviour {

    [Tooltip("目标物体")]
    public Transform target;

    //上下选装的最大角，与最小角
    [Header("上下旋转的最大角，与最小角")]
    [Range(-90, 90)]
    public float MaxAngelX = 10;
    [Range(-90, 90)]
    public float MinAngelX = -10;
    private float angelX = 0;

    //是否限制左右选择角度
    [Header("是否限制左右旋转角度")]
    public bool isCalmpY = false;
    //左右转的角度限制
    [Header("左右转的角度限制")]
    [Range(-360, 360)]
    public float MaxAngelY = 360;
    [Range(-360, 360)]
    public float MinAngelY = -360;
    private float angelY = 0;

    private Vector3 direction = Vector3.zero;

    [Range(5, 20)]
    public float MaxDistance = 10;
    public float MinDistance = 2;
    [SerializeField]
    private float distance = 5;

    [Range(0, 2)]
    public int 鼠标按键 = 0;//鼠标按键

    public float 自转速度 = -5;

    public bool isPause = false;//外部调用，控制是否在某时，暂停鼠标的旋转控制

    void Start() {
        Mathf.Clamp(MaxAngelX, MaxAngelX, MaxAngelX);
        Mathf.Clamp(MaxAngelY, MaxAngelY, MaxAngelY);
        Mathf.Clamp(MinAngelX, MinAngelX, MinAngelX);
        Mathf.Clamp(MinAngelY, MinAngelY, MinAngelY);
        ClampAngel();
        angelX = 30;
        angelY = -30;
        distance = 4;
    }

    void Update() {

        distance -= Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance, MinDistance, MaxDistance);

        //奎特尼恩
        Quaternion q1;
        q1 = Quaternion.Euler(angelX, angelY, 0);
        //只是一个方向而已
        direction = new Vector3(0, 0, -distance);
        //只是一个位置而已
        transform.position = target.position + q1 * direction;
        //只是一个注视而已
        transform.LookAt(target.position);
        //按左键
        if (Input.GetMouseButton(鼠标按键) && !isPause) {
            angelX -= Input.GetAxis("Mouse Y");

            angelY += Input.GetAxis("Mouse X");
            //print(angelX + "  " + angelY);//----------------------------------------------------------找点用的！
            ClampAngel();
            return;
        } else {
            //当然自传的前提是！并没有限制Y的角度，否者还转它干嘛？
            if (!isCalmpY) {
                //摄像机围绕物体的自转
                angelY += Time.deltaTime * 自转速度;
                //限制Y自转角度，防止定位时，绕太多圈才回来，影响用户体验
                if (angelY > 360) {
                    angelY = 0;
                }
                if (angelY < -360) {
                    angelY = 0;
                }
            }
        }

    }

    private void ClampAngel() {
        if (angelX < MinAngelX) {
            angelX = MinAngelX;
        }
        if (angelX > MaxAngelX) {
            angelX = MaxAngelX;
        }

        if (isCalmpY) {
            if (angelY < MinAngelY) {
                angelY = MinAngelY;
            }
            if (angelY > MaxAngelY) {
                angelY = MaxAngelY;
            }
        }
    }
    //旋转到某个位置
    //public void MoveToRotation(float x1, float y1, float doDistance) {//x和y为角度，distance为距离
    //    DOTween.To(() => angelX, x => angelX = x, x1, 2);
    //    DOTween.To(() => angelY, y => angelY = y, y1, 2);
    //    DOTween.To(() => distance, z => distance = z, doDistance, 2);
    //}
}
