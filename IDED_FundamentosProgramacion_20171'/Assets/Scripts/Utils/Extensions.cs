public static class Extensions
{
    public static char[] FillWithChar(this char[] targetArray, char valueToFill)
    {
        if (targetArray == null)
        {
            return null;
        }

        for (int i = 0; i < targetArray.Length; i++)
        {
            targetArray[i] = valueToFill;
        }

        return targetArray;
    }

    public static void RemoveCharacterFromElements(this string[] targetArray, char valueToRemove)
    {
        if (targetArray != null)
        {
            for (int i = 0; i < targetArray.Length; i++)
            {
                string currentWord = targetArray[i];
                targetArray[i] = currentWord.Remove(currentWord.LastIndexOf(valueToRemove));
            }
        }
    }
}