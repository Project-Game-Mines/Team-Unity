
using System.Collections.Generic;
using UnityEngine;

public class MockPlayer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    public int playerid = 123;
    public float balance = 498.45f;
    public List<int> mineList = new();


    public void StartGame()
    {
        CratingRandomMines();
    }
    public void CratingRandomMines()
    {
        const int limiteMaximoExclusivo = 25;
        
        // Limpa a lista antes de gerar novas minas (boa prática)
        mineList.Clear(); 

        for (int i = 0; i < _gameManager.bombAmount; i++)
        {
            int numeroAleatorio;
            
            // 💡 LOOP DE VERIFICAÇÃO DE UNICIDADE
            // Enquanto o número gerado já estiver na lista, gere um novo.
            do
            {
                numeroAleatorio = Random.Range(0, limiteMaximoExclusivo);
            }
            while (mineList.Contains(numeroAleatorio)); // <-- Esta é a chave!

            // Assim que encontrar um número único, adicione-o à lista.
            mineList.Add(numeroAleatorio);
        }
        
        Debug.Log("Minas geradas: " + string.Join(", ", mineList));
    }


}
