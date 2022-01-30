using DAL.Models;

namespace DAL.ViewModels
{
    public class PlayerMove
    {
        public string GameID { get; set; }
        
        public char ActivePlayer { get; set; }

        public PlayerHistory Move { get; set; }

    }
}