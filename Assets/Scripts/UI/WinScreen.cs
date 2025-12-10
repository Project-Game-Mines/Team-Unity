using System.Collections;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [Header("Prefab de win")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] public TextMeshProUGUI winText;
    
    [Header("Configurações da Animação")]
    [SerializeField] private float duracaoAnimacao = 1.5f; // Duração da contagem em segundos
    [SerializeField] private AnimationCurve curvaAnimacao = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine animacaoAtual;

    private void Awake()
    {
        if (winScreen != null) 
        {
            winScreen.SetActive(false);
        }
    }
    
    public void AtivarTela()
    { 
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Debug.Log("Activate WinScreen2");
        }
        else
        {
            Debug.LogWarning("WinScreen: O GameObject 'winScreen' não está associado no Inspector.");
        }
    }
    
    // Método para mostrar valor com animação
    public void MostrarValorAnimado(float valorFinal, string prefixo = "", string sufixo = "")
    {
        if (animacaoAtual != null)
        {
            StopCoroutine(animacaoAtual);
        }
        animacaoAtual = StartCoroutine(AnimarContagem(valorFinal, prefixo, sufixo));
    }
    
    // Sobrecarga para aceitar int
    public void MostrarValorAnimado(int valorFinal, string prefixo = "", string sufixo = "")
    {
        MostrarValorAnimado((float)valorFinal, prefixo, sufixo);
    }
    
    private IEnumerator AnimarContagem(float valorFinal, string prefixo, string sufixo)
    {
        float tempoDecorrido = 0f;
        float valorInicial = 0f;
        
        while (tempoDecorrido < duracaoAnimacao)
        {
            tempoDecorrido += Time.deltaTime;
            float progresso = tempoDecorrido / duracaoAnimacao;
            
            // Aplica a curva de animação para suavizar o movimento
            float progressoSuavizado = curvaAnimacao.Evaluate(progresso);
            
            float valorAtual = Mathf.Lerp(valorInicial, valorFinal, progressoSuavizado);
            
            // Formata o número (remove casas decimais se for número inteiro)
            string valorFormatado = valorFinal % 1 == 0 
                ? Mathf.RoundToInt(valorAtual).ToString() 
                : valorAtual.ToString("F2");
            
            winText.text = prefixo + valorFormatado + sufixo;
            
            yield return null;
        }
        
        // Garante que o valor final seja exato
        string valorFinalFormatado = valorFinal % 1 == 0 
            ? Mathf.RoundToInt(valorFinal).ToString() 
            : valorFinal.ToString("F2");
        
        winText.text = prefixo + valorFinalFormatado + sufixo;
        animacaoAtual = null;
    }
    
    public void DesativarTela()
    {
        if (animacaoAtual != null)
        {
            StopCoroutine(animacaoAtual);
            animacaoAtual = null;
        }
        
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }
}