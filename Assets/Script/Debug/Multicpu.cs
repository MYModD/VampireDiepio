using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Multicpu : MonoBehaviour
{
    private bool Flag_loop = true;//�������[�v����p�t���O��ݒ肵�A�g����

    void Start()
    {
        Thread_1();
    }

    void OnApplicationQuit()//�A�v���I�����̏����i�������[�v������j
    {
        Flag_loop = false;//�������[�v�t���O��������
    }

    public void Thread_1()//�������[�v�{��
    {
        Task.Run(() =>
        {
            while (Flag_loop)//�������[�v�t���O���`�F�b�N
            {
                try
                {
                    Debug.Log("Test");
                }
                catch (System.Exception e)//��O���`�F�b�N
                {
                    Debug.LogWarning(e);//�G���[��\��
                }
            }
        });
    }
}
