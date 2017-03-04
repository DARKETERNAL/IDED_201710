using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase de extensiones para funcionalidad de arreglos.
/// </summary>
public static class ArrayExtensions
{
    /* Uso de funciones (teoría) + uso de funciones de extensión (C#)
       Aquí definimos nueva funcionalidad para tipòs de datos existentes.
       Bonificación para parcial práctico (0.1): Qué es una función de extensión? Cómo se define?
       Consultar esas preguntas para ejercicio de bonificación que se propondrá posteriormente.
     */

    //Para pensar: Cuál es la principal ventaja de escribir funciones que se puedan reutilizar como las siguientes?
    //Para pensar y consultar: Cómo podríamos escribir las siguientes funciones para que soporten arreglos de cualquier tipo?

    //Genera una lista de strings con los elementos del arreglo.
    public static List<string> ToList(this string[] sourceArray)
    {
        List<string> result = new List<string>();

        if (sourceArray == null)
        {
            result = null;
        }
        else
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                result.Add(sourceArray[i]);
            }
        }

        return result;
    }

    //Imprime los valores de un arreglo.
    public static void PrintArrayValues(this string[] words)
    {
        if (words != null && words.Length > 0)
        {
            Debug.Log("Processed words");
            int wordCount = words.Length;

            for (int i = 0; i < wordCount; i++)
            {
                //Qué diferencia hay entre esta línea y la que queda habilitada?
                //Ambas líneas imprimen lo mismo.
                //Debug.Log("Element in position [" + i + "/" + wordCount + "]: " + "[ " + words[i] + "]");

                Debug.Log(string.Format("Element in position [{0}/{1}]: [{2}]\n", i, wordCount, words[i]));
            }
        }
        else
        {
            Debug.Log("Can't print array values. Input is invalid");
        }
    }

    public static void ToLowercase(this string[] sourceArray)
    {
        if (sourceArray == null)
        {
            Debug.Log("Can't process array. Array is invalid");
        }
        else
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                //Cuál es la diferencia entre ToLowerInvariant() y ToLower(). En qué caso se pueden usar indistintamente?
                sourceArray[i] = sourceArray[i].ToLowerInvariant();
            }
        }
    }
}

/// <summary>
/// Componente para organizar strings dado un archivo de texto fuente.
/// Se apoya fuertemente en uso de funciones.
/// </summary>
public class StringSorter : MonoBehaviour
{
    // Usen un archivo de texto de su preferencia y agréguenlo al objeto de Unity.
    [SerializeField]
    private TextAsset sourceText;

    /* Ejercicio: Creen funciones nuevas para reemplazar SortAlphabetically() y SortByNumberOfCharacters()
     * que hagan lo mismo, pero que usen este arreglo, que pueden llenar desde el inspector de Unity.
     *
     * Bonificación: Se puede usar una técnica de diseño llamada "Sobrecarga de funciones".
    */

    [SerializeField]
    private string[] sourceWords;

    [SerializeField]
    private bool useSourceText = true;

    //Colecciones necesarias para las funciones.
    //Para experimentar: Y si las quitamos como variables globales y las dejamos como variables locales?
    //Alguna debe permanecer como global?
    private string[] processedWords;

    private string[] sortedArray;

    //Para qué sirve el tag ContextMenu en Unity?
    [ContextMenu("Sort alphabetically")]
    public void SortAlphabetically()
    {
        SortAlphabetically(useSourceText);
    }

    public void SortAlphabetically(bool useSourceText)
    {
        string[] sourceArray = useSourceText ? processedWords : sourceWords;

        if (sourceArray != null && sourceArray.Length > 0)
        {
            sortedArray = new string[sourceArray.Length];

            //Aquí aplicamos una de esas funciones de extensión.
            List<string> wordList = sourceArray.ToList();

            if (wordList == null)
            {
                Debug.Log("Can't use provided list. List is invalid");
            }
            else
            {
                for (int i = 0; i < sortedArray.Length; i++)
                {
                    string selectedWord = SelectNextWordAlphabetically(wordList);
                    sortedArray[i] = selectedWord;

                    //Por qué hacemos esto aquí y no en la función que elige la palabra?
                    wordList.Remove(selectedWord);

                    Debug.Log(string.Format("Removed [{0}] from list", selectedWord));
                }

                //Aquí aplicamos la otra función de extensión.
                sortedArray.PrintArrayValues();
            }
        }
        else
        {
            Debug.Log("Can't sort. Source is invalid");
        }
    }

    [ContextMenu("Sort by number of Characters")]
    public void SortByNumberOfCharacters()
    {
        SortByNumberOfCharacters(useSourceText);
    }

    private void SortByNumberOfCharacters(bool useSourceText)
    {
        string[] sourceArray = useSourceText ? processedWords : sourceWords;

        if (sourceArray != null && sourceArray.Length > 0)
        {
            sortedArray = new string[sourceArray.Length];
            List<string> wordList = processedWords.ToList();

            if (wordList == null)
            {
                Debug.Log("Can't use provided list. List is invalid");
            }
            else
            {
                for (int i = 0; i < sortedArray.Length; i++)
                {
                    string selectedWord = SelectWordByNumberOfCharacters(wordList);
                    sortedArray[i] = selectedWord;
                    wordList.Remove(selectedWord);
                    Debug.Log(string.Format("Removed [{0}] from list", selectedWord));
                }

                sortedArray.PrintArrayValues();
            }
        }
        else
        {
            Debug.Log("Can't sort. Source is invalid");
        }
    }

    //Este es evento de Unity.
    //Para experimentar: Y si esto fuera una función que pudiéramos llamar donde fuera, aparte del Start()?
    private void Start()
    {
        if (sourceText != null)
        {
            //Qué pasó acá?
            processedWords = sourceText.text.Split(' ', '\n');

            //Por qué fue que dijimos que había que hacer esto?
            processedWords.ToLowercase();

            processedWords.PrintArrayValues();
        }
    }

    private string SelectNextWordAlphabetically(List<string> words)
    {
        string selectedWord = string.Empty;

        if (words == null || words.Count == 0)
        {
            Debug.Log("Can't use provided list. List is invalid or empty");
        }
        else
        {
            if (words.Count == 1)
            {
                selectedWord = words[0];
            }
            else
            {
                int firstIndex = 0;
                int secondIndex = 1;

                // Una diferencia importante entre array y list:
                // Longitud del arreglo = arr.lenght.
                // Longitud de la lista = lst.Count.
                while (firstIndex < words.Count && secondIndex < words.Count)
                {
                    string tempA = words[firstIndex];
                    string tempB = words[secondIndex];

                    // Usamos la función string.Compare(A, B) para determinar cuál va alfabéticamente antes.
                    // Consulten el uso de esa función.
                    // Qué debemos tener en cuenta si añadiéramos el caso string.Compare(tempA, tempB) == 0?
                    // Qué pasaría en este código, tal cual está, si se diera el caso que tempA == tempB?
                    if (string.Compare(tempA, tempB) == -1)
                    {
                        selectedWord = tempA;
                        secondIndex += 1;
                    }
                    else if (string.Compare(tempA, tempB) == 1)
                    {
                        selectedWord = tempB;
                        firstIndex = secondIndex;
                        secondIndex += 1;
                    }

                    /*
                     * Hecho con switch.
                    int compareResult = string.Compare(tempA, tempB);

                    switch (compareResult)
                    {
                        case -1:
                            // tempA va primero
                            selectedWord = tempA;
                            break;

                        case 1:
                            // tempB va primero
                            selectedWord = tempB;
                            firstIndex = secondIndex;
                            break;

                        default:
                            break;
                    }

                    secondIndex += 1;
                    */
                }
            }
        }

        return selectedWord;
    }

    private string SelectWordByNumberOfCharacters(List<string> words)
    {
        string selectedWord = string.Empty;

        if (words == null)
        {
            Debug.Log("Can't use provided list. List is invalid");
        }
        else
        {
            if (words.Count == 1)
            {
                selectedWord = words[0];
            }
            else
            {
                int firstIndex = 0;
                int secondIndex = 1;

                while (firstIndex < words.Count && secondIndex < words.Count)
                {
                    string tempA = words[firstIndex];
                    string tempB = words[secondIndex];

                    int compareResult = tempA.Length.CompareTo(tempB.Length);

                    switch (compareResult)
                    {
                        case 0:
                        case -1:
                            selectedWord = tempA;
                            break;

                        default:
                            selectedWord = tempB;
                            firstIndex = secondIndex;
                            break;
                    }

                    secondIndex += 1;
                }
            }
        }

        return selectedWord;
    }
}