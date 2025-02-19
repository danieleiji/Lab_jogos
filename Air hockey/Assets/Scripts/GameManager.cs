using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0; // Placar ainda pode ser estático
    public static int PlayerScore2 = 0;
    public GUISkin layout;
    private GameObject theBall; // Não é estático

    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        if (theBall == null)
        {
            Debug.LogError("Bola não encontrada! Verifique a tag 'Ball'.");
        }
    }

    // Removemos o 'static' do método Score.
    public void Score(string wallID)
    {
        if (wallID == "PontoAbaixo")
        {
            PlayerScore2++;
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, -2);
            }
        }
        else if (wallID == "PontoAcima")
        {
            PlayerScore1++;
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, 2);
            }
        }
        Debug.Log("Placar: Jogador Red: " + PlayerScore1 + " | PlayerAI: " + PlayerScore2);
    }

    void OnGUI()
    {
        if (layout != null)
        {
            GUI.skin = layout;
        }

        // Exibe o placar
        GUI.Label(new Rect(Screen.width / 2 - 300 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 300 - 12, 20, 100, 100), "" + PlayerScore2);

        // Estilo para a mensagem de vitória
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = Color.red; // Cor da fonte
        style.fontSize = 50; // Tamanho da fonte
        style.alignment = TextAnchor.MiddleCenter; // Alinhamento

        // Verifica se um jogador venceu
        if (PlayerScore1 >= 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, 200, 600, 100), "PLAYER RED WINS", style);
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, -2); // Ou 0, 0 para o centro
            }
        }
        else if (PlayerScore2 >= 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, 200, 600, 100), "PLAYER AI WINS", style);
			if (theBall != null)
            {
            	theBall.GetComponent<BallControl>().ResetBall(0, 2); // Ou 0, 0 para o centro
			}
        }
    }
}