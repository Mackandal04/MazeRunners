using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public class Player
    {
        public string name;
        public bool isYourTurn ; //para saber si es o no el turno de player actual
        public List<Tokens> playerTokens = new List<Tokens>(); //lista con los tokens de cada player
        
        public Player(string name)
        {
            this.name = name;
        }
        public void AddToken(Tokens token)
        {
            //Annade el token selecc a la lista de tokens del player
            
            playerTokens.Add(token);
        }

        public void RemoveToken(Tokens token)
        {
            for (int i = 0; i < playerTokens.Count; i++)
            {
                if(playerTokens[i] == token)
                    playerTokens.RemoveAt(i);
            }
        }
    }
}