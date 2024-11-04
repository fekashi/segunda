using RestauranteMvc.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestauranteMvc.Controllers
{

    public class HomeController : Controller
    {
        // Lista estática para manter os pratos
        private static List<PratoModel> cardapio = new List<PratoModel>
        {
            new PratoModel { Nome = "Pizza Margherita", Preco = 30.00m, ImagemUrl = "https://upload.wikimedia.org/wikipedia/commons/1/18/ISO_C%2B%2B_Logo.svg" },
            new PratoModel { Nome = "Spaghetti Carbonara", Preco = 25.00m, ImagemUrl = "https://www.svgrepo.com/show/341723/cplusplus.svg" },
            new PratoModel { Nome = "Lasanha Bolonhesa", Preco = 28.50m, ImagemUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQBATpoaCGJfxIMv-UDWf4gmTaw_QM2aAjMjQ&s" }
        };

        // Action para a página de login
        public ActionResult Login()
        {
            return View();
        }

        // Action para validar o login
        [HttpPost]
        public ActionResult Login(UsuarioModel usuario)
        {
            if (usuario.Nome == "admin" && usuario.Senha == "1234")
            {
                Session["IsAdmin"] = true; // Admin logado
                return RedirectToAction("Cardapio");
            }
            else if (usuario.Nome == "Andre" && usuario.Senha == "templo7k")
            {
                Session["IsAdmin"] = false; // Usuário normal logado
                Session["IsUser"] = true; // Indica que há um usuário logado, mas não é admin
                return RedirectToAction("Cardapio");
            }

            ViewBag.Mensagem = "Usuário ou senha inválidos";
            return View();
        }

        // Action para a página de cardápio
        public ActionResult Cardapio()
        {
            return View(cardapio);
        }

        // Action para a página de adição de prato, visível apenas para o admin
        [HttpGet]
        public ActionResult AdicionarPrato()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login"); // Redireciona para o login se não for admin

            return View();
        }

        [HttpPost]
        public ActionResult AdicionarPrato(PratoModel novoPrato)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            cardapio.Add(novoPrato); // Adiciona o novo prato à lista
            return RedirectToAction("Cardapio");
        }

        // Logout para finalizar a sessão de admin ou usuário
        public ActionResult Logout()
        {
            Session["IsAdmin"] = null;
            Session["IsUser"] = null;
            return RedirectToAction("Login");
        }
    }
}
