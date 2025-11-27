using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombsSelector : MonoBehaviour
{
    // Arraste e solte os componentes da UI aqui no Inspector
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button incrementButton;
    [SerializeField] private TextMeshProUGUI valueText; // Ou 'Text' se não usar TMP

    // Variáveis de controle
    [SerializeField] private int currentValue = 5;
    [SerializeField] private int minValue = 1;
    [SerializeField] private int maxValue = 20; // Exemplo

    private void Start()
    {
        // Adiciona os listeners aos botões
        decrementButton.onClick.AddListener(DecrementValue);
        incrementButton.onClick.AddListener(IncrementValue);

        UpdateUI();
    }

    private void DecrementValue()
    {
        if (currentValue > minValue)
        {
            currentValue--;
            UpdateUI();
        }
    }

    private void IncrementValue()
    {
        if (currentValue < maxValue)
        {
            currentValue++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // Atualiza o texto. Se usar o Sprite Asset, ajuste a string.
        // Exemplo simples (sem ícone):
        valueText.text = currentValue.ToString(); 

        // Exemplo com ícone TMP (se você não tiver um Image separado):
        // valueText.text = $"<sprite index=0> {currentValue}"; 

        // Desabilita os botões nos limites (opcional)
        decrementButton.interactable = currentValue > minValue;
        incrementButton.interactable = currentValue < maxValue;
    }

    // Método para obter o valor selecionado
    public int GetSelectedValue()
    {
        return currentValue;
    }
}