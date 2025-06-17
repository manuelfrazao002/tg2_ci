using UnityEngine;

public class MoverPlataformasCombinado : MonoBehaviour
{
    public GameObject[] plataformas;
    public float amplitude = 5.0f;
    public float velocidadeHorizontal = 3.0f;
    public float velocidadeDescida = 1f;

    private Vector3[] posicoesOriginais;

    void Start()
    {

        posicoesOriginais = new Vector3[plataformas.Length];
        for (int i = 0; i < plataformas.Length; i++)
        {
            posicoesOriginais[i] = plataformas[i].transform.position;
        }
    }

    void Update()
    {
        for (int i = 0; i < plataformas.Length; i++)
        {

            float deslocamentoX = Mathf.Sin(Time.time * velocidadeHorizontal + i) * amplitude;


            float deslocamentoY = -velocidadeDescida * Time.time;

            Vector3 novaPosicao = posicoesOriginais[i] + new Vector3(deslocamentoX, deslocamentoY, 0f);

            plataformas[i].transform.position = novaPosicao;
        }
    }
}