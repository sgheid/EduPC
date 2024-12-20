using UnityEngine;
using TMPro;

public class ScrollableText : MonoBehaviour
{
    public float scrollSpeed = 50f; // Velocità dello scorrimento per input della rotella
    private RectTransform textRectTransform;

    private void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Controlla l'input della rotella del mouse
        float scrollInput = Input.mouseScrollDelta.y;

        if (scrollInput != 0)
        {
            // Muove il testo in base al movimento della rotella
            textRectTransform.localPosition += Vector3.up * scrollInput * scrollSpeed * Time.deltaTime;

            // Limita il movimento per evitare che il testo esca completamente dai bordi
            float maxY = textRectTransform.rect.height / 2;
            float minY = -textRectTransform.rect.height / 2;
            textRectTransform.localPosition = new Vector3(
                textRectTransform.localPosition.x,
                Mathf.Clamp(textRectTransform.localPosition.y, minY, maxY),
                textRectTransform.localPosition.z
            );
        }
    }
}
