using System.Text;

XOR_Cipher xor = new XOR_Cipher();

Console.WriteLine(Encoding.UTF8.GetString(xor.Process("qwerty", "16")));
Console.WriteLine(xor.Process("qwerty", "16", true));

class XOR_Cipher 
{
    public byte[] Process(string inputText, string inputKey) 
    {
        if (inputText == null) throw new ArgumentNullException(nameof(inputText), "Вы ввели пустой текст");
        if (inputKey == null) throw new ArgumentNullException(nameof(inputKey), "Вы ввели пустой ключ");

        // Перевод в байты
        byte[] text = Encoding.UTF8.GetBytes(inputText);
        byte[] key = Encoding.UTF8.GetBytes(inputKey);

        byte[] result = new byte[text.Length];
        for (int i = 0; i < text.Length; i++) 
        {
            result[i] = (byte)(text[i] ^ key[i % key.Length]);
        }

        return result;
    }

    public string Process(string inputText, string inputKey, bool isKeyDigit) 
    {
        if (inputText == null) throw new ArgumentNullException(nameof(inputText), "Вы ввели пустой текст");
        if (inputKey == null) throw new ArgumentNullException(nameof(inputKey), "Вы ввели пустой ключ");

        string result = "";
        if (isKeyDigit)
        {
            int digitKey = Convert.ToInt32(inputKey, 16);
            for (int i = 0; i < inputText.Length; i++)
            {
                result += inputText[i] ^ digitKey;
            }
        }
        else 
        {
            for (int i = 0; i < inputText.Length; i++) 
            {
                result += inputText[i] ^ inputKey[i % inputKey.Length];
            }
        }
        string newText = Convert.ToString(Convert.ToInt64(result), toBase:16);
        return newText;
    }
}

/*Основные правила написания ошибок:

Выбрасывайте конкретные типы исключений, соответствующие ситуации:
 - ArgumentNullException — когда аргумент метода равен null, но не должен быть.
 - ArgumentException — когда аргумент имеет недопустимое значение (например, пустая строка, отрицательное число).
 - InvalidOperationException — когда вызов метода невозможен из-за текущего состояния объекта.
 - NotSupportedException — когда операция не поддерживается.
Другие стандартные или пользовательские исключения по необходимости.

Сообщение об ошибке должно быть информативным и часто включать имя параметра.
Для ArgumentNullException и ArgumentException есть конструкторы, принимающие имя параметра и сообщение.

Не используйте throw new Exception(...) — это затрудняет обработку, так как ловить Exception приходится слишком широко.*/