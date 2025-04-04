using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Game.Util{
    public class InGameMessage{

        //All messages to be displayed
        public static List<InGameMessage> messages = new List<InGameMessage>();

        //how much opacity is left in the string?
        private float endurance = 1.0f;
        //how quickly will the string lose opacity?
        private float decay = 0.9f;
        //how long before the string loses opacity?
        private int persistence = 0;
        private string text;
        private Vector2 position = new Vector2(100, 100);
        private float lowerBound = 0.001f;
        public InGameMessage(string message){
            this.text = message;
            messages.Add(this);
        }

        public InGameMessage(string message, Vector2 pos){
            this.text = message;
            this.position = pos;
            messages.Add(this);
        }

        public InGameMessage(string message, Vector2 pos, int length){
            this.text = message;
            this.position = pos;
            this.persistence = length;
            messages.Add(this);
        }

        public static void agit(State.Game game){
            for(int i = 0; i < messages.Count; i++){
                messages[i].update(game);
            }
        }

        public static void drawAll(SpriteBatch sb){
            for(int i = 0; i < messages.Count; i++){
                messages[i].draw(sb);
            }
        }

        public void update(State.Game game){
            if(this.persistence <= 0){
                this.endurance *= this.decay;
            }else{
                this.persistence--;
            }
            if(this.endurance < this.lowerBound){
                messages.Remove(this);
            }
        }

        public void draw(SpriteBatch sb){
            sb.DrawString(Main.fonts["arial12"], this.text, this.position, Color.White * this.endurance);
        }

    }
}