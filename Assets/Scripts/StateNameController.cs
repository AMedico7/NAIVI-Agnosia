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

    private static string[] dialogueBeforeEnding = {
        ""
    };

    public static int step = 0;
    
    public static int unlockedLevel = 0;
    public static int currentPosition = -1;
    public static int destinationLevel = 0;

    
    public static string previousScene = "";
    public static string currentScene = "";



    // Dialogue
    public static int location = 0;
    public static string[] lines;

    public enum Location
    {
        HOUSE,
        CLINIC,
        MUSIC_SCHOOL
    }



    // Rythm Variables
    public static int rythmGameDifficulty = 1;



    public static void Next()
    {
        // Debug.Log("ENTERED NEXT WITH CURRENT POSITION " + currentPosition + " DESTINATION LEVEL " + destinationLevel + " CURRENT LOCATION " + currentPosition +  " STEP " + step);
        
        // Hasn't entered first level
        if (currentPosition == -1)
        {
            destinationLevel = 0;

            // Before dialogue
            if (step == 0) 
            {
                step++;
                GoToDialogue(Location.HOUSE, dialogueBeforeLocation0);
            }
            // After dialogue
            else if (step == 1)
            {
                currentPosition = 0;
                step = 0;
                GoToPuzzle(currentPosition);
            }

        }
        else if (currentPosition == 0)
        {
            // Go to location puzzle
            if (destinationLevel == 0)
            {
                // Finish level 0, dialogue after level 0
                if (step == 0)
                {
                    step++;
                    GoToDialogue(Location.HOUSE, dialogueAfterLocation0);
                }
                // Finish dialogue, unlock next level and go to map
                else
                {
                    step=0;
                    unlockedLevel=1;
                    destinationLevel=1;
                    GoToMap();
                }
            }
            // Going to next puzzle
            else
            {
                if (step==0)
                {
                    step++;
                    TransitionLevel(destinationLevel);
                }
                else if (step==1)
                {
                    step++;
                    GoToDialogue(Location.CLINIC, dialogueBeforeLocation1);
                }
                else
                {
                    currentPosition = 1;
                    step = 0;
                    GoToPuzzle(currentPosition);
                }
            }

            

        }
        else if (currentPosition == 1)
        {
            if (destinationLevel == 1)
            {
                // Finish level 1, dialogue after level 1
                if (step == 0)
                {
                    step++;
                    GoToDialogue(Location.CLINIC, dialogueAfterLocation1);
                }
                // Finish dialogue, unlock next level and go to map
                else 
                {
                    step=0;
                    unlockedLevel=2;
                    destinationLevel=2;
                    GoToMap();
                }
            }
            else
            {
                if (step==0)
                {
                    step++;
                    TransitionLevel(destinationLevel);
                }
                else if (step==1)
                {
                    step++;
                    GoToDialogue(Location.CLINIC, dialogueBeforeLocation2);
                }
                else
                {
                    currentPosition = 2;
                    step = 0;
                    GoToPuzzle(currentPosition);
                }
            }

        }
        else if (currentPosition == 2)
        {
            if (destinationLevel == 2)
            {
                // Finished level 2, dialogue after level 2
                if (step == 0)
                {
                    step++;
                    GoToDialogue(Location.MUSIC_SCHOOL, dialogueAfterLocation2);
                }
                else
                {
                    step=0;
                    unlockedLevel=3;
                    destinationLevel=3;
                    GoToMap();
                }
            }
            else 
            {
                if (step==0)
                {
                    step++;
                    TransitionLevel(destinationLevel);
                }
                else if (step==1)
                {
                    step++;
                    GoToDialogue(Location.HOUSE, dialogueBeforeEnding);
                }
                else 
                {
                    GoToEnding();
                }
            }


        }

    }

    public static void GoToDialogue(Location dialogueLocation, string[] dialogueLines)
    {

        location = (int)dialogueLocation;
        lines = dialogueLines;
        SceneManager.LoadScene("Dialogue");
    }

    public static void GoToPuzzle(int puzzleLocation)
    {
        switch (puzzleLocation)
        {
            case 0:
                SceneManager.LoadScene("LinesPuzzle");
                break;
            case 1:
                SceneManager.LoadScene("LinesPuzzle");
                break;
            case 2:
                SceneManager.LoadScene("SlidePuzzle");
                break;
            default:
                break;
        }
    }


    public static void TransitionLevel(int destinationLevel)
    {
        rythmGameDifficulty = destinationLevel;
        SceneManager.LoadScene("RythmGame");
    }

    public static void GoToMap()
    {
        SceneManager.LoadScene("Map");
    }
    

    public static void GoToEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}
