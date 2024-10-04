
public class TextConverter
{
    public string[][] Converter(string word)
    {
        //仮フォーマット: ans1 ^ output1 | ans2 ^ output2 | ans3 ^ output3
        //仮フォーマット: spriteName1^imageNameA|spriteName2^imageNameB
        string plainText = word.Split(']')[1];  //タグを外す
        string[] pairInputOutput = plainText.Split('|'); //ペア同士を含んだデータを取得
        string[][] output = new string[2][];
        output[0] = new string[pairInputOutput.Length];
        output[1] = new string[pairInputOutput.Length];
        for (int j = 0; j < pairInputOutput.Length; j++)
        {
            output[0][j] = pairInputOutput[j].Split('^')[0];  //Sprite(answer)とImage(output,flag)のペアの分離
            output[1][j] = pairInputOutput[j].Split('^')[1];
        }
        return output;
    }
}
