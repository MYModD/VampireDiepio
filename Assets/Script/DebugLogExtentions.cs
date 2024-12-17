using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Unity�̃f�o�b�O���O�ɑ����@�\��ǉ�����g�����\�b�h�N���X
/// ���b�`�e�L�X�g���g�p
/// </summary>
public static class DebugLogExtentions
{
    #region ��{���\�b�h
    /// <summary>
    /// ������̐F��ύX
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <param name="color">�K�p����F�iColor�N���X�j</param>
    /// <returns>HTML�J���[�^�O�ő������ꂽ������</returns>
    /// <example>
    /// �g�p��: "�e�L�X�g".SetColor(Color.red)
    /// </example>
    public static string SetColor(this string str, Color color)
    {
        string colorHtmlString = ColorUtility.ToHtmlStringRGBA(color);
        return $"<color=#{colorHtmlString}>{str}</color>";
    }


    /// <summary>
    /// ������̃T�C�Y��ύX
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <param name="sizeValue">�t�H���g�T�C�Y�i15���W���A30�ȏ�ő����̑傫�߃e�L�X�g�j</param>
    /// <returns>�T�C�Y�^�O�ő������ꂽ������</returns>
    public static string SetSize(this string str, int sizeValue)
    {
        return $"<size={sizeValue}>{str}</size>";
    }


    /// <summary>
    /// ������𑾎��ɕύX
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>�����^�O�ő������ꂽ������</returns>
    public static string SetBold(this string str)
    {
        return $"<b>{str}</b>";
    }



    /// <summary>
    /// ��������Α�
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>�Α̃^�O�ő������ꂽ������</returns>
    public static string SetItalic(this string str)
    {
        return $"<i>{str}</i>";
    }


    /// <summary>
    /// ������̖����ɉ��s��ǉ�
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>���s���ǉ����ꂽ������</returns>
    public static string SetEnter(this string str)
    {
        return $"{str}\n";
    }
    #endregion






    #region �֗����\�b�h


    /// <summary>
    /// ���F�ő����̑���
    /// - ���F
    /// - ����
    /// - �T�C�Y15
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>�^�O�ő������ꂽ������</returns>
    public static string BoldYellow(this string str)
    {
        string boldYString = str.SetColor(Color.yellow).SetBold().SetSize(15);
        return boldYString;
    }

    /// <summary>
    /// �ԐF�ő����̑���
    /// - �}�[���^
    /// - ����
    /// - �T�C�Y15
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>�^�O�ő������ꂽ������</returns>
    public static string BoldRed(this string str)
    {
        string errorStr = str.SetColor(Color.magenta).SetBold().SetSize(15);
        return errorStr;
    }

    /// <summary>
    /// �Q�[�~���O���̑�����K�p���܂��i�e�����������_���ȐF�ŕ\���j
    /// - �������ƂɌ��܂��������_���ȐF�ired, yellow, blue, green, magenta�j
    /// - ����
    /// - �T�C�Y30
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>�Q�[�~���O���ɑ������ꂽ������</returns>
    public static string Gaming(this string str)
    {
        // ���F���̍����F
        Color[] colors = { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta };

        // �F�̎g�p�Ǘ��p���X�g
        List<Color> availableColors = colors.ToList();

        string gamingString = "";

        foreach (char character in str)
        {
            // �����_���ȐF��I��
            int randomIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomIndex];
            // �g�p�����F���ꎞ�I�ɏ��O
            availableColors.RemoveAt(randomIndex);

            // �g�p�\�ȐF���Ȃ��Ȃ����ꍇ�A�p���b�g�����Z�b�g
            if (availableColors.Count == 0)
            {
                availableColors = colors.ToList();
            }

            // ������K�p 
            gamingString += character.ToString().SetColor(randomColor).SetBold().SetSize(30);
        }
        return gamingString;
    }



    /// <summary>
    /// ���F�̃O���f�[�V������K�p
    /// - �ǂ̕������ł������O���f�[�V�������K�p�����
    /// - ����
    /// - �T�C�Y30
    /// </summary>
    /// <param name="str">�Ώۂ̕�����</param>
    /// <returns>�O���f�[�V�������K�p���ꂽ������</returns>
    public static string Rainbow(this string str)
    {
        float lerpT = 0f;

        // �F���̕ω��ʂ𕶎����Ŋ�����1����������̕ω���
        float addLerpTValue = 240f / 360f / str.Length;
        string rainbowString = "";

        foreach (char character in str)
        {
            // HSV�F��ԂŃO���f�[�V�����𐶐��i�ʓx�Ɩ��x�͍ő�j
            Color rainColor = Color.HSVToRGB(lerpT, 1, 1);
            rainbowString += character.ToString().SetColor(rainColor).SetBold().SetSize(30);
            lerpT += addLerpTValue;
        }
        return rainbowString;
    }
    #endregion
}