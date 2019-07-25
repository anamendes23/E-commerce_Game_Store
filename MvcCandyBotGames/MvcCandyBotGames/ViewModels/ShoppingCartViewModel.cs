using System.Collections.Generic;
using MvcCandyBotGames.Models;

namespace MvcCandyBotGames.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}