using UnityEngine;
using TMPro;
using System.Collections;

public class TextColorChanger : MonoBehaviour
{
    private TextMeshProUGUI text; // Reference to the UI Text
    private Color[] originalColors; // To store the original vertex colors
    public float changeInterval = 1f; // Time interval to change color

    private void Start()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        text = GetComponent<TextMeshProUGUI>();

        // Store the original vertex colors of the text
        originalColors = new Color[text.textInfo.characterCount];
        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            originalColors[i] = text.textInfo.meshInfo[0].colors32[i];
        }

        // Start the color change coroutine
        StartCoroutine(ChangeTextColor());
    }

    private IEnumerator ChangeTextColor()
    {
        // Infinite loop to continuously change color
        while (true)
        {
            // Change vertex color to red
            ChangeVertexColor(Color.red);
            yield return new WaitForSeconds(changeInterval);

            // Change vertex color back to the original color
            ChangeVertexColor(originalColors);
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private void ChangeVertexColor(Color newColor)
    {
        // Iterate through the vertices of the text and set the color
        TMP_TextInfo textInfo = text.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            textInfo.meshInfo[0].colors32[vertexIndex] = newColor;
        }

        // Update the mesh to reflect the changes
        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private void ChangeVertexColor(Color[] colors)
    {
        // Iterate through the vertices of the text and set the original color
        TMP_TextInfo textInfo = text.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            textInfo.meshInfo[0].colors32[vertexIndex] = colors[i];
        }

        // Update the mesh to reflect the changes
        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
