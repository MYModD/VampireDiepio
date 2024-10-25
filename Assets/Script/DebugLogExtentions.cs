using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DebugLogExtentions
{
    #region ��{���\�b�h
    /// <summary>
    /// �F�ύX   ��: Color.red
    /// </summary>>
    /// <returns></returns>
    public static string SetColor(this string str, Color color)
    {
        string colorHtmlString = ColorUtility.ToHtmlStringRGBA(color);

        return $"<color=#{colorHtmlString}>{str}</color>";
    }

    /// <summary>
    /// �T�C�Y�ύX 15�W���@30+�������ł��������ł���
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sizeValue"></param>
    /// <returns></returns>
    public static string SetSize(this string str, int sizeValue)
    {
        return $"<size={sizeValue}>{str}</size>";
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetBold(this string str)
    {
        return $"<b>{str}</b>";
    }


    /// <summary>
    /// �C�^���b�N 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetItalic(this string str)
    {
        return $"<i>{str}</i>";
    }

    /// <summary>
    ///  ���s
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetEnter(this string str)
    {
        return $"{str}\n";
    }
    #endregion

    #region �֗����\�b�h



    /// <summary>
    /// �x�� �������F
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Warning(this string str)
    {
        string warningStr = str.SetColor(Color.yellow).SetBold().SetSize(15);
        return warningStr;
    }
    /// <summary>
    /// �G���[ �����ԐF
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Error(this string str)
    {
        string errorStr = str.SetColor(Color.magenta).SetBold().SetSize(15);
        return errorStr;
    }




    /// <summary>
    /// ???
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>

    public static string Gaming(this string str)
    {
        // �J���[�̔z��@���₷���F�W�߂�
        Color[] colors = { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta };

        // �����₷���悤�Ƀ��X�g��
        List<Color> availableColors = colors.ToList();

        string gamingString = "";

        foreach (char c in str)
        {

            int randomIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomIndex];

            availableColors.RemoveAt(randomIndex);

            //�@�F�����Ȃ��悤�Ƀ����_����,�s�x�����ĂȂ��Ȃ������蒼��
            if (availableColors.Count == 0)
            {
                availableColors = colors.ToList();
            }

            // ������ �J���[���@���������@�T�C�Y�ύX
            gamingString += c.ToString().SetColor(randomColor).SetBold().SetSize(30);

        }

        return gamingString;
    }



    /// <summary>
    /// ???
    /// </summary>
    /// <returns></returns>
    public static string Rainbow(this string str)
    {
        float lerp = 0f;

        // ��:0�����:240�@�܂ŃO���f�[�V�������邽�߂̒l�𕶎����Ŋ���
        // ����ŕ��������قȂ�ꍇ�ł������O���f�[�V�������ł���
        float addLerpTValue = 240f / 360f / str.Length;


        string rainbowString = "";


        foreach (char c in str)
        {
            //HSV�̂ق����Q�[�~���O���₷�� 
            Color rainColor = Color.HSVToRGB(lerp, 1, 1);
            rainbowString += c.ToString().SetColor(rainColor).SetBold().SetSize(30);

            //���Z
            lerp += addLerpTValue;

        }

        return rainbowString;

    }


    #endregion
}
