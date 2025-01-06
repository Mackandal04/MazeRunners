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
            System.Console.WriteLine("Se annadio el token");
            playerTokens.Add(token);
        }

        public void RemoveToken(Tokens token)
        {
            for (int i = 0; i < playerTokens.Count; i++)
            {
                if(playerTokens[i] == token)
                {
                    playerTokens.RemoveAt(i);
                    return;
                }
            }

            System.Console.WriteLine("the token wasn't found it");
        }
    }
}