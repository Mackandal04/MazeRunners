using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public class Player
    {
        public bool isYourTurn ; //para saber si es o no el turno de player actual
        public List<Tokens> playerTokens = new List<Tokens>(); //lista con los tokens de cada player
        public void AddToken(Tokens token)
        {
            //Annade ek token selecc a la lista de tokens del player
            playerTokens.Add(token);
        }
    }
}