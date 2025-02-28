using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControladorHistoria : MonoBehaviour
{
    public GameObject historiaPanel; // Painel que cont�m o texto da hist�ria (atribuir no Inspector).
    public Text historiaText;       // Componente de texto para exibir a hist�ria (atribuir no Inspector).
    public float velocidadeTexto = 20f; // Velocidade da rolagem (ajustar no Inspector).
    public string textoHistoria;     // O texto completo da hist�ria (definir no Inspector).
    public KeyCode teclaFechar = KeyCode.Escape; //(Opcional) Tecla para fechar a hist�ria (definir no Inspector).

    private bool mostrandoHistoria = false;
    private RectTransform rectTransformTexto;

    void Start()
    {
        // Inicializa o painel como inativo e pega o RectTransform.
        if (historiaPanel != null)
        {
            historiaPanel.SetActive(false);
            rectTransformTexto = historiaText.GetComponent<RectTransform>();

            //Verifica se os objetos foram atribuidos.
            if (historiaText == null)
            {
                Debug.LogError("Erro: Componente Text n�o atribu�do ao script ControladorHistoria!");
                enabled = false; //Desativa o script se houver erro.
                return;
            }

            if (historiaPanel == null)
            {
                Debug.LogError("Erro: GameObject do painel n�o atribu�do ao script ControladorHistoria!");
                enabled = false; //Desativa o script se houver erro.
                return;
            }


        }

    }

    // Este Update() agora � opcional.  S� � necess�rio se voc� quiser usar a tecla para fechar.
    void Update()
    {
        // Fecha a hist�ria se a tecla configurada for pressionada.
        if (mostrandoHistoria && Input.GetKeyDown(teclaFechar))
        {
            FecharHistoria();
        }
    }


    // M�todo p�blico para iniciar a exibi��o da hist�ria.  Chame este m�todo do seu bot�o.
    public void MostrarHistoria()
    {
        if (!mostrandoHistoria && historiaPanel != null)
        {
            mostrandoHistoria = true;
            historiaPanel.SetActive(true);
            historiaText.text = textoHistoria;

            // Posiciona o texto no in�cio.
            rectTransformTexto.anchoredPosition = new Vector2(0, -rectTransformTexto.rect.height / 2);

            StartCoroutine(RolarTexto());
        }
    }

    // M�todo p�blico para fechar a hist�ria.
    public void FecharHistoria()
    {
        if (mostrandoHistoria)
        {
            mostrandoHistoria = false;
            StopAllCoroutines(); // Interrompe a rolagem.
            historiaPanel.SetActive(false);
        }
    }
    // Corrotina para animar a rolagem do texto.
    private IEnumerator RolarTexto()
    {
        float alturaPainel = historiaPanel.GetComponent<RectTransform>().rect.height;
        float alturaTexto = rectTransformTexto.rect.height;

        while (mostrandoHistoria && rectTransformTexto.anchoredPosition.y < alturaTexto + (alturaPainel / 2))
        {
            rectTransformTexto.anchoredPosition += new Vector2(0, velocidadeTexto * Time.deltaTime);
            yield return null; // Espera um frame
        }

        // Fecha a hist�ria automaticamente quando a rolagem termina.
        if (mostrandoHistoria)
        {
            FecharHistoria();
        }
    }
}