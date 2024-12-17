using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Unityのデバッグログに装飾機能を追加する拡張メソッドクラス
/// リッチテキストを使用
/// </summary>
public static class DebugLogExtentions
{
    #region 基本メソッド
    /// <summary>
    /// 文字列の色を変更
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <param name="color">適用する色（Colorクラス）</param>
    /// <returns>HTMLカラータグで装飾された文字列</returns>
    /// <example>
    /// 使用例: "テキスト".SetColor(Color.red)
    /// </example>
    public static string SetColor(this string str, Color color)
    {
        string colorHtmlString = ColorUtility.ToHtmlStringRGBA(color);
        return $"<color=#{colorHtmlString}>{str}</color>";
    }


    /// <summary>
    /// 文字列のサイズを変更
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <param name="sizeValue">フォントサイズ（15が標準、30以上で太字の大きめテキスト）</param>
    /// <returns>サイズタグで装飾された文字列</returns>
    public static string SetSize(this string str, int sizeValue)
    {
        return $"<size={sizeValue}>{str}</size>";
    }


    /// <summary>
    /// 文字列を太字に変更
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>太字タグで装飾された文字列</returns>
    public static string SetBold(this string str)
    {
        return $"<b>{str}</b>";
    }



    /// <summary>
    /// 文字列を斜体
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>斜体タグで装飾された文字列</returns>
    public static string SetItalic(this string str)
    {
        return $"<i>{str}</i>";
    }


    /// <summary>
    /// 文字列の末尾に改行を追加
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>改行が追加された文字列</returns>
    public static string SetEnter(this string str)
    {
        return $"{str}\n";
    }
    #endregion






    #region 便利メソッド


    /// <summary>
    /// 黄色で太字の装飾
    /// - 黄色
    /// - 太字
    /// - サイズ15
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>タグで装飾された文字列</returns>
    public static string BoldYellow(this string str)
    {
        string boldYString = str.SetColor(Color.yellow).SetBold().SetSize(15);
        return boldYString;
    }

    /// <summary>
    /// 赤色で太字の装飾
    /// - マゼンタ
    /// - 太字
    /// - サイズ15
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>タグで装飾された文字列</returns>
    public static string BoldRed(this string str)
    {
        string errorStr = str.SetColor(Color.magenta).SetBold().SetSize(15);
        return errorStr;
    }

    /// <summary>
    /// ゲーミング風の装飾を適用します（各文字がランダムな色で表示）
    /// - 文字ごとに決まったランダムな色（red, yellow, blue, green, magenta）
    /// - 太字
    /// - サイズ30
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>ゲーミング風に装飾された文字列</returns>
    public static string Gaming(this string str)
    {
        // 視認性の高い色
        Color[] colors = { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta };

        // 色の使用管理用リスト
        List<Color> availableColors = colors.ToList();

        string gamingString = "";

        foreach (char character in str)
        {
            // ランダムな色を選択
            int randomIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomIndex];
            // 使用した色を一時的に除外
            availableColors.RemoveAt(randomIndex);

            // 使用可能な色がなくなった場合、パレットをリセット
            if (availableColors.Count == 0)
            {
                availableColors = colors.ToList();
            }

            // 装飾を適用 
            gamingString += character.ToString().SetColor(randomColor).SetBold().SetSize(30);
        }
        return gamingString;
    }



    /// <summary>
    /// 虹色のグラデーションを適用
    /// - どの文字数でも同じグラデーションが適用される
    /// - 太字
    /// - サイズ30
    /// </summary>
    /// <param name="str">対象の文字列</param>
    /// <returns>グラデーションが適用された文字列</returns>
    public static string Rainbow(this string str)
    {
        float lerpT = 0f;

        // 色相の変化量を文字数で割った1文字あたりの変化量
        float addLerpTValue = 240f / 360f / str.Length;
        string rainbowString = "";

        foreach (char character in str)
        {
            // HSV色空間でグラデーションを生成（彩度と明度は最大）
            Color rainColor = Color.HSVToRGB(lerpT, 1, 1);
            rainbowString += character.ToString().SetColor(rainColor).SetBold().SetSize(30);
            lerpT += addLerpTValue;
        }
        return rainbowString;
    }
    #endregion
}