using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWritingScript : MonoBehaviour
{
    private static TextWritingScript instance;
    private List<Instanced_TextWritingScript> instance_list;
    public bool TextFinish = true;
    public SoundDirector sound_director;


    private void Awake()
    {
        
        instance = this;
        instance_list = new List<Instanced_TextWritingScript>();
        TextFinish = this;
    }

    public static void TextWritingScript_Static(Text UIText, string NewText, float time)
    {
        instance.TextWriter(UIText, NewText, time);
    }

    public void TextWriter(Text UIText, string NewText, float time)
    {
        TextFinish = true;
        instance_list.Add(new Instanced_TextWritingScript(UIText, NewText, time));
        sound_director.PlayText_Displayed();
    }

    private void Update()
    {
        if(TextFinish)
        {
            sound_director.PlayTyping(TextFinish);
            TextFinish = false;
        }
        
        for (int x = 0; x < instance_list.Count; x++)
        {
            bool end_list = instance_list[x].Update();
            if(end_list)
            {
                //sound_director.PlayTyping(TextFinish);
                sound_director.StopTyping();   
                instance_list.RemoveAt(x);
                x--;
            }
        }

    }


    public class Instanced_TextWritingScript
    {
        Text UIText;

        string NewText;
        float time;

        float script_timer;
        int character_index;

        // To create a single instance of this TextWritingScript, it's handled as a separate, nested class
        public Instanced_TextWritingScript(Text UIText, string NewText, float time)
        {
            this.UIText = UIText;
            this.NewText = NewText;
            this.time = time;
        }

        public bool Update()
        {
            
            // Take variable timer in consideration. Script timer is equal to the negative, current, deltaTime. as long as that value is under 0, then the text can display a character at the time
            // by the speed dictated by time. A script timer is but one of the multiple ways to keep the process running until it is completed. 
            script_timer -= Time.deltaTime;
            //sound_director.PlayTyping();

            while (script_timer <= 0f)
            {


                script_timer += time;
                character_index++;
                UIText.text = NewText.Substring(0, character_index);

                // If the index passes the length of the text, the text is null, and the function ends.
                 if (character_index >= NewText.Length)
                 {
                    UIText = null;
                    return true;
                    
                }

            }

            //sound_director.PlayTyping();
            return false;

        }
    }
}


