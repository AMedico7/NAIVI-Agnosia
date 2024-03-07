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
        "Dr. P: Mi día a día es como un rompecabezas."
    };

    private static string[] dialogueAfterLocation0 = {
        "Señora P: Recuerda que tienes cita con el doctor Sacks.", 
        "Dr. P: Cierto, mejor que vaya con tiempo, no vaya a ser que tenga algún percance.",
        "Dr. P: Siguiendo las flechas al ritmo de la música seguro que llego a tiempo."
    };

    private static string[] dialogueBeforeLocation1 = {
        "Dr. P: Ya falta menos para llegar a la clínica. ¡Casi me pierdo!",
        "Dr. Sacks: Buenas Dr. P, adelante.", 
        "Dr. Sacks: ¿Y que le pasa a usted?",
        "Dr. P: A mí me parece que nada, pero todos me dicen que me pasa algo raro en la vista.",
        "Dr. Sacks: Pero usted no nota ningún problema en la vista",
        "Dr. P: No, directamente no, pero a veces cometo errores",
        "Tras una breve charla el Dr. Sacks determina que las formas abstractas no plantean problemas para el Dr. P",
        "El Dr. Sacks decide mostrarle un libro de caricaturas",
        "Dr. P, pensando: \"Espero que hoy pueda superar la prueba de Sacks\""
    };

    private static string[] dialogueAfterLocation1 = {
        "El Dr. P se dispone a irse de la clínica",
        "Dr. Sacks: ¿Quiere que le ayude?",
        "Dr. P: ¿Ayudarme a que? ¿Ayudar a quién?",
        "Dr. Sacks: Ayudarle a usted a ponerse el zapato",
        "Dr. P: Ah sí, el zapato - dice señalando su propio pie",
        "Dr. Sacks: No, eso no es su zapato, su zapato está ahí.",
        "Dr. P: ¡Ah! Creí que era el pie.",
        "Dr. Sacks, pensando: \"Si este es uno de sus <<extraños>> errores es el más extraño que he visto en mi vida\"",
        "Dr. P: Un placer, doctor Sacks.", 
        "Dr. Sacks: El placer es mío, cuídese.", 
        "Dr. P: Ahora rumbo a la escuela de música.",
        "Dr. P: Espero que la combinación de flechas y música me guie igual de bien que antes."
    };

    private static string[] dialogueBeforeLocation2 = {
        "Dr. P: La escuela está a la vuelta de la esquina, o eso creo yo...",
        "Alumno: Buenos días Dr. P.",
        "Dr. P: Da gusto dar clase con alumnos así, aunque cada año me cuesta más quedarme con sus caras."
    };

    private static string[] dialogueAfterLocation2 = {
        "Alumno: ¡Adiós Dr. P nos vemos el jueves!", 
        "Dr. P: ¡Adiós chicos!", 
        "Dr. P: Me encanta mi trabajo, no lo cambiaria por nada."
    };

    private static string[] dialogueBeforeEnding = {
        "En \"El mundo como voluntad y representación\", Schopenhauer dice que la música es <<Voluntad Pura>>.",
        "Cómo le habria fascinado el Dr. P, un hombre que había perdido completamente el mundo como representación...",
        "... pero que lo preserva totalmente como música o voluntad.",
        "Pues a pesar del avance gradual de su enfermedad, el Dr. P sigue enseñando música y viviendola."
    };

    public static int step = 0;
    
    public static int unlockedLevel = 0;
    public static int currentPosition = -1;
    public static int destinationLevel = 0;


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
                    GoToDialogue(Location.MUSIC_SCHOOL, dialogueBeforeLocation2);
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
                SceneManager.LoadScene("MemoryPuzzle");
                
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


    public static void NewGame()
    {
        step = 0;
        unlockedLevel = 0;
        currentPosition = -1;
        destinationLevel = 0;
        SceneManager.LoadScene("Map");
    }

    public static void ContinueGame()
    {
        Next();
    }
}
