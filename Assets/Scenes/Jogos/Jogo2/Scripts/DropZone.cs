using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string tipoAceito; // "Saudavel" ou "NaoSaudavel"
    private int acertos = 0;
    public int totalNecessario = 3;

    public void OnDrop(PointerEventData eventData)
    {
        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();
        if (item != null && item.tipoAlimento == tipoAceito)
        {
            item.transform.SetParent(this.transform);
            item.enabled = false;
            acertos++;

            if (acertos >= totalNecessario)
            {
                VerificaFimDeJogo();
            }
        }
    }

    private void VerificaFimDeJogo()
    {
        DropZone[] zonas = FindObjectsOfType<DropZone>();
        bool tudoCerto = true;

        foreach (DropZone zona in zonas)
        {
            if (zona.acertos < zona.totalNecessario)
                tudoCerto = false;
        }

        if (tudoCerto)
        {
            SceneManager.LoadScene("NivelConcluido");
        }
    }
}