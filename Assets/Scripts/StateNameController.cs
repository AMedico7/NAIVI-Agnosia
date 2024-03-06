using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateNameController : MonoBehaviour
{
    // Dialogues
    private static string[] dialogueBeforeLocation0 = {
        "Dr. P: ¡Hola!", 
        "Señora P: Buenos días, cariño.", 
        "Dr. P: Hoy va a ser un día estupendo.", 
        "Señora P: Recuerda que tienes cita con el doctor Sacks.", 
        "Dr. P: Cierto, mejor que vaya con tiempo, no vaya a ser que tenga algún percance."
    };

    private static string[] dialogueAfterLocation0 = {
        "Dr. P: Ya falta menos para llegar a la clínica. ¡Casi me pierdo!"
    };

    private static string[] dialogueBeforeLocation1 = {
        "Dr. P: El doctor Sacks es un hombre muy inteligente, me encanta charlar con él.", 
        "Dr. Sacks: Buenas Dr. P, adelante.", 
        "Dr. P: Buenos días doctor Sacks."
    };

    private static string[] dialogueAfterLocation1 = {
        "Dr. P: Un placer como siempre doctor Sacks.", 
        "Dr. Sacks: El placer es mío, cuídese.", 
        "Dr. P: Ahora rumbo a la escuela."
    };

    private static string[] dialogueBeforeLocation2 = {
        "Dr. P: La escuela está a la vuelta de la esquina, o eso creo yo..."
    };

    private static string[] dialogueAfterLocation2 = {
        "Alumno: ¡Adiós Dr. P nos vemos el jueves!", 
        "Dr. P: ¡Adiós chicos!", 
        "Dr. P: Da gusto dar clase a alumnos así, aunque cada año me cuesta más quedarme con sus caras."
    };


    public static int unlockedLevel = 0;
    public static int currentPosition = 0;
    public static int destinationLevel;

    public enum Location
    {
        HOUSE,
        CLINIC,
        MUSIC_SCHOOL
    }

    public static int location = 0;
    public static string[] lines;
    
    public static void GoToDestination()
    {

        if (destinationLevel == 0){
            SceneManager.LoadScene("SlidePuzzle");
        } 
        else {
            SceneManager.LoadScene("Map");
        }
    }

    public static void GoToLevel(int levelId)
    {
        GoToDialogue(Location.MUSIC_SCHOOL, dialogueAfterLocation2);
        //destinationLevel = levelId;
        //currentPosition = levelId;
        //SceneManager.LoadScene("RythmGame");
    }


    // Example usage: GoToDialogue(Location.HOUSE, new string[] {"Dr. P. : Hello!", "Dr. Sacks: Hi!"});
    public static void GoToDialogue(Location dialogueLocation, string[] dialogueLines)
    {

        location = (int)dialogueLocation;
        lines = dialogueLines;
        SceneManager.LoadScene("Dialogue");
    }


    public static void ExitDialogue()
    {
        Debug.Log("EXITING DIALOGUE");
        SceneManager.LoadScene("Map");
    }

    
}
